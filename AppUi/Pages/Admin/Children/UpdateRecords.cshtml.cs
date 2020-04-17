using System.Threading.Tasks;
using AppDataAccess.Repositories;
using AppModels.DTO;
using AppModels.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppUi.Pages.Admin.Children
{
    [Authorize(Roles = "Administrator")]
    public class UpdateRecordsModel : PageModel
    {
        private readonly IChildServices _childData;

        public UpdateRecordsModel(IChildServices childServices)
        {
            _childData = childServices;
        }

        [BindProperty()]
        public UpdateStudentRecordViewModel ViewModel { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync() 
        {
            if (!ModelState.IsValid) 
            {
                TempData["Warning"] = "";
                return RedirectToPage("/Admin/Children/Details", new { Id = ViewModel.ChildId });
            }

            SemesterRegistration semesterRegistration = new SemesterRegistration() 
            {
                ChildId = ViewModel.ChildId,
                ClassScore = ViewModel.ClassScore,
                ExamScore = ViewModel.ExamScore,
                SemesterId = ViewModel.SemesterId,
                SubjectId = ViewModel.SubjectId
            };

            var response = await _childData.UpdateSemesterRegistationAsync(semesterRegistration);

            if (response)
                TempData["Success"] = "Recored was updated successfully";
            else
                TempData["Failed"] = "Semester registration does not exist. Student has not been registered for the specified registration details";


            return RedirectToPage("/Admin/Children/Details", new { Id = ViewModel.ChildId });
        }
    }
}