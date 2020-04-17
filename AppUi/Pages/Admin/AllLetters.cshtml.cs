using System.Collections.Generic;
using System.Threading.Tasks;
using AppDataAccess.Repositories;
using AppModels.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppUi.Pages.Admin
{
    public class AllLettersModel : PageModel
    {
        private readonly ILettersServices _letterData;

        public AllLettersModel(ILettersServices lettersServices)
        {
            _letterData = lettersServices;
        }
        public List<Letter> Letters { get; set; }
        public async Task<IActionResult> OnGet()
        {
            Letters = await _letterData.GetLettersAsync();
            return Page();
        }
    }
}