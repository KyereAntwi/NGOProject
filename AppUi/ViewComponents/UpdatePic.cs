using AppDataAccess.Repositories;
using AppModels.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AppUi.ViewComponents
{
    public class UpdatePic : ViewComponent
    {
        private readonly IUsersServices _user;

        public UpdatePic(IUsersServices usersServices)
        {
            _user = usersServices;
        }

        public async Task<IViewComponentResult> InvokeAsync(Guid Id)
        {
            var user = await _user.GetUserAsync(Id);

            UpdatePictureViewModel viewModel = new UpdatePictureViewModel();
            viewModel.Id = user.Id;

            return View(viewModel);
        }
    }
}
