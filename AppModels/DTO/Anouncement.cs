using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppModels.DTO
{
    public class Anouncement
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime DateAdded { get; set; }
        public byte[] Banner { get; set; }

        public virtual ICollection<AnouncementSub> Subs { get; set; }
    }
}
