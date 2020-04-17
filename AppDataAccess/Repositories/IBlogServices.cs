using AppModels.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppDataAccess.Repositories
{
    public interface IBlogServices
    {
        Task<List<Blog>> BlogsAsync();
        Task<Blog> GetBlogAsync(Guid Id);
        Task<Blog> AddBlogAsync(Blog blog);
        Task<Blog> DeleteBlogAsync(Guid Id);
        Task<BlogSub> AddBlogSubAsync(BlogSub blogSub);
    }
}
