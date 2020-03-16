using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppModels.DTO
{
    public class ChildFeature
    {
        [Key]
        public Guid Id { get; set; }

        public Guid FeatureId { get; set; }
        [ForeignKey(nameof(FeatureId))]
        public Feature Feature { get; set; }

        public Guid ChildId { get; set; }
        [ForeignKey(nameof(ChildId))]
        public Child Child { get; set; }
    }
}
