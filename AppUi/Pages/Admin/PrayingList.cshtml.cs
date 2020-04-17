using System.Collections.Generic;
using System.Threading.Tasks;
using AppDataAccess.Repositories;
using AppModels.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppUi.Pages.Admin
{
    public class PrayingListModel : PageModel
    {
        private readonly IPrayerSupportServices _data;

        public PrayingListModel(IPrayerSupportServices prayerSupportServices)
        {
            _data = prayerSupportServices;
        }

        public List<PrayerVolunteer> List { get; set; }

        public async Task<IActionResult> OnGet()
        {
            List = await _data.GetPrayerVolunteersAsync();
            return Page();
        }
    }
}