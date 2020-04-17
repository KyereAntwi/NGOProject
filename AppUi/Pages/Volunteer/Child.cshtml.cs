using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDataAccess.Repositories;
using AppModels.DTO;
using AppModels.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppUi.Pages.Volunteer
{
    public class ChildModel : PageModel
    {
        private readonly IChildServices _childData;

        public ChildModel(IChildServices childServices)
        {
            _childData = childServices;
        }

        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }
        public ChildDetailsViewModel ViewMdodel { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var child = await _childData.GetAChildAsync(Id);

            ViewMdodel = new ChildDetailsViewModel();
            ViewMdodel.Child = new Child();
            ViewMdodel.Features = new List<Feature>();
            ViewMdodel.Letters = new List<Letter>();
            ViewMdodel.Registrations = new List<SemesterRegistration>();
            ViewMdodel.Sponser = new ApplicationUser();
            ViewMdodel.Class = new Class();

            ViewMdodel.Child = child;

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

            return Page();
        }
    }
}