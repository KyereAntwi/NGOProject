using AppModels.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppModels.ViewModels
{
    public class UpdateChildFeatureViewModel
    {
        public List<Feature> Features { get; set; }

        [Required]
        public Guid ChildId { get; set; }
        [Required]
        public Guid FeatureID { get; set; }
    }
}
