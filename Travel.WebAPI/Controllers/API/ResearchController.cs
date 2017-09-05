using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Travel.WebAPI.WebAPI.Controllers
{
    [RoutePrefix("api/research")]
    public class ResearchController : ApiController
    {

        //Multiple GET Methods ------------------------------------------------------

        /// <summary>
        /// Looks up some data by ID.
        /// </summary>
        /// <returns>Book</returns>
        [Route("GetBook")]
        [HttpGet]
        public String GetBook()
        {
            return "Book";
        }
        // GET api/Research/GetNotBook
        [Route("GetNotBook")]
        [HttpGet]
        public String GetNotBook()
        {
            return "NotBook";
        }

        //Query String Parameters

        ///api/research/example1?id=2
        [Route("ParameterWithQueryString")]
        [HttpGet]
        public string Get(int id)
        {
            return id.ToString();
        }

        //api/rsearch/example2?id1=1&id2=2&id3=3
        [Route("MultipleParameterWithQueryString")]
        [HttpGet]
        public string GetWith3Parameters(int id1, long id2, double id3)
        {
            return id1.ToString() + id2 + id3;
        }

        //Attribute Routing Parameters

        // http://localhost:49407/api/values/example3/2/3/4
        [Route("AttributeRoutingParams/{id1}/{id2}/{id3}")]
        [HttpGet]
        public string GetWith3ParametersAttributeRouting(int id1, long id2, double id3)
        {
            return id1.ToString() + id2 + id3;
        }


    }
}