using AppModels.DTO;
using Microsoft.AspNetCore.Identity;

namespace AppModels.ViewModels
{
    public class UserSummaryViewModel
    {
        public ApplicationUser User { get; set; }
        public IdentityUser Identity { get; set; }
    }
}
