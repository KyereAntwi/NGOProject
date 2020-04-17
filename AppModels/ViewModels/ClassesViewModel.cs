using AppModels.DTO;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppModels.ViewModels
{
    public class ClassesViewModel
    {
        public List<Class> Classes { get; set; }

        [Required]
        public string  Name { get; set; }

    }
}
