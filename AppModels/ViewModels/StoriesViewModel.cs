using AppModels.DTO;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppModels.ViewModels
{
    public class StoriesViewModel
    {
        public List<Anouncement> Anouncements { get; set; }

        [Required]
        public string Title { get; set; }
        public IFormFile Banner { get; set; }
    }
}
