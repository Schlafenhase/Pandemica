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
    public class SanitaryMeasurementsController : ApiController
    {
        General_Insert insert = new General_Insert();
        DatabaseDataHolder connection = new DatabaseDataHolder();

        [Route("api/SanitaryMeasurements")]
        [HttpGet]
        public IEnumerable<SanitaryMeasurements> Get()
        {
            SanitaryMeasurements sp1 = new SanitaryMeasurements(1, "Nose", "No salga de casa necio");
            SanitaryMeasurements sp2 = new SanitaryMeasurements(2, "Prueba aaaaaaa", "Sinceramente nose");
            return new SanitaryMeasurements[] { sp1, sp2 };
        }

        [Route("api/SanitaryMeasurements/{id:int}")]
        [HttpGet]
        public int Get(int id)
        {
            return id;
        }

        [Route("api/SanitaryMeasurements")]
        [HttpPost]
        public void Post(SanitaryMeasurements sanitaryMeasurements)
        {
            connection.openConnection();
            insert.makeSanitaryMeasurementsInsert(sanitaryMeasurements.id.ToString(), sanitaryMeasurements.name, sanitaryMeasurements.description);
            connection.closeConnection();
            Debug.WriteLine("Inserted");
        }

        [Route("api/SanitaryMeasurements/{id:int}")]
        [HttpPut]
        public void Put(int id, SanitaryMeasurements sanitaryMeasurements)
        {
            Debug.WriteLine("Updated");
        }

        [Route("api/SanitaryMeasurements/{id:int}")]
        [HttpDelete]
        public void Delete(int id)
        {
            Debug.WriteLine("Deleted");
        }
    }
}
