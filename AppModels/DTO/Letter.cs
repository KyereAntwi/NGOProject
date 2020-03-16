using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppModels.DTO
{
    public class Letter
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public bool Seen { get; set; }
        public DateTime DateWritten { get; set; }

        public Guid ApplicationUserId { get; set; }
        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser ApplicationUser { get; set; }

        public Guid ChildId { get; set; }
        [ForeignKey(nameof(ChildId))]
        public Child Child { get; set; }
    }
}
