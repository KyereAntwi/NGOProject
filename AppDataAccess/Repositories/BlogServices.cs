using AppDataAccess.Data;
using AppModels.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppDataAccess.Repositories
{
    public class BlogServices : IBlogServices
    {
        private readonly DataContext _dataContext;

        public BlogServices(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Blog> AddBlogAsync(Blog blog)
        {
            await _dataContext.Blogs.AddAsync(blog);
            await _dataContext.SaveChangesAsync();
            return blog;
        }

        public async Task<BlogSub> AddBlogSubAsync(BlogSub blogSub)
        {
            await _dataContext.BlogSubs.AddAsync(blogSub);
            await _dataContext.SaveChangesAsync();
            return blogSub;
        }

        public async Task<List<Blog>> BlogsAsync()
        {
            return await _dataContext.Blogs.Include(b => b.Subs).ToListAsync();
        }

        public async Task<Blog> DeleteBlogAsync(Guid Id)
        {
            var blog = await _dataContext.Blogs.FindAsync(Id);
            if (blog == null)
                return null;

            _dataContext.Blogs.Remove(blog);
            await _dataContext.SaveChangesAsync();
            return blog;
        }

        public async Task<Blog> GetBlogAsync(Guid Id)
        {
            return await _dataContext.Blogs
                .Include(b => b.Subs)
                .SingleOrDefaultAsync(b => b.Id == Id);
        }
    }
}
