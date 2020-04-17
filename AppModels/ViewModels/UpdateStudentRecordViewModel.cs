using AppModels.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppModels.ViewModels
{
    public class UpdateStudentRecordViewModel
    {
        public List<Semester> Semesters { get; set; }
        public List<Subject> Subjects { get; set; }

        [Required]
        public Guid ChildId { get; set; }
        [Required]
        public Guid SemesterId { get; set; }
        [Required]
        public Guid SubjectId { get; set; }
        [Required]
        public double ClassScore { get; set; }
        [Required]
        public double ExamScore { get; set; }
    }
}
