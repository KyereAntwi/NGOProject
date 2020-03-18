using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDataAccess.Repositories;
using AppModels.DTO;
using Microsoft.AspNetCore.Mvc;

namespace AppUi.Controllers
{
    public class UtilityController : Controller
    {
        private readonly IChildServices _childData;

        public UtilityController(IChildServices childData)
        {
            _childData = childData;
        }

        public async Task<FileContentResult> GetChildProfileImage(Guid Id)
        {
            if (Id == Guid.Empty)
                return null;

            Child child = await _childData.GetAChildAsync(Id);

            if (child == null)
                return null;

            return File(child.Phtotograph, "");
        }

        public async Task<FileContentResult> GetUserProfileImage(Guid Id)
        {
            if (Id == Guid.Empty)
                return null;

            Child child = await _childData.GetAChildAsync(Id);

            if (child == null)
                return null;

            return File(child.Phtotograph, "");
        }
    }
}