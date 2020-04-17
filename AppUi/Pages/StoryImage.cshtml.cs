using System;
using System.Threading.Tasks;
using AppDataAccess.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppUi.Pages
{
    [Authorize()]
    public class StoryImageModel : PageModel
    {
        private readonly IStoriesServies _storyData;

        public StoryImageModel(IStoriesServies storiesServies)
        {
            _storyData = storiesServies;
        }

        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        public async Task<FileContentResult> OnGet()
        {
            var story = await _storyData.GetAnouncementAsync(Id);
            return File(story.Banner, "image/jpeg");
        }
    }
}
