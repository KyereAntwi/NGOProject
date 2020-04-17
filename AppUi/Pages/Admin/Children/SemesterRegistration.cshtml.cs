using System;
using System.Threading.Tasks;
using AppDataAccess.Repositories;
using AppModels.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppUi.Pages.Admin.Children
{
    [Authorize(Roles = "Administrator")]
    public class SemesterRegistrationModel : PageModel
    {
        private readonly IChildServices _data;

        public SemesterRegistrationModel(IChildServices childServices)
        {
            _data = childServices;
        }

        [BindProperty()]
        public SemesterRegistrationViewModel ViewModel { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost() 
        {
            if (ViewModel.ChildId == Guid.Empty || ViewModel.SemesterId == Guid.Empty) 
            {
                TempData["Warning"] = "";
                return RedirectToPage("/Admin/Children/Details", new { Id = ViewModel.ChildId });
            }

            var response = await _data.RegisterForSemester(ViewModel.ChildId, ViewModel.SemesterId);

            if (response)
                TempData["Success"] = "Semester Registration was successfully done";
            else
                TempData["Failed"] = "The operation failed, try again";

            return RedirectToPage("/Admin/Children/Details", new { Id = ViewModel.ChildId });
        }
    }
}