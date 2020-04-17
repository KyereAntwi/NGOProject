using System.Collections.Generic;
using System.Threading.Tasks;
using AppDataAccess.Repositories;
using AppModels.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppUi.Pages.Admin
{
    public class AllVisitsModel : PageModel
    {
        private readonly IVisitationServices _visitData;

        public AllVisitsModel(IVisitationServices visitationServices)
        {
            _visitData = visitationServices;
        }

        public List<Visitation> Visitations { get; set; }

        public async Task<IActionResult> OnGet()
        {
            Visitations = await _visitData.GetVisitationsAsync();
            return Page();
        }
    }
}