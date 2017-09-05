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
    builder.EntitySet<UploadedImage>("UploadedImages");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class UploadedImagesController : ODataController
    {
        private WebAPIContext db = new WebAPIContext();

        // GET: odata/UploadedImages
        [EnableQuery]
        public IQueryable<UploadedImage> GetUploadedImages()
        {
            return db.UploadedImages;
        }

        // GET: odata/UploadedImages(5)
        [EnableQuery]
        public SingleResult<UploadedImage> GetUploadedImage([FromODataUri] int key)
        {
            return SingleResult.Create(db.UploadedImages.Where(uploadedImage => uploadedImage.ImageID == key));
        }

        // PUT: odata/UploadedImages(5)
        [Authorize()]
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<UploadedImage> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UploadedImage uploadedImage = await db.UploadedImages.FindAsync(key);
            if (uploadedImage == null)
            {
                return NotFound();
            }

            patch.Put(uploadedImage);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UploadedImageExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(uploadedImage);
        }

        // POST: odata/UploadedImages
        [Authorize()]
        public async Task<IHttpActionResult> Post(UploadedImage uploadedImage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UploadedImages.Add(uploadedImage);
            await db.SaveChangesAsync();

            return Created(uploadedImage);
        }

        // PATCH: odata/UploadedImages(5)
        [AcceptVerbs("PATCH", "MERGE")]
        [Authorize()]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<UploadedImage> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UploadedImage uploadedImage = await db.UploadedImages.FindAsync(key);
            if (uploadedImage == null)
            {
                return NotFound();
            }

            patch.Patch(uploadedImage);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UploadedImageExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(uploadedImage);
        }

        // DELETE: odata/UploadedImages(5)
        [Authorize()]
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            UploadedImage uploadedImage = await db.UploadedImages.FindAsync(key);
            if (uploadedImage == null)
            {
                return NotFound();
            }

            db.UploadedImages.Remove(uploadedImage);
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

        private bool UploadedImageExists(int key)
        {
            return db.UploadedImages.Count(e => e.ImageID == key) > 0;
        }
    }
}
