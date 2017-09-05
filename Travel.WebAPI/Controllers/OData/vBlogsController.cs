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
    builder.EntitySet<vBlog>("vBlogs");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class vBlogsController : ODataController
    {
        private WebAPIContext db = new WebAPIContext();

        // GET: odata/vBlogs
        [EnableQuery]
        public IQueryable<vBlog> GetvBlogs()
        {
            return db.vBlogs;
        }

        // GET: odata/vBlogs(5)
        [EnableQuery]
        public SingleResult<vBlog> GetvBlog([FromODataUri] int key)
        {
            return SingleResult.Create(db.vBlogs.Where(vBlog => vBlog.BlogID == key));
        }

        // PUT: odata/vBlogs(5)
        [Authorize()]
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<vBlog> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            vBlog vBlog = await db.vBlogs.FindAsync(key);
            if (vBlog == null)
            {
                return NotFound();
            }

            patch.Put(vBlog);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!vBlogExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(vBlog);
        }

        // POST: odata/vBlogs
        [Authorize()]
        public async Task<IHttpActionResult> Post(vBlog vBlog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.vBlogs.Add(vBlog);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (vBlogExists(vBlog.BlogID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(vBlog);
        }

        // PATCH: odata/vBlogs(5)
        [Authorize()]
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<vBlog> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            vBlog vBlog = await db.vBlogs.FindAsync(key);
            if (vBlog == null)
            {
                return NotFound();
            }

            patch.Patch(vBlog);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!vBlogExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(vBlog);
        }

        // DELETE: odata/vBlogs(5)
        [Authorize()]
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            vBlog vBlog = await db.vBlogs.FindAsync(key);
            if (vBlog == null)
            {
                return NotFound();
            }

            db.vBlogs.Remove(vBlog);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool vBlogExists(int key)
        {
            return db.vBlogs.Count(e => e.BlogID == key) > 0;
        }
    }
}
