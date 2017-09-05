using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace Travel.WebAPI.Infrastructure
{
    public interface ICategoryRepository
    {
        Task<DAL.vCategory> GetCategory();
        Task<IEnumerable<DAL.vCategory>> GetCategories();
        Task<DAL.Category> UpdateCategory(int id);
        Task<int> SaveCategory();
        Task<int> DeleteCategory(int id);
    }
}