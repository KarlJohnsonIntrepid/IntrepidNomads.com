using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace Travel.WebAPI.Controllers.API
{
    public class ImageController : ApiController
    {
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        // GET api/<controller>
        public string Get()
        {
            List<Models.ImageModel> Image = AutoMapper.Mapper.Map<List<DAL.vUploadedImage>, List<Models.ImageModel>>(Business.ImageUpload.SelectImages());
            return JsonConvert.SerializeObject( Image );
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            var image = Business.ImageUpload.GetImageRow(id);

            if (image != null)
            {
                Models.ImageModel imageDTO = AutoMapper.Mapper.Map<DAL.vUploadedImage, Models.ImageModel>(image);
                return JsonConvert.SerializeObject(imageDTO);

            }
            else
            {
                return "Not Found for property " + id;
            }
        }

        // POST api/<controller>
        [Authorize()]
        public HttpResponseMessage Post([FromBody]DAL.UploadedImage Image)
        {
            try
            {
                Business.ImageUpload.Insert(Image);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Conflict, ex.Message);
            }

            return Request.CreateResponse(HttpStatusCode.OK);

        }

        // PUT api/<controller>/5
        [Authorize()]
        public HttpResponseMessage Put(int id, [FromBody]DAL.UploadedImage image)
        {
            try
            {
                Business.ImageUpload.Update(image, id);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Conflict, ex.Message);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // DELETE api/<controller>/5
        [Authorize()]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                Business.ImageUpload.Delete(id);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Conflict, ex.Message);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        //Other RPC methods

        // GET rpc/BlogRPC/Count
        [HttpGet()]
        [Route("api/image/selectImagesByBlog/{BlogID}")]
        public string SelectImagesByBlog(string BlogID)
        {
            List<Models.ImageModel> Image = AutoMapper.Mapper.Map<List<DAL.vUploadedImage>, List<Models.ImageModel>>(Business.ImageUpload.SelectImagesByBlog(BlogID));
            return JsonConvert.SerializeObject(Image);
        }


        // GET api/image/clear
        [HttpGet()]
        [Route("api/image/clear")]
        public void ClearImageCAche()
        {
            Business.ImageUpload.UpdateCache();
        }
    }
}