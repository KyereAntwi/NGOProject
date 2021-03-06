﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        public virtual ICollection<ChildSponser> Children { get; set; }
        public virtual ICollection<Visitation> Visitations { get; set; }
        public virtual PrayerVolunteer PrayerVolunteer { get; set; }
        public virtual ICollection<Letter> Letters { get; set; }
        public virtual ICollection<TimeLine> TimeLines { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
    }
}
