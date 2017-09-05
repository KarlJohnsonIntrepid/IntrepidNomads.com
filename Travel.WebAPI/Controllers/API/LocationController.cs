using System;
using System.Net;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Travel.WebAPI.Controllers.API {

    public class LocationController : ApiController
    {


        private JavaScriptSerializer jss = new JavaScriptSerializer();
        // GET api/<controller>
        public string GetValues()
        {

            List<Models.LocationModel> Locations = AutoMapper.Mapper.Map<List<DAL.Location>, List<Models.LocationModel>>(Business.Location.SelectLocations());
            return JsonConvert.SerializeObject(Locations);

        }


        // GET api/location/Graph
        [Route("api/location/Graph")]
        public string GetGraphTable()
        {

            dynamic googleDataTable = new Bortosky.Google.Visualization.GoogleDataTable(Business.Location.SelectLocationsForMap());
            string Json = googleDataTable.GetJson();
            return Json;
        }

        // GET api/<controller>/5
        public string GetValue(int id)
        {

            if (id == 0)
            {
                dynamic googleDataTable = new Bortosky.Google.Visualization.GoogleDataTable(Business.Location.SelectLastKnownLocationForMap());
                string Json = googleDataTable.GetJson();

                return Json;
            }

            return "Not Configured";
        }

        // POST api/<controller>
        [Authorize()]
        public HttpResponseMessage PostValue([FromBody()]  DAL.Location Location)
        {

            try
            {
                if (!Business.Location.EntryExistsForToday(Location))
                    Business.Location.Insert(Location);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Conflict, ex.Message);
            }

          return  Request.CreateResponse(HttpStatusCode.OK);

        }

        // DELETE: api/<controller>/5
        [Authorize()]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                Business.Location.Delete(id);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Conflict, ex.Message);
            }

            return Request.CreateResponse(HttpStatusCode.OK);

        }

        // PUT api/<controller>/5
        [Authorize()]
        public HttpResponseMessage Put(int id, [FromBody] DAL.Location location)
        {
            try
            {
                Business.Location.Update(location, id);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Conflict, ex.Message);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // GET api/location/lastknown
        [HttpGet()]
        [Route("api/location/lastknown")]
        public string LastKnown()
        {
            return Business.Location.SetLastKnownLocation();
        }
    }
}