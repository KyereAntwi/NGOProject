using System;
using System.Threading.Tasks;
using AppDataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppUi.Pages.Admin.Children
{
    public class ActivateSupportModel : PageModel
    {
        private readonly IChildServices _childData;

        public ActivateSupportModel(IChildServices childServices)
        {
            _childData = childServices;
        }

        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        public async Task<IActionResult> OnGet()
        {
            _ = await _childData.ActivateSupportAsync(Id);
            TempData["Success"] = "Activation was successful";
            return RedirectToPage("/Admin/Index");
        }
    }
}