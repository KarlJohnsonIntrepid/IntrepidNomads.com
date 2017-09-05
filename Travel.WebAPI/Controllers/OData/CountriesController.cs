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
    builder.EntitySet<Country>("Countries");
    builder.EntitySet<Continent>("Continents"); 
    builder.EntitySet<Blog>("Blogs"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class CountriesController : ODataController
    {
        private WebAPIContext db = new WebAPIContext();

        // GET: odata/Countries
        [EnableQuery]
        public IQueryable<Country> GetCountries()
        {
            return db.Countries;
        }

        // GET: odata/Countries(5)
        [EnableQuery]
        public SingleResult<Country> GetCountry([FromODataUri] int key)
        {
            return SingleResult.Create(db.Countries.Where(country => country.CountryID == key));
        }

        // PUT: odata/Countries(5)
        [Authorize()]
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Country> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Country country = await db.Countries.FindAsync(key);
            if (country == null)
            {
                return NotFound();
            }

            patch.Put(country);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(country);
        }

        // POST: odata/Countries
        [Authorize()]
        public async Task<IHttpActionResult> Post(Country country)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Countries.Add(country);
            await db.SaveChangesAsync();

            return Created(country);
        }

        // PATCH: odata/Countries(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Country> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Country country = await db.Countries.FindAsync(key);
            if (country == null)
            {
                return NotFound();
            }

            patch.Patch(country);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(country);
        }

        // DELETE: odata/Countries(5
        [Authorize()]
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Country country = await db.Countries.FindAsync(key);
            if (country == null)
            {
                return NotFound();
            }

            db.Countries.Remove(country);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Countries(5)/Continent
        [EnableQuery]
        public SingleResult<Continent> GetContinent([FromODataUri] int key)
        {
            return SingleResult.Create(db.Countries.Where(m => m.CountryID == key).Select(m => m.Continent));
        }

        // GET: odata/Countries(5)/Blogs
        [EnableQuery]
        public IQueryable<Blog> GetBlogs([FromODataUri] int key)
        {
            return db.Countries.Where(m => m.CountryID == key).SelectMany(m => m.Blogs);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CountryExists(int key)
        {
            return db.Countries.Count(e => e.CountryID == key) > 0;
        }
    }
}
