using System;
using System.Threading.Tasks;
using AppDataAccess.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppUi.Pages.Admin
{
    [Authorize(Roles = "Administrator")]
    public class AddSubjectToClassModel : PageModel
    {
        private readonly IClassServices _data;

        public AddSubjectToClassModel(IClassServices data)
        {
            _data = data;
        }

        [BindProperty(SupportsGet = true)]
        public Guid ClassId { get; set; }
        [BindProperty(SupportsGet = true)]
        public Guid SubjectId { get; set; }

        public async Task<IActionResult> OnGet()
        {
            if (ClassId == Guid.Empty || SubjectId == Guid.Empty) 
            {
                TempData["Warning"] = "You did not select any subject to be added";
                return RedirectToPage("/Admin/ClassDetails", new { Id = ClassId });
            }

            var response = await _data.AddSubjectToClassAsync(ClassId, SubjectId);

            if (response == null)
                TempData["Failed"] = "The operation failed! Reason, subject would already be part of the list";
            else
                TempData["Success"] = "Subject was successfully added";

            return RedirectToPage("/Admin/ClassDetails", new { ClassId = ClassId });
        }
    }
}