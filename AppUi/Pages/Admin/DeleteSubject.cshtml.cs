using System;
using System.Threading.Tasks;
using AppDataAccess.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppUi.Pages.Admin
{
    [Authorize(Roles = "Administrator")]
    public class DeleteSubjectModel : PageModel
    {
        private readonly ISubjectServices data;

        public DeleteSubjectModel(ISubjectServices subjectServices)
        {
            data = subjectServices;
        }

        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }
        public async Task<IActionResult> OnGet()
        {
            if (Id == Guid.Empty)
            {
                TempData["Warning"] = "";
                return RedirectToPage("/Admin/Subjects");
            }

            var response = await data.DeleteSubjectAsync(Id);

            if (response == null)
                TempData["Failed"] = "The subject submitted does not exist";
            else
                TempData["Success"] = "The subject was deleted successfully";

            return RedirectToPage("/Admin/Subjects");
        }
    }
}