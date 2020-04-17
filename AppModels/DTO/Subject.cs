using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppModels.DTO
{
    public class Subject
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateAdded { get; set; }
        public string Type { get; set; }

        public virtual ICollection<ClassSubject> Classes { get; set; }
        public virtual ICollection<SemesterRegistration> Registrations { get; set; }
    }
}
