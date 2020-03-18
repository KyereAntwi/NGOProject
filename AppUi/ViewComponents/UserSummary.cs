using AppDataAccess.Repositories;
using AppModels.DTO;
using AppModels.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AppUi.ViewComponents
{
    public class UserSummary : ViewComponent
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUsersServices _userServices;

        public UserSummary(UserManager<IdentityUser> userManager, IUsersServices usersServices)
        {
            _userManager = userManager;
            _userServices = usersServices;
        }

        public async Task<IViewComponentResult> InvokeAsync(string userId) 
        {
            UserSummaryViewModel viewModel = new UserSummaryViewModel();

            try
            {
                var loggedUser = await _userManager.FindByIdAsync(userId);
                if (loggedUser != null) 
                {
                    viewModel.User = new ApplicationUser();
                    viewModel.Identity = new IdentityUser();

                    var response = await _userServices.GetUserAsync(userId);

                    if (response != null) 
                    {
                        viewModel.User = response;
                        viewModel.Identity = loggedUser;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return View(viewModel);
        }
    }
}
