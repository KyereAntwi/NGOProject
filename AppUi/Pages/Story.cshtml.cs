using System;
using System.Threading.Tasks;
using AppDataAccess.Repositories;
using AppModels.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppUi.Pages
{
    public class StoryModel : PageModel
    {
        private readonly IStoriesServies _storyData;

        public StoryModel(IStoriesServies storiesServies)
        {
            _storyData = storiesServies;
        }

        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }
        public Anouncement ViewModel { get; set; }

        public async Task<IActionResult> OnGet()
        {
            ViewModel = await _storyData.GetAnouncementAsync(Id);
            return Page();
        }
    }
}
