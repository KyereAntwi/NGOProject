using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppModels.DTO
{
    public class Class
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateAdded { get; set; }

        public virtual ICollection<ChildClass> Students { get; set; }
        public virtual ICollection<ClassSubject> Subjects { get; set; }
    }
}
