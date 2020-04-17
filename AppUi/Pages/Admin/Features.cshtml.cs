using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppDataAccess.Repositories;
using AppModels.DTO;
using AppModels.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppUi.Pages.Admin
{
    public class FeaturesModel : PageModel
    {
        private readonly IFeaturesServices _featureServices;

        public FeaturesModel(IFeaturesServices featuresServices)
        {
            _featureServices = featuresServices;
        }

        [BindProperty]
        public FeaturesViewModel ViewModel { get; set; }

        public async Task<IActionResult> OnGet()
        {
            ViewData["Title"] = "Features List";
            await GetList();
            return Page();
        }

        public async Task<IActionResult> OnPost() 
        {
            if (!ModelState.IsValid) 
            {
                TempData["Warning"] = "Sorry you entered wrong or incomplete values";
                await GetList();
                return Page();
            }

            Feature feature = new Feature 
            {
                Title = ViewModel.Title,
                Disability = ViewModel.Disability,
                DateAdded = DateTime.Now
            };

            var response = await _featureServices.AddFeatureAsync(feature);
            if (response == null)
                TempData["Warning"] = "Sorry the feature submitted already exist";
            else
                TempData["Success"] = "The operation was successful";

            await GetList();
            return Page();
        }

        async Task GetList() 
        {
            ViewModel = new FeaturesViewModel();
            ViewModel.Features = new List<Feature>();
            var featureList = await _featureServices.GetFeaturesAsync();
            ViewModel.Features = featureList;
        }
    }
}