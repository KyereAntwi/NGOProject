using System;
using System.Threading.Tasks;
using AppDataAccess.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppUi.Pages.Admin
{
    [Authorize(Roles = "Administrator")]
    public class DeleteEventModel : PageModel
    {
        private readonly IEventsServices _eventData;

        public DeleteEventModel(IEventsServices eventsServices)
        {
            _eventData = eventsServices;
        }

        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        public async Task<IActionResult> OnGet()
        {
            _= await _eventData.DeleteEventAsync(Id);

            TempData["Success"] = "Event was deleted successfully";
            return RedirectToPage("/Admin/Events");
        }
    }
}
