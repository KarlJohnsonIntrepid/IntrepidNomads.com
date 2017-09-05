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
    builder.EntitySet<vUploadedImage>("vUploadedImages");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class vUploadedImagesController : ODataController
    {
        private WebAPIContext db = new WebAPIContext();

        // GET: odata/vUploadedImages
        [EnableQuery]
        public IQueryable<vUploadedImage> GetvUploadedImages()
        {
            return db.vUploadedImages;
        }

        // GET: odata/vUploadedImages(5)
        [EnableQuery]
        public SingleResult<vUploadedImage> GetvUploadedImage([FromODataUri] int key)
        {
            return SingleResult.Create(db.vUploadedImages.Where(vUploadedImage => vUploadedImage.ImageID == key));
        }

        // PUT: odata/vUploadedImages(5)
        [Authorize()]
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<vUploadedImage> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            vUploadedImage vUploadedImage = await db.vUploadedImages.FindAsync(key);
            if (vUploadedImage == null)
            {
                return NotFound();
            }

            patch.Put(vUploadedImage);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!vUploadedImageExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(vUploadedImage);
        }

        // POST: odata/vUploadedImages
        [Authorize()]
        public async Task<IHttpActionResult> Post(vUploadedImage vUploadedImage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.vUploadedImages.Add(vUploadedImage);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (vUploadedImageExists(vUploadedImage.ImageID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(vUploadedImage);
        }

        // PATCH: odata/vUploadedImages(5)
        [AcceptVerbs("PATCH", "MERGE")]
        [Authorize()]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<vUploadedImage> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            vUploadedImage vUploadedImage = await db.vUploadedImages.FindAsync(key);
            if (vUploadedImage == null)
            {
                return NotFound();
            }

            patch.Patch(vUploadedImage);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!vUploadedImageExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(vUploadedImage);
        }

        // DELETE: odata/vUploadedImages(5)
        [Authorize()]
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            vUploadedImage vUploadedImage = await db.vUploadedImages.FindAsync(key);
            if (vUploadedImage == null)
            {
                return NotFound();
            }

            db.vUploadedImages.Remove(vUploadedImage);
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

        private bool vUploadedImageExists(int key)
        {
            return db.vUploadedImages.Count(e => e.ImageID == key) > 0;
        }
    }
}
