using System.Threading.Tasks;
using AppDataAccess.Repositories;
using AppModels.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppUi.Pages
{
    public class StoriesModel : PageModel
    {
        private readonly IStoriesServies _storyData;

        public StoriesModel(IStoriesServies storiesServies)
        {
            _storyData = storiesServies;
        }

        public StoriesViewModel ViewModel { get; set; }

        public async Task<IActionResult> OnGet()
        {
            ViewModel = new StoriesViewModel
            {
                Anouncements = await _storyData.GetAnouncementsAsync()
            };

            return Page();
        }
    }
}
