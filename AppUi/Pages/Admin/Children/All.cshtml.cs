using System.Collections.Generic;
using System.Threading.Tasks;
using AppDataAccess.Repositories;
using AppModels.DTO;
using AppModels.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppUi.Pages.Admin.Children
{
    [Authorize(Roles = "Administrator")]
    public class AllModel : PageModel
    {
        private readonly IChildServices _child;

        public AllModel(IChildServices childServices)
        {
            _child = childServices;
        }

        public AllChildrenViewModel ViewModel { get; set; }

        public async Task<IActionResult> OnGet()
        {
            ViewData["Title"] = "All Children";
            ViewModel = new AllChildrenViewModel();
            ViewModel.Children = new List<Child>();

            var response = await _child.GetAllChildrenAsync();

            if (response.Count > 0)
                ViewModel.Children = response;

            return Page();
        }
    }
}
