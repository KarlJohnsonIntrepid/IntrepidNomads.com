//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Web;
//using Travel.DAL;
//using System.Data.Entity;

//namespace Travel.WebAPI.Infrastructure
//{
//    public class CategoryRepository : ICategoryRepository
//    {
//        private TravelEntities context = new TravelEntities();

//        public async Task<Category> DeleteCategory(int id)
//        {
//            Category DBEntry = context.Categories.Find(id);
//            if (DBEntry != null)
//            {
//                context.Categories.Remove(DBEntry);
//            }

//            await context.SaveChangesAsync();
//            return DBEntry;

//        }

//        public async Task<IEnumerable<vCategory>> GetCategories()
//        {
//            return await context.vCategories.ToListAsync(); 
//        }

//        public async Task<vCategory> GetCategory(int id)
//        {
//            return await context.vCategories.Where(x => x.CategoryID == id).FirstOrDefaultAsync();
//        }

//        public async Task<int> SaveCategory(Category c)
//        {
//            context.Categories.Add(c);
//            return await context.SaveChangesAsync();
//        }

//        public async Task<int> UpdateCategory(Category c)
//        {
//            Category DbEntry = context.Categories.Find(c);
//            context.Entry(DbEntry).CurrentValues.SetValues(c);

//           return await context.SaveChangesAsync();
//        }
//    }
//}