using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppDataAccess.Repositories;
using AppModels.DTO;
using AppModels.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppUi.Pages.Admin
{
    public class ClassesModel : PageModel
    {
        private readonly IClassServices _dataContext;

        public ClassesModel(IClassServices classServices)
        {
            _dataContext = classServices;
        }

        [BindProperty]
        public ClassesViewModel ViewModel { get; set; }

        public async Task<IActionResult> OnGet()
        {
            await GetData();
            return Page();
        }

        public async Task<IActionResult> OnPost() 
        {
            if (!ModelState.IsValid) 
            {
                TempData["Warning"] = "";
                await GetData();
                return Page();
            }

            Class subject = new Class();
            subject.Name = ViewModel.Name;
            subject.DateAdded = DateTime.Now;

            var response = await _dataContext.AddClassAsync(subject);

            if (response == null)
                TempData["Failed"] = "The class trying to be added has already been added";
            else
                TempData["Success"] = "class was added successfully";

            await GetData();
            return Page();
        }

        async Task GetData() 
        {
            ViewModel = new ClassesViewModel();
            ViewModel.Classes = new List<Class>();

            var response = await _dataContext.ClassesAsync();

            if (response.Count > 0)
            {
                ViewModel.Classes = response;
            }
        }
    }
}