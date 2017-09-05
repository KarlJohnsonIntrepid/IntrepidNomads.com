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
    public class CountryController : ApiController
    {
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        // GET api/<controller>
        public string GetValues()
        {
            List<Models.CountryModel> Country = AutoMapper.Mapper.Map<List<DAL.vCountry>, List<Models.CountryModel>>(Business.Country.SelectCountries());
            return JsonConvert.SerializeObject(Country);
        }

        // GET api/<controller>/5
        public string GetValue(int id)
        {
            dynamic Country = Business.Country.GetCountryRow(id);

            if (Country != null)
            {
                Models.CountryModel CountryDTO = AutoMapper.Mapper.Map<DAL.vCountry, Models.CountryModel>(Country);
                return JsonConvert.SerializeObject(Country);

            }
            else
            {
                return "Not Found for property " + id;
            }
        }




        // POST: api/Country
        [Authorize()]
        public HttpResponseMessage Post([FromBody]DAL.Country C)
        {
            try
            {
                Business.Country.Insert(C);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Conflict, ex.Message);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // PUT: api/Country/5
        [Authorize()]
        public HttpResponseMessage Put(int id, [FromBody]DAL.Country Country)
        {
            try
            {
                Business.Country.Update(Country, id);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Conflict, ex.Message);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // DELETE: api/Country/5
        [Authorize()]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                Business.Country.Delete(id);
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.Conflict, ex.Message);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }


        //Other Methods

        // GET rpc/CountryRPC/NotNA
        [HttpGet()]
        [Route("api/country/NotNA")]
        public string NotNA()
        {
            List<Models.CountryModel> Countries = AutoMapper.Mapper.Map<List<DAL.vCountry>, List<Models.CountryModel>>(Business.Country.SelectCountriesNotNA());
            return JsonConvert.SerializeObject(Countries);

        }


        [HttpGet()]
        [Route("api/country/OrderByContinent")]
        public string OrderByContinent()
        {
            List<Models.CountryModel> Countries = AutoMapper.Mapper.Map<List<DAL.vCountry>, List<Models.CountryModel>>(Business.Country.SelectCountriesOrderByContinent());
            return JsonConvert.SerializeObject(Countries);
        }


        [HttpGet()]
        [Route("api/country/GetCountries/{numberofblogs}")]
        public string GetCountries(int NumberOfBlogs)
        {
            List<Models.CountryModel> Countries = AutoMapper.Mapper.Map<List<DAL.vCountry>, List<Models.CountryModel>>(Business.Country.GetCountries(NumberOfBlogs));
            return JsonConvert.SerializeObject(Countries);
        }


        [Route("api/country/orderbycontinent/{numberofblogs}")]
        [HttpGet]
        public string GetCountriesWithInfo(int NumberOfBlogs)
        {
            List<Models.CountryModel> Countries = AutoMapper.Mapper.Map<List<DAL.vCountry>, List<Models.CountryModel>>(Business.Country.GetCountriesWithInfo(NumberOfBlogs));
            return JsonConvert.SerializeObject(Countries);
        }
    }
}
