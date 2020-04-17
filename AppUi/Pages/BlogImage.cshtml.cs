using System;
using System.Threading.Tasks;
using AppDataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppUi.Pages
{
    public class BlogImageModel : PageModel
    {
        private readonly IBlogServices _blogData;

        public BlogImageModel(IBlogServices blogServices)
        {
            _blogData = blogServices;
        }

        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        public async Task<FileContentResult> OnGet()
        {
            var blog = await _blogData.GetBlogAsync(Id);
            return File(blog.Banner, "image/jpeg");
        }
    }
}
