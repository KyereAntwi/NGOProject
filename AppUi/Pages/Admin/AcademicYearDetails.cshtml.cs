using System;
using System.Collections.Generic;
using System.Linq;
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
    public class AcademicYearDetailsModel : PageModel
    {
        private readonly IYearsSemServices _yearServices;

        public AcademicYearDetailsModel(IYearsSemServices yearsSemServices)
        {
            _yearServices = yearsSemServices;
        }

        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }
        [BindProperty]
        public AcademicYearDetailsViewModel ViewModel { get; set;  }

        public async Task OnGet()
        {
            if (Id == Guid.Empty) 
            {
                TempData["Warning"] = "No Academic Year was selected";
                RedirectToPage("/Admin/AcademicYears");
            }

            var response = await _yearServices.GetAcademicYearAsync(Id);
            if (response == null) 
            {
                TempData["Warning"] = "Selected Academic Year does not exist";
                RedirectToPage("/Admin/AcademicYears");
            }

            ViewModel = new AcademicYearDetailsViewModel();
            ViewModel.Year = new AcademicYear();
            ViewModel.Semesters = new List<Semester>();

            ViewModel.Year = response;

            if (response.Semesters.Count > 0)
                ViewModel.Semesters = response.Semesters.ToList();

            RedirectToPage();
        }

        public async Task<IActionResult> OnPost() 
        {
            if (!ModelState.IsValid) 
            {
                TempData["Warning"] = "You submitted a no or inaccurate values";
                return RedirectToPage("/Admin/AcademicYearDetails", new { Id });
            }

            Semester semester = new Semester 
            {
                AcademicYearId = Id,
                DateAdded = DateTime.Now,
                Title = ViewModel.SemesterName
            };

            var response = await _yearServices.AddSemesterAsync(semester);

            if (response == null)
                TempData["Failed"] = $"{semester.Title} already exist for this Academic Year";
            else
                TempData["Success"] = $"{semester.Title} successfully added";

            return RedirectToPage("/Admin/AcademicYearDetails", new { Id });
        }
    }
}