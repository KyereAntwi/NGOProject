using AppDataAccess.Repositories;
using AppModels.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AppUi.ViewComponents
{
    public class UpdateRecords : ViewComponent
    {
        private readonly IYearsSemServices _semData;
        private readonly ISubjectServices _subjectData;

        public UpdateRecords(IYearsSemServices yearsSemServices, ISubjectServices subjectServices)
        {
            _semData = yearsSemServices;
            _subjectData = subjectServices;
        }

        public async Task<IViewComponentResult> InvokeAsync(Guid ChildId) 
        {
            UpdateStudentRecordViewModel ViewModel = new UpdateStudentRecordViewModel()
            {
                Semesters = await _semData.GetSemestersAsync(),
                Subjects = await _subjectData.GetStudentSubjectsAsync(ChildId),
                ChildId = ChildId
            };

            return View(ViewModel);
        }
    }
}
