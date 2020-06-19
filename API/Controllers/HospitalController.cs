using API.Source.Entities;
using API.Source.Server_Connections;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class HospitalController : ApiController
    {
        General_Insert insert = new General_Insert();
        DatabaseDataHolder connection = new DatabaseDataHolder();

        [Route("api/Hospital")]
        [HttpGet]
        public IEnumerable<Hospital> Get()
        {
            Hospital h1 = new Hospital(1, "Salvatierra", 76755443, "Kevin", 100, 30, "Rusia", "Moscow");
            Hospital h2 = new Hospital(2, "Calderon Guardia", 6565323, "Jose", 50, 5, "Irak", "Por donde tiran bombas");
            return new Hospital[] { h1, h2 };
        }

        [Route("api/Hospital/{id:int}")]
        [HttpGet]
        public int Get(int id)
        {
            return id;
        }

        [Route("api/Hospital")]
        [HttpPost]
        public void Post(Hospital hospital)
        {
            connection.openConnection();
            insert.makeHospitalInsert(hospital.id.ToString(), hospital.name, hospital.phone.ToString(), hospital.managerName, hospital.capacity.ToString(), hospital.icuCapacity.ToString(), hospital.country, hospital.region);
            connection.closeConnection();
            Debug.WriteLine("Inserted");
        }

        [Route("api/Hospital/{id:int}")]
        [HttpPut]
        public void Put(int id, Hospital hospital)
        {
            Debug.WriteLine("Updated");
        }

        [Route("api/Hospital/{id:int}")]
        [HttpDelete]
        public void Delete(int id)
        {
            Debug.WriteLine("Deleted");
        }
    }
}
