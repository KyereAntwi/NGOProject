using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace AppModels.ViewModels
{
    public class NewStaffViewModel
    {
        [DataType(DataType.EmailAddress)]
        [Display(Name = "User Name or Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Full name")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Required]
        [Display(Name = "Nationality")]
        public string Nationality { get; set; }

        [DataType(DataType.PhoneNumber)]
        [StringLength(13, MinimumLength = 10)]
        [Display(Name = "Mobile Number")]
        public string Phone { get; set; }

        public IFormFile Photograph { get; set; }
    }
}
