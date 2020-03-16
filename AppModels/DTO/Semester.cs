using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppModels.DTO
{
    public class Semester
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime DateAdded { get; set; }


        public Guid AcademicYearId { get; set; }
        [ForeignKey(nameof(AcademicYearId))]
        public AcademicYear AcademicYear { get; set; }

        public virtual ICollection<SemesterRegistration> Registrations { get; set; }
    }
}
