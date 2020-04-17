using System.Threading.Tasks;
using AppDataAccess.Repositories;
using AppModels.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppUi.Pages.Admin
{
    [Authorize(Roles = "Administrator")]
    public class IndexModel : PageModel
    {
        private readonly IChildServices _childData;
        private readonly IVisitationServices _visitData;
        private readonly ILettersServices _letterData;

        public IndexModel(IChildServices childServices, 
                          IVisitationServices visitationServices, 
                          ILettersServices lettersServices)
        {
            _childData = childServices;
            _visitData = visitationServices;
            _letterData = lettersServices;
        }


        public AdminViewModel ViewModel { get; set; }

        public async Task<IActionResult> OnGet()
        {
            ViewModel = new AdminViewModel()
            {
                Requests = await _childData.GetAllUnActivatedSupportAsync(),
                Visitations = await _visitData.GetPendingVisitationsAsync(),
                Letters = await _letterData.GetUnReadLettersAsync()
            };

            return Page();
        }
    }
}
