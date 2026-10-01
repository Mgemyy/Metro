using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MetroApp.Models
{
    public class Station
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public int LineNumber { get; set; } 

        public ICollection<Subscription> StartSubscriptions { get; set; }
        public ICollection<Subscription> EndSubscriptions { get; set; }
    }
}
