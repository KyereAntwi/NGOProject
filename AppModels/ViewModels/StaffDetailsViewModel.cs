using AppModels.DTO;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace AppModels.ViewModels
{
    public class StaffDetailsViewModel
    {
        public IdentityUser User { get; set; }
        public ApplicationUser AppUser { get; set; }
        public List<TimeLine> TimeLines { get; set; }
    }
}
