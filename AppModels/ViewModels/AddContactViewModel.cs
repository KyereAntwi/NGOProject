using System;
using System.ComponentModel.DataAnnotations;

namespace AppModels.ViewModels
{
    public class AddContactViewModel
    {
        [Required]
        public string Type { get; set; }
        [Required]
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string Zipcode { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Telephone { get; set; }
        public string UserId { get; set; }
    }
}
