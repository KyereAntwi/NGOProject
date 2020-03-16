using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppModels.DTO
{
    public class ApplicationUser
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Nationality { get; set; }
        public byte[] Photograph { get; set; }
        public DateTime DateAdded { get; set; }

        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public IdentityUser MyProperty { get; set; }

        public virtual ICollection<ChildSponser> Children { get; set; }
        public virtual PrayerVolunteer PrayerVolunteer { get; set; }
        public virtual ICollection<Letter> Letters { get; set; }
    }
}
