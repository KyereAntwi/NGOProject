using AppModels.DTO;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppModels.ViewModels
{
    public class FeaturesViewModel
    {
        public List<Feature> Features { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public bool Disability { get; set; }
    }
}
