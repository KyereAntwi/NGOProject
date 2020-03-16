using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppModels.DTO
{
    public class ChildClass
    {
        [Key]
        public Guid ChildId { get; set; }
        [ForeignKey(nameof(ChildId))]
        public Child Child { get; set; }

        public Guid ClassId { get; set; }
        [ForeignKey(nameof(ClassId))]
        public Class Class { get; set; }
    }
}
