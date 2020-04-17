using System;
using System.Threading.Tasks;
using AppDataAccess.Repositories;
using AppModels.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppUi.Pages.Admin.Children
{
    [Authorize(Roles = "Administrator")]
    public class LetterModel : PageModel
    {
        private readonly ILettersServices _letterData;

        public LetterModel(ILettersServices lettersServices)
        {
            _letterData = lettersServices;
        }

        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }
        public Letter Letter { get; set; }

        public async Task<IActionResult> OnGet()
        {
            Letter = await _letterData.GetLetterAsync(Id);
            _ = await _letterData.MarkLetterReadAsync(Id);
            return Page();
        }
    }
}