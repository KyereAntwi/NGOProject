using System.Threading.Tasks;
using AppDataAccess.Repositories;
using AppModels.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppUi.Pages.Volunteer
{
    public class ChildrenModel : PageModel
    {
        private readonly IChildServices _childData;

        public ChildrenModel(IChildServices childServices)
        {
            _childData = childServices;
        }

        public AllChildrenViewModel ViewModel { get; set; }

        public async Task<IActionResult> OnGet()
        {
            ViewModel = new AllChildrenViewModel
            {
                Children = await _childData.GetUnSupportedChildrenAsync()
            };
            return Page();
        }
    }
}
