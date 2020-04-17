using System;
using System.Threading.Tasks;
using AppDataAccess.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppUi.Pages.Admin.Children
{
    [Authorize(Roles = "Administrator")]
    public class RemoveFeatureModel : PageModel
    {
        private readonly IChildServices _childData;

        public RemoveFeatureModel(IChildServices childServices)
        {
            _childData = childServices;
        }

        [BindProperty(SupportsGet = true)]
        public Guid ChildId { get; set; }
        [BindProperty(SupportsGet = true)]
        public Guid FeatureId { get; set; }

        public async Task<IActionResult> OnGet()
        {
            if (ChildId == Guid.Empty || FeatureId == Guid.Empty) 
            {
                TempData["Warning"] = "No child Id and feature Id provided";
                return RedirectToPage("/Admin/Children/Details", new { Id = ChildId });
            }

            var response = await _childData.RemoveFeature(ChildId, FeatureId);

            if (response)
                TempData["Success"] = "Removing feature was successful";
            else
                TempData["Failed"] = "The selected feature does not exist in child's list";

            return RedirectToPage("/Admin/Children/Details", new { Id = ChildId });
        }
    }
}