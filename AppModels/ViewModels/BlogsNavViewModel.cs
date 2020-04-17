using AppModels.DTO;
using System.Collections.Generic;

namespace AppModels.ViewModels
{
    public class BlogsNavViewModel
    {
        public List<Blog> Blogs { get; set; }
        public int Total { get; set; }
    }
}
