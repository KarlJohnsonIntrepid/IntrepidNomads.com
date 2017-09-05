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
    builder.EntitySet<Location>("Locations");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class LocationsController : ODataController
    {
        private WebAPIContext db = new WebAPIContext();

        // GET: odata/Locations
        [EnableQuery]
        public IQueryable<Location> GetLocations()
        {
            return db.Locations;
        }

        // GET: odata/Locations(5)
        [EnableQuery]
        public SingleResult<Location> GetLocation([FromODataUri] int key)
        {
            return SingleResult.Create(db.Locations.Where(location => location.LocationID == key));
        }

        // PUT: odata/Locations(5)
        [Authorize()]
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Location> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Location location = await db.Locations.FindAsync(key);
            if (location == null)
            {
                return NotFound();
            }

            patch.Put(location);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(location);
        }

        // POST: odata/Locations
        [Authorize()]
        public async Task<IHttpActionResult> Post(Location location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Locations.Add(location);
            await db.SaveChangesAsync();

            return Created(location);
        }

        // PATCH: odata/Locations(5)
        [Authorize()]
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Location> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Location location = await db.Locations.FindAsync(key);
            if (location == null)
            {
                return NotFound();
            }

            patch.Patch(location);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(location);
        }

        // DELETE: odata/Locations(5)
        [Authorize()]
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Location location = await db.Locations.FindAsync(key);
            if (location == null)
            {
                return NotFound();
            }

            db.Locations.Remove(location);
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

        private bool LocationExists(int key)
        {
            return db.Locations.Count(e => e.LocationID == key) > 0;
        }
    }
}
