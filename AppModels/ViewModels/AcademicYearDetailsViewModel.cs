using AppModels.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppModels.ViewModels
{
    public class AcademicYearDetailsViewModel
    {
        public AcademicYear Year { get; set; }
        public List<Semester> Semesters { get; set; }


        [Required]
        [StringLength(6, MinimumLength = 5)]
        [Display(Name = "Semester Name")]
        public string SemesterName { get; set; }
    }
}
