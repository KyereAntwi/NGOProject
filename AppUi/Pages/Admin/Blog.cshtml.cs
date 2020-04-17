using System;
using System.Threading.Tasks;
using AppDataAccess.Repositories;
using AppModels.DTO;
using AppModels.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppUi.Pages.Admin
{
    [Authorize(Roles = "Administrator")]
    public class BlogModel : PageModel
    {
        private readonly IBlogServices _blogService;

        public BlogModel(IBlogServices blogServices)
        {
            _blogService = blogServices;
        }

        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }
        public BlogDetailsViewModel ViewModel { get; set; }

        public async Task<IActionResult> OnGet()
        {
            if (Id == Guid.Empty) 
            {
                TempData["Warning"] = "An Id for selected Blog was not provided";
                return RedirectToPage("/Admin/Blogs");
            }

            var blog = await _blogService.GetBlogAsync(Id);
            ViewModel = new BlogDetailsViewModel();
            ViewModel.Blog = new Blog();
            ViewModel.Blog = blog;

            return Page();
        }

        public async Task<IActionResult> OnPost() 
        {
            BlogSub blogSub = new BlogSub()
            {
                Subtitle = ViewModel.SubTitle,
                Details = ViewModel.SubDetails,
                BlogId = Id
            };

            _ = await _blogService.AddBlogSubAsync(blogSub);
            return RedirectToPage();
        }
    }
}
