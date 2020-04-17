using AppDataAccess.Repositories;
using AppModels.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppUi.ViewComponents
{
    [Authorize]
    public class Notifications : ViewComponent
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly INotificationServices _notificationService;
        private readonly IUsersServices _user;

        public Notifications(UserManager<IdentityUser> userManager, 
                            IUsersServices usersServices,
                            INotificationServices notificationServices)
        {
            _userManager = userManager;
            _notificationService = notificationServices;
            _user = usersServices;
        }

        public async Task<IViewComponentResult> InvokeAsync() 
        {
            List<Notification> Notifications = new List<Notification>();
            var user = await _user.GetUserByUserIdAsync(_userManager.GetUserAsync(HttpContext.User).Result.Id);
            Notifications = await _notificationService.GetUsersNotificationsAsync(user.Id);
            return View(Notifications);
        }
    }
}
