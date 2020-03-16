using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppModels.DTO
{
    public class Child
    {
        [Key]
        public Guid Id { get; set; }
        public string GivingName { get; set; }
        public string  Othernames { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public string Gender { get; set; }
        public string Teaser { get; set; }
        public byte[] Phtotograph { get; set; }
        public DateTime DateAdded { get; set; }

        public virtual ChildClass Class { get; set; }
        public virtual ChildSponser Sponser { get; set; }
        public virtual ICollection<SemesterRegistration> Registrations { get; set; }
        public virtual ICollection<ChildFeature> Features { get; set; }
        public virtual ICollection<Letter> Letters { get; set; }

        // calculated fields
        public string Fullname 
        { 
            get 
            {
                return GivingName + " " + Othernames + " " + Surname;
            } 
        }
    }
}
