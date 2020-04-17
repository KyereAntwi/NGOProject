using System;
using System.Threading.Tasks;
using AppDataAccess.Repositories;
using AppModels.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppUi.Pages
{
    public class BlogModel : PageModel
    {
        private readonly IBlogServices _blogService;

        public BlogModel(IBlogServices blogServices)
        {
            _blogService = blogServices;
        }

        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }
        public Blog ViewModel { get; set; }

        public async Task<IActionResult> OnGet()
        {
            if (Id == Guid.Empty)
                return RedirectToPage("/Blogs");

            ViewModel = await _blogService.GetBlogAsync(Id);
            return Page();
        }
    }
}
