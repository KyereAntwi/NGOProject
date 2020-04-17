using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDataAccess.Repositories;
using AppModels.DTO;
using AppModels.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppUi.Pages.Admin.Children
{
    public class DetailsModel : PageModel
    {
        private readonly IChildServices _childData;

        public DetailsModel(IChildServices childData)
        {
            _childData = childData;
        }

        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }
        public ChildDetailsViewModel ViewMdodel { get; set; }

        public async Task<IActionResult> OnGet()
        {
            if (Id == Guid.Empty)
                return RedirectToPage("/Admin/Children/All");
              
            var child = await _childData.GetAChildAsync(Id);

            if (child == null) 
            {
                TempData["Failed"] = "";
                return RedirectToPage("/Adim/Child/All");
            }

            ViewData["Title"] = $"{child.Fullname} Details";

            ViewMdodel = new ChildDetailsViewModel();
            ViewMdodel.Child = new Child();
            ViewMdodel.Features = new List<Feature>();
            ViewMdodel.Letters = new List<Letter>();
            ViewMdodel.Registrations = new List<SemesterRegistration>();
            ViewMdodel.Sponser = new ApplicationUser();
            ViewMdodel.Class = new Class();

            ViewMdodel.Child = child;

            if (child.Sponser != null)
            {
                ViewMdodel.Sponser = child.Sponser.ApplicationUser;
            }
            else 
            {
                ViewMdodel.Sponser = null;
            }

            if (child.Registrations.Count > 0) 
            {
                ViewMdodel.Registrations = child.Registrations.ToList();
            }

            if (child.Features.Count > 0) 
            {
                foreach (var item in child.Features)
                    ViewMdodel.Features.Add(item.Feature);
            }

            if (child.Class != null)
                ViewMdodel.Class = child.Class.Class;
            else
                ViewMdodel.Class = null;

            if (child.Letters.Count > 0) 
            {
                ViewMdodel.Letters = child.Letters.ToList();
            }

            return Page();
        }
    }
}