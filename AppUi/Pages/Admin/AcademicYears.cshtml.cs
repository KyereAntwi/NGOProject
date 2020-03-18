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
    public class AcademicYearsModel : PageModel
    {
        private readonly IYearsSemServices _yearData;

        public AcademicYearsModel(IYearsSemServices yearsSemServices)
        {
            _yearData = yearsSemServices;
        }

        [BindProperty]
        public AcademicYearsViewModel ViewModel { get; set; }

        public async Task OnGet()
        {
            ViewModel = new AcademicYearsViewModel();
            ViewModel.Years = new List<AcademicYear>();
            ViewModel.Years = await _yearData.GetAcademicYearsAsync();
        }

        public async Task OnPost() 
        {
            if (ModelState.IsValid) 
            {
                var newYear = new AcademicYear() 
                {
                    Year = ViewModel.YearName,
                    DateAdded = DateTime.Now
                };

                var response = await _yearData.AddAcademicYearAsync(newYear);
                if (response == null)
                    TempData["Warning"] = "The academic year submitted already exist!";
                else
                    TempData["Success"] = $"The academic year with name {response.Year.ToUpper()} was added successfully";

                ViewModel = new AcademicYearsViewModel();
                ViewModel.Years = new List<AcademicYear>();
                ViewModel.Years = await _yearData.GetAcademicYearsAsync();
                RedirectToPage();
            }
        }
    }
}