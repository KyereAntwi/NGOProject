using AppDataAccess.Repositories;
using AppModels.DTO;
using AppModels.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppUi.ViewComponents
{
    public class SupportingChildren : ViewComponent
    {
        private readonly IUsersServices _userData;

        public SupportingChildren(IUsersServices usersServices)
        {
            _userData = usersServices;
        }

        public async Task<IViewComponentResult> InvokeAsync(string UserId) 
        {
            var children = await _userData.GetSupportingChildren(UserId);

            SupportingChildrenViewModel ViewModel = new SupportingChildrenViewModel()
            {
                Children = children
            };

            return View(ViewModel);
        }
    }
}
