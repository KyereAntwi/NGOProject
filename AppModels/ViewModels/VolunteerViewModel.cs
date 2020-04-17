using AppModels.DTO;
using Microsoft.AspNetCore.Identity;

namespace AppModels.ViewModels
{
    public class VolunteerViewModel
    {
        public ApplicationUser User { get; set; }
        public IdentityUser Identity { get; set; }
    }
}
