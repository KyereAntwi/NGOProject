using AppModels.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AppUi.ViewComponents
{
    public class SearchChild : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync() 
        {
            SearchChildViewModel viewModel = new SearchChildViewModel();
            return View(viewModel);
        }
    }
}
