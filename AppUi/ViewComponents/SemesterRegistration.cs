using AppDataAccess.Repositories;
using AppModels.DTO;
using AppModels.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppUi.ViewComponents
{
    public class SemesterRegistration : ViewComponent
    {
        private readonly IYearsSemServices _data;

        public SemesterRegistration(IYearsSemServices yearsSemServices)
        {
            _data = yearsSemServices;
        }

        public async Task<IViewComponentResult> InvokeAsync(Guid ChildId) 
        {
            SemesterRegistrationViewModel viewModel = new SemesterRegistrationViewModel()
            {
                ChildId = ChildId
            };

            viewModel.Semesters = new List<Semester>();
            viewModel.Semesters = await _data.GetSemestersAsync();

            return View(viewModel);
        }
    }
}
