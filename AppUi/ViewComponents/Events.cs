using AppDataAccess.Repositories;
using AppModels.DTO;
using AppModels.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppUi.ViewComponents
{
    public class Events : ViewComponent
    {
        private readonly IEventsServices _eventData;

        public Events(IEventsServices eventsServices)
        {
            _eventData = eventsServices;
        }

        public async Task<IViewComponentResult> InvokeAsync() 
        {
            EventsViewModel ViewModel = new EventsViewModel();
            ViewModel.Events = new List<Event>();
            ViewModel.Events = await _eventData.GetEventsAsync();

            return View(ViewModel);
        }
    }
}
