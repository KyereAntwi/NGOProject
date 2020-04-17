using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppDataAccess.Repositories;
using AppModels.DTO;
using AppModels.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppUi.Pages.Admin
{
    [Authorize(Roles = "Administrator")]
    public class FeatureModel : PageModel
    {
        private readonly IFeaturesServices _featureService;

        public FeatureModel(IFeaturesServices featuresServices)
        {
            _featureService = featuresServices;
        }

        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }
        public FeatureDetialsViewModel ViewModel { get; set; }

        public async Task<IActionResult> OnGet()
        {
            if (Id == Guid.Empty) 
            {
                TempData["Warning"] = "You did not select any feature";
                return RedirectToPage("/Admin/Features");
            }

            var feature = await _featureService.GetFeatureAsync(Id);

            if (feature == null) 
            {
                TempData["Failed"] = "The selected feature does not exist";
                return RedirectToPage("/Admin/Features");
            }

            ViewData["Title"] = $"{feature.Title} Details";

            GetData(feature);

            return Page();
        }

        public async Task<IActionResult> OnPost() 
        {
            if (Id == Guid.Empty) 
            {
                TempData[""] = "";
                return Page();
            }

            var response = await _featureService.DeleteFeatureAsync(Id);

            if (response == null) 
            {
                TempData["Failed"] = "Operation failed, you did not choose any feature";
                GetData(response);
                return Page();
            }
            else
                TempData["Success"] = "The operation to delete feature was successful";

            return RedirectToPage("/Admin/Features");
        }

        void GetData(Feature feature) 
        {
            ViewModel = new FeatureDetialsViewModel();
            ViewModel.Feature = new Feature();
            ViewModel.Children = new List<Child>();

            ViewModel.Feature = feature;

            if (feature.Children.Count > 0)
            {
                foreach (var child in feature.Children)
                {
                    ViewModel.Children.Add(child.Child);
                }
            }
        }
    }
}