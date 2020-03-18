using AppDataAccess.Repositories;
using AppModels.DTO;
using AppModels.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppUi.Pages.Admin
{
    [Authorize(Roles = "Administrator")]
    public class SearchChildModel : PageModel
    {
        private readonly IChildServices _childServices;

        public SearchChildModel(IChildServices childServices)
        {
            _childServices = childServices;
        }

        [BindProperty]
        public SearchChildViewModel SearchModel { get; set; }

        public void OnGet()
        {
        }

        public async Task OnPost() 
        {
            if (string.IsNullOrWhiteSpace(SearchModel.KeyString)) 
            {
                SearchModel = new SearchChildViewModel();
                SearchModel.Children = new List<Child>();
                RedirectToPage();
            }

            var list = await _childServices.GetAllChildrenAsync();
            if (list.Count > 0)
            {
                list = list
                    .Where(c => c.Fullname.ToLower()
                    .Contains(SearchModel.KeyString.ToLower()))
                    .ToList();

                SearchModel = new SearchChildViewModel();
                SearchModel.Children = new List<Child>();

                SearchModel.Children = list;
                RedirectToPage();
            }
            else 
            {
                SearchModel = new SearchChildViewModel();
                SearchModel.Children = new List<Child>();
                RedirectToPage();
            }
        } 
    }
}