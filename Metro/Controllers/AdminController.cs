using MetroApp.Models;
using MetroApp.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace MetroApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var requests = await _context.Subscriptions
                .Include(s => s.User)
                .Include(s => s.StartStation)
                .Include(s => s.EndStation)
                .OrderByDescending(s => s.RequestDate)
                .ToListAsync();

            return View(requests);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(int id, SubscriptionStatus status)
        {
            var subscription = await _context.Subscriptions.FindAsync(id);
            if (subscription == null)
                return NotFound();

            subscription.Status = status;

            if (status == SubscriptionStatus.Approved)
            {
                subscription.StartDate = DateTime.Now;
                subscription.EndDate = DateTime.Now.AddMonths(3); // 3-month subscription period
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
