using System;
using System.Threading.Tasks;
using AppDataAccess.Repositories;
using AppModels.DTO;
using Microsoft.AspNetCore.Mvc;

namespace AppUi.Controllers
{
    public class UtilityController : Controller
    {
        private readonly IChildServices _childData;
        private readonly IUsersServices _user;

        public UtilityController(IChildServices childData, IUsersServices usersServices)
        {
            _childData = childData;
            _user = usersServices;
        }

        public async Task<FileContentResult> GetChildProfileImage(Guid Id)
        {
            if (Id == Guid.Empty)
                return null;

            Child child = await _childData.GetAChildAsync(Id);

            if (child == null)
                return null;

            return File(child.Phtotograph, "jpg");
        }

        [HttpGet]
        [Route("GetProfileImage")]
        public async Task<FileContentResult> GetUserProfileImage(Guid Id)
        {
            if (Id == Guid.Empty)
                return null;

            ApplicationUser user = await _user.GetUserAsync(Id);

            if (user == null)
                return null;

            return File(user.Photograph, "jpg");
        }
    }
}