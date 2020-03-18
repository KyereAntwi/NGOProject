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
    public class SemestersModel : PageModel
    {
        private readonly IYearsSemServices _dataServices;

        public SemestersModel(IYearsSemServices dataServices)
        {
            _dataServices = dataServices;
        }

        [BindProperty]
        public SemestersViewModel ViewModel { get; set; }

        public async Task<IActionResult> OnGet()
        {
            ViewData["Title"] = "Semesters List";

            await CallData();

            return Page();
        }

        public async Task<IActionResult> OnPost() 
        {
            if (!ModelState.IsValid)
            {
                TempData["Warning"] = "No semester was selected";
                return Page();
            }

            var response = await _dataServices.DeleteSemester(ViewModel.SemId);

            if (response == null)
                TempData["Failed"] = "";
            else
                TempData["Success"] = "The semester was successfully deleted";

            await CallData();

            return Page();
        }

        async Task CallData() 
        {
            var response = await _dataServices.GetSemestersAsync();

            ViewModel = new SemestersViewModel();
            ViewModel.Semesters = new List<Semester>();

            if (response.Count > 0)
                ViewModel.Semesters.AddRange(response);
        }
    }
}