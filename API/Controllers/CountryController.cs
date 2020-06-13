using API.Source.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class CountryController : ApiController
    {
        [Route("api/Country")]
        [HttpGet]
        public IEnumerable<Country> Get()
        {
            Country c1 = new Country("Costa Rica", "America");
            Country c2 = new Country("Nicaragua", "America");
            return new Country[] { c1, c2 };
        }

        [Route("api/Country/{name}")]
        [HttpGet]
        public string Get(string name)
        {
            return name;
        }

        [Route("api/Country")]
        [HttpPost]
        public void Post(Country country)
        {
            Debug.WriteLine("Inserted");
        }

        [Route("api/Country/{name}")]
        [HttpPut]
        public void Put(string name, Country country)
        {
            Debug.WriteLine("Updated");
        }

        [Route("api/Country/{name}")]
        [HttpDelete]
        public void Delete(string name)
        {
            Debug.WriteLine("Deleted");
        }
    }
}
