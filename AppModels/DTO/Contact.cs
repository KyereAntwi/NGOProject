using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppModels.DTO
{
    public class Contact
    {
        [Key]
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Street { get; set; }
        public string Country { get; set; }
        public string Zipcode { get; set; }
        public string Telephone { get; set; }

        public Guid UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
