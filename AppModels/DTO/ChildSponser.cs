using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppModels.DTO
{
    public class ChildSponser
    {
        [Key]
        public Guid ChildId { get; set; }
        [ForeignKey(nameof(ChildId))]
        public Child Child { get; set; }

        public Guid ApplicationUserId { get; set; }
        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
