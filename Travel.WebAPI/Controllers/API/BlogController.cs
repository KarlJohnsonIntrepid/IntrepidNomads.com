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
    public class BlogController : ApiController
    {
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        // GET api/<controller>
        public string GetValues()
        {
            List<Models.BlogModel> Blog = AutoMapper.Mapper.Map<List<DAL.vBlog>, List<Models.BlogModel>>(Business.Blog.SelectBlogs());
            return JsonConvert.SerializeObject(Blog);
        }

        // GET api/<controller>/5
        public string GetValue(int id)
        {
            var blog = Business.Blog.GetBlogItem(id);

            if (blog != null)
            {
                Models.BlogModel blogDTO = AutoMapper.Mapper.Map<DAL.vBlog, Models.BlogModel>(blog);
                return JsonConvert.SerializeObject(blogDTO);

            }
            else
            {
                return "Not Found for property " + id;
            }
        }

        // POST: api/Blog
        [Authorize()]
        public HttpResponseMessage Post([FromBody] DAL.Blog blog)
        {
            try
            {
                Business.Blog.Insert(blog) ;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Conflict, ex.Message);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // PUT: api/Blog/5
        [Authorize()]
        public HttpResponseMessage Put(int id, [FromBody] DAL.Blog blog)
        {

            try
            {
                Business.Blog.Update(blog, id);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Conflict, ex.Message);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // DELETE: api/Blog/5
        [Authorize()]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
             
                Business.Blog.Delete(id);
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
        [Route("api/blog/count")]
        public string Count()
        {
            return Business.Blog.CountOfBlogs().ToString();
        }

        // GET rpc/BlogRPC/Count
        //[HttpGet()]
        //[Route("api/blog/minimal")]
        //public string Minimal()
        //{

        //    //List<Models.BlogModel> Blog = AutoMapper.Mapper.Map<List<DAL.vBlog>, List<Models.BlogModel>>(Business.Blog.GetBlogsMinimal(false));
        //    //return JsonConvert.SerializeObject(Blog);
        //}

        [HttpGet()]
        [Route("api/blog/includehidden")]
        public string IncludeHidden()
        {
            List<Models.BlogModel> Blog = AutoMapper.Mapper.Map<List<DAL.vBlog>, List<Models.BlogModel>>(Business.Blog.SelectBlogs(false, true));
            return JsonConvert.SerializeObject(Blog);
        }

        [HttpGet()]
        [Route("api/blog/related/{BlogId}/{number}")]
        public string Related(int BlogId, int number)
        {
            List<Models.BlogModel> Blogs = AutoMapper.Mapper.Map<List<DAL.vBlog>, List<Models.BlogModel>>(Business.Blog.SelectRelatedBlogs(BlogId, number));
            return JsonConvert.SerializeObject(Blogs);
        }


        [HttpGet()]
        [Route("api/blog/BlogByTitle/{Destination}/{Category}/{Title}/")]
        public string ByTitle(string Destination, string Category, string Title)
        {

            Models.BlogModel blogDTO = AutoMapper.Mapper.Map<DAL.vBlog, Models.BlogModel>(Business.Blog.SelectBlogsByTitle(Destination, Category, Title, true));
            return JsonConvert.SerializeObject(blogDTO);
       }


        [HttpGet()]
        [Route("api/blog/ByCategory/{CategoryID}/{ReverseDateOrder}/{IncludeHidden}")]
        public string ByCategory(string CategoryID, bool ReverseDateOrder = false, bool IncludeHidden = false)
        {
            List<Models.BlogModel> Blogs = AutoMapper.Mapper.Map<List<DAL.vBlog>, List<Models.BlogModel>>(Business.Blog.SelectBlogsByCategory(CategoryID, ReverseDateOrder, IncludeHidden));
            return JsonConvert.SerializeObject(Blogs);
        }


        [HttpGet()]
        [Route("api/blog/ByCountry/{CountryID}/{IncludeHidden}")]
        public string ByCountry(string CountryID, bool IncludeHidden = false)
        {
            List<Models.BlogModel> Blogs = AutoMapper.Mapper.Map<List<DAL.vBlog>, List<Models.BlogModel>>(Business.Blog.SelectBlogsByCountry(CountryID, IncludeHidden));
            return JsonConvert.SerializeObject(Blogs);
        }

        // GET rpc/BlogRPC/ForDrinks
        [HttpGet()]
        [Route("api/blog/ForDrinks/{IncludeHidden}")]
        public string ForDrinks(bool IncludeHidden = false)
        {
            List<Models.BlogModel> Blogs = AutoMapper.Mapper.Map<List<DAL.vBlog>, List<Models.BlogModel>>(Business.Blog.SelectBlogsForDrinks(IncludeHidden));
            return JsonConvert.SerializeObject(Blogs);
        }

        // GET rpc/BlogRPC/RecentBlogs
        [HttpGet()]
        [Route("api/blog/RecentBlogs/{NumberOfBlogs}/{IncludeHidden}")]
        public string RecentBlogs(int NumberOfBlogs, bool IncludeHidden = false)
        {
            List<Models.BlogModel> Blogs = AutoMapper.Mapper.Map<List<DAL.vBlog>, List<Models.BlogModel>>(Business.Blog.GetRecentBlogs(NumberOfBlogs, IncludeHidden));
            return JsonConvert.SerializeObject(Blogs);
        }

        // GET rpc/BlogRPC/MostRecent
        [HttpGet()]
        [Route("api/blog/MostRecent/{IncludeHidden}")]
        public string MostRecent(bool IncludeHidden = false)
        {
            Models.BlogModel Blogs = AutoMapper.Mapper.Map<DAL.vBlog, Models.BlogModel>(Business.Blog.GetMostRecent(IncludeHidden));
            return JsonConvert.SerializeObject(Blogs);
        }


        // GET rpc/BlogRPC/MostRecent
        [HttpGet()]
        [Route("api/blog/paged/{PageNumber}/{PageSize}/{IncludeHidden}")]
        public string Paged(int PageNumber, int PageSize, bool IncludeHidden = false)
        {
            List<DAL.vBlog> datasource = Business.Blog.SelectBlogs(false, IncludeHidden).Skip((PageNumber * PageSize) - PageSize).Take(PageSize).ToList();

            List<Models.BlogModel> Blog = AutoMapper.Mapper.Map<List<DAL.vBlog>, List<Models.BlogModel>>(datasource);
            return JsonConvert.SerializeObject(Blog);
        }


       

    }
}
