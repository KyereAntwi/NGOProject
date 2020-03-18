using System;
using System.Threading.Tasks;
using AppDataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppUi.Pages.Admin
{
    public class DeleteYearModel : PageModel
    {
        private readonly IYearsSemServices _yearService;

        public DeleteYearModel(IYearsSemServices yearsSemServices)
        {
            _yearService = yearsSemServices;
        }

        [BindProperty(SupportsGet = true)]
        public Guid YearId { get; set; }
        public async Task<IActionResult> OnGet()
        {
            if (YearId == Guid.Empty)
                TempData["Warning"] = $"You did not supply any Academic Year Id";
            else
            {
                var response = await _yearService.DeleteAcademicYearAsync(YearId);

                if (response == null)
                    TempData["Warning"] = $"Academic Year with ID = {YearId} does not exist";
                else
                    TempData["Success"] = "Operation was successfully performed";
            }

            return RedirectToPage("/Admin/AcademicYears");
        }
    }
}