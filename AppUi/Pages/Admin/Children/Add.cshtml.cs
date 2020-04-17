using System;
using System.IO;
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
    public class AddModel : PageModel
    {
        private readonly IChildServices _data;

        public AddModel(IChildServices childServices)
        {
            _data = childServices;
        }

        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }
        [BindProperty()]
        public NewChildViewModel ViewModel { get; set; }

        public async Task<IActionResult> OnGet()
        {
            if (Id != Guid.Empty)
            {
                ViewData["Title"] = "Update Child";
                var child = await _data.GetAChildAsync(Id);

                if (child == null)
                {
                    TempData["Warning"] = "The selected child does not exist";
                    return RedirectToPage("/Admin/Children/All");
                }

                ViewModel = new NewChildViewModel();
                ViewModel.DateOfBirth = child.DateOfBirth;
                ViewModel.Gender = child.Gender;
                ViewModel.GivingName = child.GivingName;
                ViewModel.Nationality = child.Nationality;
                ViewModel.Othernames = child.Othernames;
                ViewModel.Surname = child.Surname;
                ViewModel.Teaser = child.Teaser;
            }
            else 
            {
                ViewData["Title"] = "New Child";
            }
            
            return Page();
        }

        public async Task<IActionResult> OnPost() 
        {
            if (!ModelState.IsValid) 
            {
                TempData["Warning"] = "You entered wrong values, try again!";
                return Page();
            }


            Child child = new Child 
            {
                GivingName = ViewModel.GivingName,
                Othernames = ViewModel.Othernames,
                Surname = ViewModel.Surname,
                DateAdded = DateTime.Now,
                DateOfBirth = ViewModel.DateOfBirth.Date,
                Nationality = ViewModel.Nationality,
                Teaser = ViewModel.Teaser,
                Gender = ViewModel.Gender
            };

            // process file upload
            if (ViewModel.Photograph != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await ViewModel.Photograph.CopyToAsync(memoryStream);
                    // Upload the file if less than 2 MB        
                    if (memoryStream.Length < 2097152)
                    {
                        child.Phtotograph = memoryStream.ToArray();
                    }
                    else 
                    {
                        TempData["Warning"] = "File must not be more than 2mb";
                        return Page();
                    }
                }
            }

            Child response = new Child();

            if (Id == Guid.Empty)
                response = await _data.AddAChild(child);
            else 
            {
                child.Id = Id;
                response = await _data.UpdateChild(child);
            }

            if (response == null)
                TempData["Failed"] = "Operation failed, try again";
            else
            {
                if (Id == Guid.Empty)
                    TempData["Success"] = "Child added successfully";
                else
                {
                    TempData["Success"] = "Child Updated successfully";
                    return RedirectToPage("/Admin/Children/Details", new { Id } );
                }
            }

            return RedirectToPage("/Admin/Children/All");
        }
    }
}
