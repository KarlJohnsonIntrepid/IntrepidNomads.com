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
    builder.EntitySet<vCountry>("vCountries");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class vCountriesController : ODataController
    {
        private WebAPIContext db = new WebAPIContext();

        // GET: odata/vCountries
        [EnableQuery]
        public IQueryable<vCountry> GetvCountries()
        {
            return db.vCountries;
        }

        // GET: odata/vCountries(5)
        [EnableQuery]
        public SingleResult<vCountry> GetvCountry([FromODataUri] int key)
        {
            return SingleResult.Create(db.vCountries.Where(vCountry => vCountry.CountryID == key));
        }

        // PUT: odata/vCountries(5)
        [Authorize()]
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<vCountry> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            vCountry vCountry = await db.vCountries.FindAsync(key);
            if (vCountry == null)
            {
                return NotFound();
            }

            patch.Put(vCountry);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!vCountryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(vCountry);
        }

        // POST: odata/vCountries
        [Authorize()]
        public async Task<IHttpActionResult> Post(vCountry vCountry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.vCountries.Add(vCountry);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (vCountryExists(vCountry.CountryID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(vCountry);
        }

        // PATCH: odata/vCountries(5)
        [AcceptVerbs("PATCH", "MERGE")]
        [Authorize()]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<vCountry> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            vCountry vCountry = await db.vCountries.FindAsync(key);
            if (vCountry == null)
            {
                return NotFound();
            }

            patch.Patch(vCountry);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!vCountryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(vCountry);
        }

        // DELETE: odata/vCountries(5)
        [Authorize()]
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            vCountry vCountry = await db.vCountries.FindAsync(key);
            if (vCountry == null)
            {
                return NotFound();
            }

            db.vCountries.Remove(vCountry);
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

        private bool vCountryExists(int key)
        {
            return db.vCountries.Count(e => e.CountryID == key) > 0;
        }
    }
}
