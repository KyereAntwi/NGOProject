using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppDataAccess.Data;
using AppDataAccess.Repositories;
using AppModels.DTO;
using AppModels.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AppUi.Pages.Admin
{
    [Authorize(Roles = "Administrator")]
    public class ClassDetailsModel : PageModel
    {
        private readonly IClassServices dataService;
        private readonly DataContext _context;

        public ClassDetailsModel(IClassServices classServices, DataContext subject)
        {
            dataService = classServices;
            _context = subject;
        }

        [BindProperty(SupportsGet = true)]
        public Guid ClassId { get; set; }
        public ClassDetailsViewModel ViewModel { get; set; }

        public async Task<IActionResult> OnGet()
        {
            if (ClassId == Guid.Empty) 
            {
                TempData["Warning"] = "No class was selected";
                return RedirectToPage("/Admin/Classes");
            }

            var classResponse = await dataService.ClassAsync(ClassId);

            if (classResponse == null) 
            {
                TempData["Failed"] = "The class selected does not exist";
                return RedirectToPage("/Admin/Classes");
            }

            ViewModel = new ClassDetailsViewModel
            {
                Class = classResponse,
                Children = new List<Child>(),
                BelongingSubjects = new List<Subject>(),
                NonBelongingSubjects = new List<Subject>(),
            };

            if (classResponse.Students.Count > 0) 
            {
                foreach (var student in classResponse.Students) 
                {
                    ViewModel.Children.Add(student.Child);
                }
            }

            if (classResponse.Subjects.Count > 0) 
            {
                foreach (var subject in classResponse.Subjects) 
                {
                    ViewModel.BelongingSubjects.Add(subject.Subject);
                }
            }

            if (await _context.Subjects.AnyAsync()) 
            {
                var allSubjects = await _context.Subjects.ToListAsync();
                allSubjects.ForEach(subject =>
                {
                    if (!ViewModel.BelongingSubjects.Contains(subject))
                        ViewModel.NonBelongingSubjects.Add(subject);
                });
            }


            ViewData["Title"] = $"{ViewModel.Class.Name} Details";
            return Page();
        }

        public async Task<IActionResult> OnPost() 
        {
            if (ClassId == Guid.Empty) 
            {
                TempData["Warning"] = "You did not provide any class Id for this operation";
                return Page();
            }

            var response = await dataService.DeleteClassAsync(ClassId);
            if (response == null)
                TempData["Failed"] = "";
            else
                TempData["Success"] = "The operation to delete class was successful";

            return RedirectToPage("/Admin/Classes");
        }
    }
}