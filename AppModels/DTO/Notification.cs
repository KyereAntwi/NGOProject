using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppModels.DTO
{
    public class Notification
    {
        [Key]
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime DateTimeAdded { get; set; }
        public bool Seen { get; set; }

        public Guid UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }
    }
}
