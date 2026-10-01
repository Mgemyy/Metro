using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MetroApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        [StringLength(14, MinimumLength = 14)]
        public string NationalId { get; set; }
        public string? ProfilePicturePath { get; set; }

        public ICollection<Subscription> Subscriptions { get; set; }
    }
}

