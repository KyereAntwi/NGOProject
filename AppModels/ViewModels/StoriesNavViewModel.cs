using AppModels.DTO;
using System.Collections.Generic;

namespace AppModels.ViewModels
{
    public class StoriesNavViewModel
    {
        public List<Anouncement> Anouncements { get; set; }
        public int Total { get; set; }
    }
}
