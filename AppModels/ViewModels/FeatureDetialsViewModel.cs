using AppModels.DTO;
using System.Collections.Generic;

namespace AppModels.ViewModels
{
    public class FeatureDetialsViewModel
    {
        public Feature Feature{ get; set; }
        public List<Child> Children { get; set; }
    }
}
