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
    builder.EntitySet<vCategory>("vCategories");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class vCategoriesController : ODataController
    {
        private WebAPIContext db = new WebAPIContext();

        // GET: odata/vCategories
        [EnableQuery]
        public IQueryable<vCategory> GetvCategories()
        {
            return db.vCategories;
        }

        // GET: odata/vCategories(5)
        [EnableQuery]
        public SingleResult<vCategory> GetvCategory([FromODataUri] int key)
        {
            return SingleResult.Create(db.vCategories.Where(vCategory => vCategory.CategoryID == key));
        }

        // PUT: odata/vCategories(5)
        [Authorize()]
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<vCategory> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            vCategory vCategory = await db.vCategories.FindAsync(key);
            if (vCategory == null)
            {
                return NotFound();
            }

            patch.Put(vCategory);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!vCategoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(vCategory);
        }

        // POST: odata/vCategories
        [Authorize()]
        public async Task<IHttpActionResult> Post(vCategory vCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.vCategories.Add(vCategory);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (vCategoryExists(vCategory.CategoryID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(vCategory);
        }

        // PATCH: odata/vCategories(5)
        [AcceptVerbs("PATCH", "MERGE")]
        [Authorize()]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<vCategory> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            vCategory vCategory = await db.vCategories.FindAsync(key);
            if (vCategory == null)
            {
                return NotFound();
            }

            patch.Patch(vCategory);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!vCategoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(vCategory);
        }

        // DELETE: odata/vCategories(5)
        [Authorize()]
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            vCategory vCategory = await db.vCategories.FindAsync(key);
            if (vCategory == null)
            {
                return NotFound();
            }

            db.vCategories.Remove(vCategory);
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

        private bool vCategoryExists(int key)
        {
            return db.vCategories.Count(e => e.CategoryID == key) > 0;
        }
    }
}
