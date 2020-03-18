using AppModels.DTO;
using System.Collections.Generic;

namespace AppModels.ViewModels
{
    public class SearchChildViewModel
    {
        public string KeyString { get; set; }
        public List<Child> Children { get; set; }
    }
}
