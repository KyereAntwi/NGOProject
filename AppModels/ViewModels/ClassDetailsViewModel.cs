using AppModels.DTO;
using System.Collections.Generic;

namespace AppModels.ViewModels
{
    public class ClassDetailsViewModel
    {
        public Class Class { get; set; }
        public List<Subject> BelongingSubjects { get; set; }
        public List<Child> Children { get; set; }
        public List<Subject> NonBelongingSubjects { get; set; }
    }
}
