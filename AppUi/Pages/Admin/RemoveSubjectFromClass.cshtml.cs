using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDataAccess.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppUi.Pages.Admin
{
    [Authorize(Roles = "Administrator")]
    public class RemoveSubjectFromClassModel : PageModel
    {
        private readonly IClassServices _data;

        public RemoveSubjectFromClassModel(IClassServices data)
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
                TempData["Warning"] = "";
                return RedirectToPage("/Admin/ClassDetails", new { ClassId = ClassId });
            }

            var response = await _data.RemoveSubjectFromClassAsync(ClassId, SubjectId);

            if (response == null)
                TempData["Failed"] = "Operation failed because selected subject does not exist in list";
            else
                TempData["Success"] = "Subject successfully remove form list";

            return RedirectToPage("/Admin/ClassDetails", new { ClassId = ClassId });
        }
    }
}