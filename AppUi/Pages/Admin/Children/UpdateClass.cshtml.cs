using System;
using System.Threading.Tasks;
using AppDataAccess.Repositories;
using AppModels.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppUi.Pages.Admin.Children
{
    public class UpdateClassModel : PageModel
    {
        private readonly IChildServices _childData;

        public UpdateClassModel(IChildServices childServices)
        {
            _childData = childServices;
        }

        [BindProperty()]
        public UpdateChildClassViewModel ViewModel { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost() 
        {
            if (ViewModel.ChildId == Guid.Empty || ViewModel.CalssId == Guid.Empty) 
            {
                TempData["Warning"] = "Nothing has been selected from class list";
                return RedirectToPage("/Admin/Children/Details", new { Id = ViewModel.ChildId });
            }

            var response = await _childData.UpdateClass(ViewModel.CalssId, ViewModel.ChildId);

            if (response)
                TempData["Success"] = "Child's class was successfully updated";
            else
                TempData["Failed"] = "The operation failed, try again";

            return RedirectToPage("/Admin/Children/Details", new { Id = ViewModel.ChildId });
        }
    }
}