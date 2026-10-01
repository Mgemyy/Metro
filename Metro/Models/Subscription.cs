using MetroApp.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MetroApp.Models
{
    public class Subscription
    {
        public bool IsFirstTime { get; set; }
        public string UserCategory { get; set; }
        public string? EmployerName { get; set; }
        public string? HrLetterPath { get; set; }
        public bool? IsUniversityStudent { get; set; }
        public string? SchoolOrUniversityName { get; set; }
        public string? StudentProofPath { get; set; } 

        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        [Required]
        public int StartStationId { get; set; }

        [ForeignKey("StartStationId")]
        public Station StartStation { get; set; }

        [Required]
        public int EndStationId { get; set; }

        [ForeignKey("EndStationId")]
        public Station EndStation { get; set; }

        public SubscriptionType Type { get; set; }
        public SubscriptionStatus Status { get; set; } = SubscriptionStatus.Pending;

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }

        public DateTime RequestDate { get; set; } = DateTime.Now;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        [Required]
        public string PersonalPhotoPath { get; set; }

        [Required]
        public string NationalIdPhotoPath { get; set; }

        // Nullable: Only required for specific subscription types like Students
        public string? AdditionalDocumentPath { get; set; }
        public string? ReviewedByAdminId { get; set; }

        [ForeignKey("ReviewedByAdminId")]
        public virtual ApplicationUser? ReviewedByAdmin { get; set; }

        public DateTime? ReviewedAt { get; set; }
        public string? AdminNotes { get; set; }
    }
}
