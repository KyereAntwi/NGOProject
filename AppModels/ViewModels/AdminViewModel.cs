using AppModels.DTO;
using System.Collections.Generic;

namespace AppModels.ViewModels
{
    public class AdminViewModel
    {
        public List<ChildSponser> Requests { get; set; }
        public List<Visitation> Visitations { get; set; }
        public List<Letter> Letters { get; set; }
    }
}
