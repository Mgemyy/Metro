using MetroApp.Models.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MetroApp.ViewModels
{
    public class SubscriptionRequestViewModel
    {
        [Required(ErrorMessage = "Please select a departure station.")]
        [Display(Name = "Departure Station")]
        public int StartStationId { get; set; }

        [Required(ErrorMessage = "Please select a destination station.")]
        [Display(Name = "Destination Station")]
        public int EndStationId { get; set; }

        [Required]
        [Display(Name = "Application Type")]
        public bool IsFirstTime { get; set; } = true;

        [Required(ErrorMessage = "National ID is required.")]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "National ID must be exactly 14 digits.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "National ID must contain numbers only.")]
        [Display(Name = "National ID")]
        public string NationalId { get; set; }

        [Required(ErrorMessage = "Please upload a personal photo.")]
        [Display(Name = "Personal Photo (White Background)")]
        public IFormFile PersonalPhoto { get; set; }

        [Required(ErrorMessage = "Please upload a National ID copy.")]
        [Display(Name = "National ID Card Copy")]
        public IFormFile NationalIdPhoto { get; set; }

        [Required(ErrorMessage = "Please specify your occupation/status.")]
        [Display(Name = "Category")]
        public string UserCategory { get; set; } // "General", "Employee", "Student"

        // Employee specific
        [Display(Name = "Employer / Organization Name")]
        public string? EmployerName { get; set; }

        [Display(Name = "Employment Proof (HR Letter / Work ID)")]
        public IFormFile? HrLetter { get; set; }

        // Student specific
        [Display(Name = "University Student?")]
        public bool IsUniversityStudent { get; set; }

        [Display(Name = "School or University Name")]
        public string? SchoolOrUniversityName { get; set; }

        [Display(Name = "Enrollment Proof Document")]
        public IFormFile? StudentProofDocument { get; set; }

        public IEnumerable<SelectListItem>? Stations { get; set; }
    }
}