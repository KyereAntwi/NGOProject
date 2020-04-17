using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AppDataAccess.Repositories;
using AppModels.DTO;
using AppModels.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppUi.Pages.Admin
{
    public class BlogsModel : PageModel
    {
        private readonly IBlogServices _blogService;

        public BlogsModel(IBlogServices blogServices)
        {
            _blogService = blogServices;
        }

        [BindProperty()]
        public BlogsViewModel ViewModel { get; set; }

        public async Task<IActionResult> OnGet()
        {
            ViewModel = new BlogsViewModel();
            ViewModel.Blogs = new List<Blog>();
            ViewModel.Blogs = await _blogService.BlogsAsync();
            return Page();
        }

        public async Task<IActionResult> OnPost() 
        {
            if (!ModelState.IsValid) 
            {
                TempData["Warning"] = "The values entered for blog are not correct, try again";
                return RedirectToPage();
            }

            Blog blog = new Blog 
            {
                Title = ViewModel.Title,
                Introduction = ViewModel.Introduction,
                DateAdded = DateTime.Now
            };

            // process file upload
            if (ViewModel.Banner != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await ViewModel.Banner.CopyToAsync(memoryStream);
                    blog.Banner = memoryStream.ToArray();
                }
            }

            _ = await _blogService.AddBlogAsync(blog);
            TempData["Success"] = "Blog added successfully";
            return RedirectToPage();
        }
    }
}
