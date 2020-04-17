using System.Threading.Tasks;
using AppDataAccess.Repositories;
using AppModels.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppUi.Pages
{
    public class BlogsModel : PageModel
    {
        private readonly IBlogServices _blogServices;

        public BlogsModel(IBlogServices blogServices)
        {
            _blogServices = blogServices;
        }

        public BlogsViewModel ViewModel { get; set; }

        public async Task<IActionResult> OnGet()
        {
            ViewModel = new BlogsViewModel
            {
                Blogs = await _blogServices.BlogsAsync()
            };

            return Page();
        }
    }
}
