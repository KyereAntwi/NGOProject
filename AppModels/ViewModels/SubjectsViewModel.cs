using AppModels.DTO;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppModels.ViewModels
{
    public class SubjectsViewModel
    {
        public List<Subject> Subjects { get; set; }
        
        [Required]
        public string Name { get; set; }
        [Required]
        public string Type { get; set; }
    }
}
