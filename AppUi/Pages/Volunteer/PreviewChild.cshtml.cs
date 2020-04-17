using System;
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
    public class PreviewChildModel : PageModel
    {
        private readonly IChildServices _data;

        public PreviewChildModel(IChildServices childServices)
        {
            _data = childServices;
        }

        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty()]
        public SupportChildViewModel ViewModel { get; set; }

        public async Task<IActionResult> OnGet()
        {
            ViewModel = new SupportChildViewModel 
            {
                Child = await _data.GetAChildAsync(Id)
            };

            ViewData["Title"] = $"Preview {ViewModel.Child.Fullname}";
            return Page();
        }
    }
}
