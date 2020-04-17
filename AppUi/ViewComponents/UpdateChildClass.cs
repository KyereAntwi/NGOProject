using AppDataAccess.Repositories;
using AppModels.DTO;
using AppModels.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppUi.ViewComponents
{
    public class UpdateChildClass : ViewComponent
    {
        private readonly IClassServices _classData;

        public UpdateChildClass(IClassServices classServices)
        {
            _classData = classServices;
        }

        public async Task<IViewComponentResult> InvokeAsync(Guid ChildId) 
        {
            var list = await _classData.ClassesAsync();

            UpdateChildClassViewModel ViewModel = new UpdateChildClassViewModel
            {
                ChildId = ChildId
            };

            ViewModel.Classes = new List<Class>();
            ViewModel.Classes = list;

            return View(ViewModel);
        }
    }
}
