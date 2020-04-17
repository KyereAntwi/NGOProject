using System.Threading.Tasks;
using AppDataAccess.Repositories;
using AppModels.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppUi.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IChildServices _childData;

        public IndexModel( IChildServices childServices)
        {
            _childData = childServices;
        }

        public IndexViewModel ViewModel { get; set; }

        public async Task OnGet()
        {
            ViewModel = new IndexViewModel();
            ViewModel.Children = await _childData.GetRandomChildrenAsync();
        }
    }
}
