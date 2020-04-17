using AppModels.DTO;
using System;
using System.Collections.Generic;

namespace AppModels.ViewModels
{
    public class SemesterRegistrationViewModel
    {
        public List<Semester> Semesters { get; set; }

        public Guid ChildId { get; set; }
        public Guid SemesterId { get; set; }
    }
}
