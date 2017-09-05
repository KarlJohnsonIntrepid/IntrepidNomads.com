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
    builder.EntitySet<Continent>("Continents");
    builder.EntitySet<Country>("Countries"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class ContinentsController : ODataController
    {
        private WebAPIContext db = new WebAPIContext();

        // GET: odata/Continents
        [EnableQuery]
        public IQueryable<Continent> GetContinents()
        {
            return db.Continents;
        }

        // GET: odata/Continents(5)
        [EnableQuery]
        public SingleResult<Continent> GetContinent([FromODataUri] int key)
        {
            return SingleResult.Create(db.Continents.Where(continent => continent.ContinentID == key));
        }

        // PUT: odata/Continents(5)
        [Authorize()]
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Continent> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Continent continent = await db.Continents.FindAsync(key);
            if (continent == null)
            {
                return NotFound();
            }

            patch.Put(continent);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContinentExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(continent);
        }

        // POST: odata/Continents
        [Authorize()]
        public async Task<IHttpActionResult> Post(Continent continent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Continents.Add(continent);
            await db.SaveChangesAsync();

            return Created(continent);
        }

        // PATCH: odata/Continents(5)
        [Authorize()]
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Continent> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Continent continent = await db.Continents.FindAsync(key);
            if (continent == null)
            {
                return NotFound();
            }

            patch.Patch(continent);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContinentExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(continent);
        }

        // DELETE: odata/Continents(5)
        [Authorize()]
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Continent continent = await db.Continents.FindAsync(key);
            if (continent == null)
            {
                return NotFound();
            }

            db.Continents.Remove(continent);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Continents(5)/Countries
        [EnableQuery]
        public IQueryable<Country> GetCountries([FromODataUri] int key)
        {
            return db.Continents.Where(m => m.ContinentID == key).SelectMany(m => m.Countries);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ContinentExists(int key)
        {
            return db.Continents.Count(e => e.ContinentID == key) > 0;
        }
    }
}
