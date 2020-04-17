using AppDataAccess.Repositories;
using AppModels.DTO;
using AppModels.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppUi.ViewComponents
{
    public class UpdateChildFeature : ViewComponent
    {
        private readonly IFeaturesServices _featuresData;
        private readonly IChildServices _childData;

        public UpdateChildFeature(IFeaturesServices featuresServices, IChildServices childServices)
        {
            _featuresData = featuresServices;
            _childData = childServices;
        }

        public async Task<IViewComponentResult> InvokeAsync(Guid ChildId) 
        {
            UpdateChildFeatureViewModel ViewModel = new UpdateChildFeatureViewModel();
            ViewModel.Features = new List<Feature>();

            var featureList = await _featuresData.GetFeaturesAsync();
            if (featureList.Count > 0) 
            {
                List<Feature> newList = new List<Feature>();
                var child = await _childData.GetAChildAsync(ChildId);

                if (child.Features.Count > 0)
                {
                    foreach (var item in child.Features)
                        newList.Add(item.Feature);

                    foreach (var item in featureList)
                    {
                        if (!newList.Contains(item))
                            ViewModel.Features.Add(item);
                    }
                }
                else 
                {
                    ViewModel.Features = featureList;
                }
            }

            ViewModel.ChildId = ChildId;
            return View(ViewModel);
        }
    }
}
