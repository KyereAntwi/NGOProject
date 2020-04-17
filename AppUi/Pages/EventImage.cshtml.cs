using System;
using System.Threading.Tasks;
using AppDataAccess.Repositories;
using AppModels.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppUi.Pages
{
    public class EventImageModel : PageModel
    {
        private readonly IEventsServices _eventData;

        public EventImageModel(IEventsServices eventsServices)
        {
            _eventData = eventsServices;
        }

        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        public async Task<FileContentResult> OnGet()
        {
            if (Id == Guid.Empty)
                return null;

            Event user = await _eventData.GetEventAsync(Id);

            if (user == null)
                return null;

            return File(user.Banner, "image/jpeg");
        }
    }
}