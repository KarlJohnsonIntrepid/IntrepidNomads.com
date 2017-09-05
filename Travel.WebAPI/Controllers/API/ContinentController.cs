using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;

namespace Travel.WebAPI.Controllers
{
    public class ContinentController : ApiController
    {
        // GET: api/Continent
        public String Get()
        {
            var Continents = AutoMapper.Mapper.Map<List<DAL.Continent>, List<Models.ContinentModel>>(Business.Continent.SelectContinents());
            return JsonConvert.SerializeObject(Continents);
        }

        // GET: api/Continent/5
        public string Get(int id)
        {
           var  Continent = Business.Continent.GetContinentRow(id);

            if (Continent != null) {
	             Models.ContinentModel ContinentDTO = AutoMapper.Mapper.Map<DAL.Continent, Models.ContinentModel>(Continent);
	            return JsonConvert.SerializeObject(ContinentDTO);

            } else {
	            return "Not Found for property " + id;
            }
                    }
        }
}
