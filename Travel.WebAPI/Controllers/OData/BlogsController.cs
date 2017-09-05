using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using Travel.WebAPI.Providers;

namespace Travel.WebAPI.Controllers.OData
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using Travel.WebAPI.Providers;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Blog>("Blogs");
    builder.EntitySet<Author>("Authors"); 
    builder.EntitySet<Category>("Categories"); 
    builder.EntitySet<Country>("Countries"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class BlogsController : ODataController
    {
        private WebAPIContext db = new WebAPIContext();

        // GET: odata/Blogs
        [EnableQuery]
        public IQueryable<Blog> GetBlogs()
        {
            return db.Blogs;
        }

        // GET: odata/Blogs(5)
        [EnableQuery]
        public SingleResult<Blog> GetBlog([FromODataUri] int key)
        {
            return SingleResult.Create(db.Blogs.Where(blog => blog.BlogID == key));
        }

        // PUT: odata/Blogs(5)
        [Authorize()]
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Blog> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Blog blog = await db.Blogs.FindAsync(key);
            if (blog == null)
            {
                return NotFound();
            }

            patch.Put(blog);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(blog);
        }

        // POST: odata/Blogs
        [Authorize()]
        public async Task<IHttpActionResult> Post(Blog blog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Blogs.Add(blog);
            await db.SaveChangesAsync();

            return Created(blog);
        }

        // PATCH: odata/Blogs(5)
        [Authorize()]
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Blog> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Blog blog = await db.Blogs.FindAsync(key);
            if (blog == null)
            {
                return NotFound();
            }

            patch.Patch(blog);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(blog);
        }

        // DELETE: odata/Blogs(5)
        [Authorize()]
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Blog blog = await db.Blogs.FindAsync(key);
            if (blog == null)
            {
                return NotFound();
            }

            db.Blogs.Remove(blog);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Blogs(5)/Author
        [EnableQuery]
        public SingleResult<Author> GetAuthor([FromODataUri] int key)
        {
            return SingleResult.Create(db.Blogs.Where(m => m.BlogID == key).Select(m => m.Author));
        }

        // GET: odata/Blogs(5)/Category
        [EnableQuery]
        public SingleResult<Category> GetCategory([FromODataUri] int key)
        {
            return SingleResult.Create(db.Blogs.Where(m => m.BlogID == key).Select(m => m.Category));
        }

        // GET: odata/Blogs(5)/Country
        [EnableQuery]
        public SingleResult<Country> GetCountry([FromODataUri] int key)
        {
            return SingleResult.Create(db.Blogs.Where(m => m.BlogID == key).Select(m => m.Country));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BlogExists(int key)
        {
            return db.Blogs.Count(e => e.BlogID == key) > 0;
        }
    }
}
