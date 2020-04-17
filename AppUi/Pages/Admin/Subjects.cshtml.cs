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
    public class SubjectsModel : PageModel
    {
        private readonly ISubjectServices _subjectData;

        public SubjectsModel(ISubjectServices subjectServices)
        {
            _subjectData = subjectServices;
        }

        [BindProperty]
        public SubjectsViewModel ViewModel { get; set; }

        public async Task<IActionResult> OnGet()
        {
            ViewData["Title"] = "Subject Lists";

            await FetchData();
            return Page();
        }

        public async Task<IActionResult> OnPost() 
        {
            if (!ModelState.IsValid) 
            {
                TempData["Warning"] = "Wrong or No Entries provided";
                await FetchData();
                return Page();
            }

            Subject subject = new Subject();

            subject.Name = ViewModel.Name;
            subject.Type = ViewModel.Type;
            subject.DateAdded = DateTime.Now;

            var response = await _subjectData.AddSubject(subject);

            if (response == null)
                TempData["Failed"] = "";
            else
                TempData["Success"] = "Subject was added successfully";

            await FetchData();
            return Page();
        }

        async Task FetchData() 
        {
            ViewModel = new SubjectsViewModel();
            ViewModel.Subjects = new List<Subject>();

            var response = await _subjectData.SubjectsAsync();

            if (response.Count > 0)
                ViewModel.Subjects = response;
        }
    }
}