using AppModels.DTO;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppModels.ViewModels
{
    public class BlogsViewModel
    {
        public List<Blog> Blogs { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string Introduction { get; set; }
        public IFormFile Banner { get; set; }
    }
}
