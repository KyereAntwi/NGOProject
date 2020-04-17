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
    public class StoryModel : PageModel
    {
        private readonly IStoriesServies _storiesServices;

        public StoryModel(IStoriesServies storiesServies)
        {
            _storiesServices = storiesServies;
        }

        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }
        [BindProperty()]
        public StoryDetailsViewModel ViewModel { get; set; }

        public async Task<IActionResult> OnGet()
        {
            await GetData();
            return Page();
        }

        public async Task<IActionResult> OnPost() 
        {
            if (!ModelState.IsValid) 
            {
                TempData["Warning"] = "The values entered for sub is not correct, try again";
                return RedirectToPage();
            }

            var response = await _storiesServices.AddAnouncementSubAsync(new AnouncementSub 
            {
                Details = ViewModel.SubDetails,
                AnouncementId = Id
            });

            TempData["Success"] = "Sub Detail added successfully";

            await GetData();

            return Page();
        }

        async Task GetData() 
        {
            ViewModel = new StoryDetailsViewModel();
            ViewModel.Anouncement = new Anouncement();
            ViewModel.Anouncement = await _storiesServices.GetAnouncementAsync(Id);
        }
    }
}
