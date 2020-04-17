using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace AppModels.ViewModels
{
    public class NewChildViewModel
    {
        [Display(Name = "First Name")]
        [Required]
        [StringLength(15)]
        public string GivingName { get; set; }

        [StringLength(15)]
        [Display(Name = "Other Names")]
        public string Othernames { get; set; }

        [Required]
        [StringLength(15)]
        [Display(Name = "Surname")]
        public string Surname { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Display(Name = "Nationality")]
        public string Nationality { get; set; }
        
        [Required]
        public string Gender { get; set; }
        
        [Required]
        [DataType(DataType.MultilineText)]
        public string Teaser { get; set; }

        [Display(Name = "Photograph")]
        public IFormFile Photograph { get; set; }
    }
}
