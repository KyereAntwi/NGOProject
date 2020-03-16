using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppModels.DTO
{
    public class ClassSubject
    {
        [Key]
        public Guid Id { get; set; }

        public Guid ClassId { get; set; }
        [ForeignKey(nameof(ClassId))]
        public Class Class { get; set; }

        public Guid SubjectId { get; set; }
        [ForeignKey(nameof(SubjectId))]
        public Subject Subject { get; set; }
    }
}
