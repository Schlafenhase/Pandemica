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
        SpecificDelete delete = new SpecificDelete();
        SpecificUpdate update = new SpecificUpdate();

        DatabaseDataHolder connection = new DatabaseDataHolder();

        /// <summary>
        /// Function in charge of recolecting all the regions in the database
        /// </summary>
        /// <returns>
        /// List with all the regions found
        /// </returns>
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

        /// <summary>
        /// Function in charge of recolecting all the region names in the database
        /// </summary>
        /// <returns>
        /// List with all the region names
        /// </returns>
        [Route("api/ProvinceStateRegion/Names")]
        [HttpGet]
        public IEnumerable<string> GetRegionNames()
        {
            connection.openConnection();
            string[] allrecords;
            allrecords = select.makeProvinceStateRegionNamesSelect().ToArray();
            connection.closeConnection();
            return allrecords;
        }

        /// <summary>
        /// Function in charge of searching a region from name
        /// </summary>
        /// <param name="name">
        /// Name of the region
        /// </param>
        /// <returns>
        /// List with the region found
        /// </returns>
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

        /// <summary>
        /// Function in charge of searching a region from a country
        /// </summary>
        /// <param name="name">
        /// Name of the country
        /// </param>
        /// <returns>
        /// List with the region found
        /// </returns>
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

        /// <summary>
        /// Function in charge of inserting a region to the database
        /// </summary>
        /// <param name="provinceStateRegion">
        /// Name of the region
        /// </param>
        [Route("api/ProvinceStateRegion")]
        [HttpPost]
        public void Post(ProvinceStateRegion provinceStateRegion)
        {
            connection.openConnection();
            insert.makeProvinceStateRegionInsert(provinceStateRegion.name, provinceStateRegion.country);
            connection.closeConnection();
            Debug.WriteLine("Inserted");
        }

        /// <summary>
        /// Function in charge of updating a region to the database
        /// </summary>
        /// <param name="id">
        /// Id of the region to be updated
        /// </param>
        /// <param name="provinceStateRegion">
        /// Region with the data
        /// </param>
        [Route("api/ProvinceStateRegion/{id:int}")]
        [HttpPut]
        public void PutProvinceStateRegion(int id, ProvinceStateRegion provinceStateRegion)
        {
            connection.openConnection();
            update.makeSpecificProvinceStateRegionUpdate(id.ToString(), provinceStateRegion.name, provinceStateRegion.country);
            connection.closeConnection();
            Debug.WriteLine("Updated from Country");
        }

        /// <summary>
        /// Function in charge of deleting a region from the database
        /// </summary>
        /// <param name="id">
        /// Id of the region
        /// </param>
        [Route("api/ProvinceStateRegion/{id:int}")]
        [HttpDelete]
        public void DeleteProvinceStateRegion(int id)
        {
            connection.openConnection();
            delete.makeSpecificProvinceStateRegionDelete(id.ToString());
            connection.closeConnection();
            Debug.WriteLine("Deleted from Country");
        }
    }
}
