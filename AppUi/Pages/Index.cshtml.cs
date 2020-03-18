using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDataAccess.Repositories;
using AppModels.DTO;
using AppModels.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace AppUi.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IChildServices _childData;

        public IndexModel(ILogger<IndexModel> logger, IChildServices childServices)
        {
            _logger = logger;
            _childData = childServices;
        }

        public IndexViewModel ViewModel { get; set; }

        public async Task OnGet()
        {
            try
            {
                ViewModel = new IndexViewModel();
                ViewModel.Children = await _childData.GetAllChildrenAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
