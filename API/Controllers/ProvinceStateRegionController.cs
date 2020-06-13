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
    public class ProvinceStateRegionController : ApiController
    {
        [Route("api/ProvinceStateRegion")]
        [HttpGet]
        public IEnumerable<ProvinceStateRegion> Get()
        {
            ProvinceStateRegion psr1 = new ProvinceStateRegion("Hola", "Argentina");
            ProvinceStateRegion psr2 = new ProvinceStateRegion("Aloh", "Bolivia");
            return new ProvinceStateRegion[] { psr1, psr2 };
        }

        [Route("api/ProvinceStateRegion/Name/{name}")]
        [HttpGet]
        public string GetProvinceStateRegionFromName(string name)
        {
            return name;
        }

        [Route("api/ProvinceStateRegion/Country/{name}")]
        [HttpGet]
        public string GetProvinceStateRegionFromCountry(string name)
        {
            return name;
        }

        [Route("api/ProvinceStateRegion")]
        [HttpPost]
        public void Post(ProvinceStateRegion provinceStateRegion)
        {
            Debug.WriteLine("Inserted");
        }

        [Route("api/ProvinceStateRegion/Name/{name}")]
        [HttpPut]
        public void PutProvinceStateRegionFromName(string name, ProvinceStateRegion provinceStateRegion)
        {
            Debug.WriteLine("Updated from name");
        }

        [Route("api/ProvinceStateRegion/Country/{name}")]
        [HttpPut]
        public void PutProvinceStateRegionFromCountry(string name, ProvinceStateRegion provinceStateRegion)
        {
            Debug.WriteLine("Updated from country");
        }

        [Route("api/ProvinceStateRegion/Name/{name}")]
        [HttpDelete]
        public void DeleteProvinceStateRegionFromName(string name)
        {
            Debug.WriteLine("Deleted from name");
        }

        [Route("api/ProvinceStateRegion/Country/{name}")]
        [HttpDelete]
        public void DeleteProvinceStateRegionFromCountry(string name)
        {
            Debug.WriteLine("Deleted from country");
        }
    }
}
