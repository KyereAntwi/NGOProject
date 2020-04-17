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
    public class StoriesModel : PageModel
    {
        private readonly IStoriesServies _data;

        public StoriesModel(IStoriesServies storiesServies)
        {
            _data = storiesServies;
        }

        [BindProperty()]
        public StoriesViewModel ViewModel { get; set; }

        public async Task<IActionResult> OnGet()
        {
            ViewModel = new StoriesViewModel();
            ViewModel.Anouncements = new List<Anouncement>();

            ViewModel.Anouncements = await _data.GetAnouncementsAsync();

            return Page();
        }

        public async Task<IActionResult> OnPost() 
        {
            if (!ModelState.IsValid)
                return RedirectToPage();

            var newAnouncement = new Anouncement
            {
                Title = ViewModel.Title,
                DateAdded = DateTime.Now
            };

            // process file upload
            if (ViewModel.Banner != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await ViewModel.Banner.CopyToAsync(memoryStream);
                    newAnouncement.Banner = memoryStream.ToArray();
                }
            }

             _= await _data.AddAnouncementAsync(newAnouncement);
            return RedirectToPage();
        }
    }
}
