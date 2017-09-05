using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace Travel.WebAPI.Controllers
{
    public class CategoryController : ApiController
    {
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        // GET api/<controller>
        public string GetValues()
        {
            List<Models.CategoryModel> Categories = AutoMapper.Mapper.Map<List<DAL.vCategory>, List<Models.CategoryModel>>(Business.Category.SelectCategories());
            return JsonConvert.SerializeObject(Categories);
        }

        // GET api/<controller>/5
        public string GetValue(int id)
        {
            var Category = Business.Category.GetCategoryRow(id);

            if (Category != null)
            {
                Models.CategoryModel CategoryDTO = AutoMapper.Mapper.Map<DAL.vCategory, Models.CategoryModel>(Category);
                return JsonConvert.SerializeObject(CategoryDTO);

            }
            else
            {
                return "Not Found for property " + id;
            }
        }

        // POST: api/Category
        [Authorize()]
        public HttpResponseMessage Post([FromBody] DAL.Category C)
        {
            try
            {
                Business.Category.Insert(C);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Conflict, ex.Message);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // PUT: api/Category/5
        [Authorize()]
        public HttpResponseMessage Put(int id, [FromBody]DAL.Category Category)
        {
            try
            {
                Business.Category.Update(Category, id);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Conflict, ex.Message);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // DELETE: api/Category/5
        [Authorize()]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                Business.Category.Delete(id);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Conflict, ex.Message);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }


        //Other Methods ----------------------------------------------------------------------------------------------------------------------

        // GET rpc/CategoryRPC/NotFeatured

        [Route("api/category/notfeatured")]
        [HttpGet]
        public string NotFeatured()
        {
            List<Models.CategoryModel> Categories = AutoMapper.Mapper.Map<List<DAL.vCategory>, List<Models.CategoryModel>>(Business.Category.SelectCategoriesNotFeatured());
            return JsonConvert.SerializeObject(Categories);
        }

        // GET rpc/CategoryRPC/ForMenu
        [Route("api/category/menu")]
        [HttpGet]
        public string ForMenu()
        {
            List<Models.CategoryModel> Categories = AutoMapper.Mapper.Map<List<DAL.vCategory>, List<Models.CategoryModel>>(Business.Category.SelectCategoriesForMenu());
            return JsonConvert.SerializeObject(Categories);
        }


        // GET rpc/CategoryRPC/BlogFilter
        [Route("api/category/blogfilter")]
        [HttpGet]
        public string BlogFilter()
        {
            List<Models.CategoryModel> Categories = AutoMapper.Mapper.Map<List<DAL.vCategory>, List<Models.CategoryModel>>(Business.Category.SelectCategoriesBlogFilter());
            return JsonConvert.SerializeObject(Categories);
        }

        // GET rpc/CategoryRPC/Featured
        [Route("api/category/featured")]
        [HttpGet]
        public string Featured()
        {
            List<Models.CategoryModel> Categories = AutoMapper.Mapper.Map<List<DAL.vCategory>, List<Models.CategoryModel>>(Business.Category.SelectCategoriesFeatured());
            return JsonConvert.SerializeObject(Categories);
        }

        [Route("api/category/ChildCategories/{CategoryID}/{CountryID}")]
        [HttpGet]
        public string ChildCategories(int CategoryID, int CountryID)
        {
            List<Models.CategoryModel> Categories = AutoMapper.Mapper.Map<List<DAL.vCategory>, List<Models.CategoryModel>>(Business.Category.SelectChildCategories(CategoryID, CountryID));
            return JsonConvert.SerializeObject(Categories);
        }

    }
}
