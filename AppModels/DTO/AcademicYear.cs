using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AppModels.DTO
{
    public class AcademicYear
    {
        [Key]
        public Guid Id { get; set; }
        public string Year { get; set; }
        public DateTime DateAdded { get; set; }

        public virtual ICollection<Semester> Semesters { get; set; }
    }
}
