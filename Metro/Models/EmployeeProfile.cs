using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MetroApp.Models
{
    public class EmployeeProfile
    {
        [Key, ForeignKey("User")]
        public string UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string EmployeeCode { get; set; }

        [Required]
        [StringLength(100)]
        public string Department { get; set; }

        public string OfficeLocation { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
