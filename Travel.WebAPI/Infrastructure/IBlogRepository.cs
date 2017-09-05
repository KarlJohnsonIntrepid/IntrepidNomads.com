using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Threading.Tasks;

namespace Travel.WebAPI
{
    public interface IBlogRepository
    {
        Task<DAL.vBlog> Blog(int id);
        Task<IEnumerable<DAL.vBlog>> Blogs();
        Task<int> SaveBlogASync(DAL.Blog blog);
        Task<int> UpdateBlog(DAL.Blog blog);
        Task<DAL.Blog> DeleteBlogAsync(int BlogID);
    }
}