using AppModels.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppModels.ViewModels
{
    public class SemestersViewModel
    {
        public List<Semester> Semesters { get; set; }

        [Required]
        public Guid SemId { get; set; }
    }
}
