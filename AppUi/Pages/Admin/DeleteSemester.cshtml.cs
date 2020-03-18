using System;
using System.Threading.Tasks;
using AppDataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppUi.Pages.Admin
{
    public class DeleteSemesterModel : PageModel
    {
        private readonly IYearsSemServices _data;

        public DeleteSemesterModel(IYearsSemServices dataService)
        {
            _data = dataService;
        }

        [BindProperty(SupportsGet = true)]
        public Guid SemId { get; set; }
        public async Task<IActionResult> OnGet()
        {
            if (SemId == Guid.Empty) 
            {
                TempData["Warning"] = "No semester was chosen";
                return RedirectToPage("/Admin/");
            }

            var response = await _data.DeleteSemester(SemId);
            if (response == null)
                TempData["Failed"] = "The chosen semester does not exist";
            else
                TempData["Success"] = "Semester deletion was successfully";

            return RedirectToPage("/Admin/AcademicYearDetails", new { Id = response.AcademicYearId });
        }
    }
}