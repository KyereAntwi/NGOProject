using AppDataAccess.Repositories;
using AppModels.DTO;
using AppModels.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace AppUi.Pages.Admin
{
    public class EventsModel : PageModel
    {
        private readonly IEventsServices _eventData;
        public EventsModel(IEventsServices eventsServices)
        {
            _eventData = eventsServices;
        }

        [BindProperty()]
        public EventsViewModel ViewModel { get; set; }

        public async Task<IActionResult> OnGet()
        {
            ViewModel = new EventsViewModel();
            ViewModel.Events = new List<Event>();
            ViewModel.Events = await _eventData.GetEventsAsync();

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {

            Event newEvent = new Event
            {
                Theme = ViewModel.Theme,
                DateAdded = DateTime.Now,
                StartingDate = ViewModel.StartingDate.Date,
                EndingDate = ViewModel.EndingDate.Date,
                StartingTime = ViewModel.StartingTime,
                Details = ViewModel.Details,
            };

            // process file upload
            if (ViewModel.Banner != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await ViewModel.Banner.CopyToAsync(memoryStream);
                    newEvent.Banner = memoryStream.ToArray();
                }
            }

            _ = await _eventData.AddEventAsync(newEvent);
            return RedirectToPage();
        }
    }
}