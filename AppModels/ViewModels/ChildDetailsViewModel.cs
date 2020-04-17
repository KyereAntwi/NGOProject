using AppModels.DTO;
using System.Collections.Generic;

namespace AppModels.ViewModels
{
    public class ChildDetailsViewModel
    {
        public Child Child { get; set; }
        public List<Letter> Letters { get; set; }
        public List<SemesterRegistration> Registrations { get; set; }
        public List<Feature> Features { get; set; }
        public ApplicationUser Sponser { get; set; }
        public Class Class { get; set; }
    }
}
