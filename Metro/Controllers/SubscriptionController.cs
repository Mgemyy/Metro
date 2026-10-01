using MetroApp.Models;
using MetroApp.Models.Enums;
using MetroApp.Services;
using MetroApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MetroApp.Controllers
{
    [Authorize] // Only authenticated users can submit subscription requests
    public class SubscriptionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _environment;
        private readonly IQrCodeService _qrCodeService;

        public SubscriptionController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment environment,
            IQrCodeService qrCodeService)
        {
            _context = context;
            _userManager = userManager;
            _environment = environment;
            _qrCodeService = qrCodeService;
        }

        [HttpGet]
        public async Task<IActionResult> Card(int id)
        {
            var subscription = await _context.Subscriptions
                .Include(s => s.User)
                .Include(s => s.StartStation)
                .Include(s => s.EndStation)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (subscription == null || subscription.Status != SubscriptionStatus.Approved)
                return NotFound();

            var currentUserId = _userManager.GetUserId(User);
            var isAdmin = User.IsInRole("Admin");

            if (subscription.UserId != currentUserId && !isAdmin)
                return Forbid(); 

            string payload = $"METRO-PASS:{subscription.Id}|User:{subscription.User.FullName}|Route:{subscription.StartStation.Name}-{subscription.EndStation.Name}|ValidTo:{subscription.EndDate:yyyy-MM-dd}";
            ViewBag.QrCodeImage = _qrCodeService.GenerateQrCodeBase64(payload);

            return View(subscription);
        }

        private decimal CalculatePrice(int startStationId, int endStationId, SubscriptionType type)
        {
            int stationCount = Math.Abs(startStationId - endStationId) + 1;

            if (type == SubscriptionType.Student)
            {
                return stationCount switch
                {
                    <= 9 => 150m,
                    <= 16 => 200m,
                    <= 23 => 250m,
                    _ => 300m
                };
            }

            if (type == SubscriptionType.Elderly)
            {
                return stationCount switch
                {
                    <= 9 => 100m,
                    <= 16 => 135m,
                    <= 23 => 170m,
                    _ => 200m
                };
            }

            return stationCount switch
            {
                <= 9 => 350m,
                <= 16 => 500m,
                <= 23 => 650m,
                _ => 800m
            };
        }
        [HttpGet]
        public async Task<IActionResult> MySubscriptions()
        {
            var userId = _userManager.GetUserId(User);

            var subscriptions = await _context.Subscriptions
                .Include(s => s.StartStation)
                .Include(s => s.EndStation)
                .Where(s => s.UserId == userId)
                .OrderByDescending(s => s.RequestDate)
                .ToListAsync();

            return View(subscriptions);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new SubscriptionRequestViewModel
            {
                Stations = await _context.Stations
                    .Select(s => new SelectListItem
                    {
                        Value = s.Id.ToString(),
                        Text = $"{s.Name} (Line {s.LineNumber})"
                    }).ToListAsync()
            };

            return View(model);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SubscriptionRequestViewModel model)
        {
            if (model.StartStationId == model.EndStationId)
            {
                ModelState.AddModelError(string.Empty, "Departure and destination stations cannot be the same.");
            }

            if (!ModelState.IsValid)
            {
                model.Stations = await _context.Stations
                    .Select(s => new SelectListItem { Value = s.Id.ToString(), Text = $"{s.Name} (Line {s.LineNumber})" })
                    .ToListAsync();
                return View(model);
            }

            var userId = _userManager.GetUserId(User);

            // Upload Basic Files
            string personalPhotoPath = await UploadDocumentAsync(model.PersonalPhoto);
            string nationalIdPath = await UploadDocumentAsync(model.NationalIdPhoto);

            // Upload Category Specific Files
            string? hrLetterPath = model.HrLetter != null ? await UploadDocumentAsync(model.HrLetter) : null;
            string? studentProofPath = model.StudentProofDocument != null ? await UploadDocumentAsync(model.StudentProofDocument) : null;

            // Pricing Logic
            var subType = model.UserCategory == "Student" ? SubscriptionType.Student : SubscriptionType.General;
            decimal price = CalculatePrice(model.StartStationId, model.EndStationId, subType);

            var subscription = new Subscription
            {
                UserId = userId,
                StartStationId = model.StartStationId,
                EndStationId = model.EndStationId,
                IsFirstTime = model.IsFirstTime,
                UserCategory = model.UserCategory,
                EmployerName = model.EmployerName,
                HrLetterPath = hrLetterPath,
                IsUniversityStudent = model.UserCategory == "Student" ? model.IsUniversityStudent : null,
                SchoolOrUniversityName = model.SchoolOrUniversityName,
                StudentProofPath = studentProofPath,
                PersonalPhotoPath = personalPhotoPath,
                NationalIdPhotoPath = nationalIdPath,
                Type = subType,
                TotalPrice = price,
                Status = SubscriptionStatus.Pending,
                RequestDate = DateTime.Now
            };

            _context.Subscriptions.Add(subscription);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(MySubscriptions));
        }
        private async Task<string> UploadDocumentAsync(IFormFile file)
        {
            string uploadsFolder = Path.Combine(_environment.ContentRootPath, "PrivateUploads");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            string uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return uniqueFileName; 
        }
        [HttpGet]
        public async Task<IActionResult> ViewDocument(int subscriptionId, string type)
        {
            var subscription = await _context.Subscriptions.FindAsync(subscriptionId);
            if (subscription == null) return NotFound();

            var currentUserId = _userManager.GetUserId(User);
            if (subscription.UserId != currentUserId && !User.IsInRole("Admin"))
                return Forbid();

            string fileName = type switch
            {
                "nationalId" => subscription.NationalIdPhotoPath,
                "personalPhoto" => subscription.PersonalPhotoPath,
                _ => null
            };

            if (fileName == null) return NotFound();

            string filePath = Path.Combine(_environment.ContentRootPath, "PrivateUploads", fileName);
            if (!System.IO.File.Exists(filePath)) return NotFound();

            var contentType = "image/png"; 
            return PhysicalFile(filePath, contentType);
        }
    }
}
