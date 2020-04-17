using System;
using System.Threading.Tasks;
using AppDataAccess.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppUi.Pages
{
    [Authorize(Roles = "Administrator")]
    public class DeleteStoryModel : PageModel
    {
        private readonly IStoriesServies _storyServices;

        public DeleteStoryModel(IStoriesServies storiesServies)
        {
            _storyServices = storiesServies;
        }

        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        public async Task<IActionResult> OnGet()
        {
            _ = await _storyServices.DeleteAnouncementAsync(Id);
            return RedirectToPage("/Admin/Stories");
        }
    }
}
