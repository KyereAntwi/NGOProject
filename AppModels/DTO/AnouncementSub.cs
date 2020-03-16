using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppModels.DTO
{
    public class AnouncementSub
    {
        [Key]
        public Guid Id { get; set; }
        public string Details { get; set; }

        public Guid AnouncementId { get; set; }
        [ForeignKey(nameof(AnouncementId))]
        public Anouncement Anouncement { get; set; }
    }
}
