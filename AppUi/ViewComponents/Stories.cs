using AppDataAccess.Repositories;
using AppModels.DTO;
using AppModels.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppUi.ViewComponents
{
    public class Stories : ViewComponent
    {
        private readonly IStoriesServies _storyData;

        public Stories(IStoriesServies storiesServies)
        {
            _storyData = storiesServies;
        }

        public async Task<IViewComponentResult> InvokeAsync() 
        {
            var stories = await _storyData.GetAnouncementsAsync();
            StoriesNavViewModel viewModel = new StoriesNavViewModel();
            viewModel.Anouncements = new List<Anouncement>();

            int count = 1;
            foreach (var item in stories) 
            {
                if (count <= 4) 
                {
                    viewModel.Anouncements.Add(item);
                }

                count++;
            }

            viewModel.Total = stories.Count;
            return View(viewModel);
        }
    }
}
