using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDataAccess.Repositories;
using AppModels.DTO;
using AppModels.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppUi.Pages.Volunteer
{
    [Authorize(Roles = "Volunteer")]
    public class VisitationsModel : PageModel
    {
        private readonly IUsersServices _visitationData;
        private UserManager<IdentityUser> _userManager;
        private readonly IVisitationServices _data;

        public VisitationsModel(IUsersServices visitationServices,
                                UserManager<IdentityUser> userManager,
                                IVisitationServices services)
        {
            _visitationData = visitationServices;
            _userManager = userManager;
            _data = services;
        }

        [BindProperty()]
        public VisitationsViewModel ViewModel { get; set; }
        public ApplicationUser AppUser { get; set; }

        public async Task<IActionResult> OnGet()
        {
            AppUser = await _visitationData.GetUserByUserIdAsync(_userManager.GetUserAsync(User).Result.Id);

            ViewModel = new VisitationsViewModel();
            ViewModel.Visitations = new List<Visitation>();

            if (AppUser.Visitations != null) 
            {
                foreach (var item in AppUser.Visitations)
                    ViewModel.Visitations.Add(item);
            }
            return Page();
        }

        public async Task<IActionResult> OnPost() 
        {
            AppUser = await _visitationData.GetUserByUserIdAsync(_userManager.GetUserAsync(User).Result.Id);

            Visitation visitation = new Visitation 
            {
                UserId = AppUser.Id,
                Cancelled = false,
                Done = false,
                DateAdded = DateTime.Now,
                LeavingDate = ViewModel.LeavingDate,
                ReportingDate = ViewModel.ReportingDate
            };

            _ = await _data.BookVisitationAsync(visitation);
            return RedirectToPage();
        }
    }
}