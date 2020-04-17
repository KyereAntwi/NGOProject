using System;
using System.Threading.Tasks;
using AppDataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppUi.Pages
{
    public class DeleteBlogModel : PageModel
    {
        private readonly IBlogServices _blogService;

        public DeleteBlogModel(IBlogServices blogServices)
        {
            _blogService = blogServices;
        }

        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        public async Task<IActionResult> OnGet()
        {
            _ = await _blogService.DeleteBlogAsync(Id);
            TempData["Success"] = "You have successfully deleted the Blog";
            return RedirectToPage("/Admin/Blogs");
        }
    }
}
