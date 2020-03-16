using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppModels.DTO
{
    public class PrayerVolunteer
    {
        [Key]
        public Guid ApplicationUserId { get; set; }
        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser ApplicationUser { get; set; }

        public DateTime DateAdded { get; set; }
    }
}
