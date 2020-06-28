using API.Source.Entities;
using API.Source.Server_Connections;
using API.Source.Server_Connections.Specific_Selects;
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
        GeneralInsert insert = new GeneralInsert();
        GeneralSelect select = new GeneralSelect();
        SpecificSelect specificSelect = new SpecificSelect();

        DatabaseDataHolder connection = new DatabaseDataHolder();

        [Route("api/ProvinceStateRegion")]
        [HttpGet]
        public IEnumerable<ProvinceStateRegion> Get()
        {
            connection.openConnection();
            ProvinceStateRegion[] allrecords;
            allrecords = select.makeProvinceStateRegionSelect().ToArray();
            connection.closeConnection();
            return allrecords;
        }

        [Route("api/ProvinceStateRegion/Name/{name}")]
        [HttpGet]
        public IEnumerable<ProvinceStateRegion> GetProvinceStateRegionFromName(string name)
        {
            connection.openConnection();
            ProvinceStateRegion[] allrecords;
            allrecords = specificSelect.makeSpecificProvinceStateRegionSelectByName(name).ToArray();
            connection.closeConnection();
            return allrecords;
        }

        [Route("api/ProvinceStateRegion/Country/{name}")]
        [HttpGet]
        public IEnumerable<ProvinceStateRegion> GetProvinceStateRegionFromCountry(string name)
        {
            connection.openConnection();
            ProvinceStateRegion[] allrecords;
            allrecords = specificSelect.makeSpecificProvinceStateRegionSelectByCountry(name).ToArray();
            connection.closeConnection();
            return allrecords;
        }

        [Route("api/ProvinceStateRegion")]
        [HttpPost]
        public void Post(ProvinceStateRegion provinceStateRegion)
        {
            connection.openConnection();
            insert.makeProvinceStateRegionInsert(provinceStateRegion.name, provinceStateRegion.country);
            connection.closeConnection();
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
