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
    public class UpdateFeatureModel : PageModel
    {
        private readonly IChildServices _childData;

        public UpdateFeatureModel(IChildServices childServices)
        {
            _childData = childServices;
        }

        [BindProperty()]
        public UpdateChildFeatureViewModel ViewModel { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            ChildFeature childFeature = new ChildFeature 
            {
                ChildId = ViewModel.ChildId,
                FeatureId = ViewModel.FeatureID
            };


            bool response = await _childData.AddFeature(childFeature);

            if (response)
                TempData["Success"] = "Feature added successfully";

            return RedirectToPage("/Admin/Children/Details", new { Id = ViewModel.ChildId });
        }
    }
}