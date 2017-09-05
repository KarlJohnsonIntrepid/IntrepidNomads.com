using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Travel.DAL;
using System.Data.Entity;

namespace Travel.WebAPI.Infrastructure
{
    public class BlogRepository : IBlogRepository
    {
        private static TravelEntities context = new TravelEntities();


        public async Task<vBlog> Blog(int id)
        {
           return await  context.vBlogs.Where(x => x.BlogID == id).FirstOrDefaultAsync();       
        }

        public async Task<IEnumerable<vBlog>> Blogs()
        {
            return await context.vBlogs.ToListAsync();
        }

        public async Task<Blog> DeleteBlogAsync(int BlogID)
        {
            DAL.Blog blog = context.Blogs.Find(BlogID);
            if(blog != null)
            {
                context.Blogs.Remove(blog);
            }

            await context.SaveChangesAsync();
            return blog;
        }

        public async Task<int> SaveBlogASync(Blog blog)
        {
            context.Blogs.Add(blog);
            return await context.SaveChangesAsync();
        }

        public async Task<int> UpdateBlog(Blog blog)
        {
            DAL.Blog dbEntry = context.Blogs.Find(blog.BlogID);

            context.Entry(dbEntry).CurrentValues.SetValues(dbEntry);

            return await context.SaveChangesAsync();
        }
    }
}