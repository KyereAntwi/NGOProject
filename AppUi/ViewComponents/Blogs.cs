using AppDataAccess.Repositories;
using AppModels.DTO;
using AppModels.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppUi.ViewComponents
{
    public class Blogs : ViewComponent
    {
        private readonly IBlogServices _data;

        public Blogs(IBlogServices blogServices)
        {
            _data = blogServices;
        }

        public async Task<IViewComponentResult> InvokeAsync() 
        {
            BlogsNavViewModel viewModel = new BlogsNavViewModel();
            viewModel.Blogs = new List<Blog>();

            var blogList = await _data.BlogsAsync();

            int i = 1;
            foreach (var item in blogList) 
            {
                if (i <= 4) 
                {
                    viewModel.Blogs.Add(item);
                }

                i++;
            }

            viewModel.Total = blogList.Count;

            return View(viewModel);
        }
    }
}
