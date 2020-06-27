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
    public class SanitaryMeasurementsController : ApiController
    {
        GeneralInsert insert = new GeneralInsert();
        GeneralSelect select = new GeneralSelect();
        SpecificSelect specificSelect = new SpecificSelect();
        SpecificDelete delete = new SpecificDelete();
        SpecificUpdate update = new SpecificUpdate();

        DatabaseDataHolder connection = new DatabaseDataHolder();

        [Route("api/SanitaryMeasurements")]
        [HttpGet]
        public IEnumerable<SanitaryMeasurements> Get()
        {
            connection.openConnection();
            SanitaryMeasurements[] allrecords;
            allrecords = select.makeSanitaryMeasurementsSelect().ToArray();
            connection.closeConnection();
            return allrecords;
        }

        [Route("api/SanitaryMeasurements/{id:int}")]
        [HttpGet]
        public IEnumerable<SanitaryMeasurements> Get(int id)
        {
            connection.openConnection();
            SanitaryMeasurements[] allrecords;
            allrecords = specificSelect.makeSpecificSanitaryMeasurementsSelectById(id.ToString()).ToArray();
            connection.closeConnection();
            return allrecords;
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
            connection.openConnection();
            update.makeSpecificSanitaryMeasurementsUpdateById(id.ToString(), sanitaryMeasurements.name, sanitaryMeasurements.description);
            connection.closeConnection();
            Debug.WriteLine("Updated");
        }

        [Route("api/SanitaryMeasurements/{id:int}")]
        [HttpDelete]
        public void Delete(int id)
        {
            connection.openConnection();
            delete.makeSpecificSanitaryMeasurementsDeleteById(id.ToString());
            connection.closeConnection();
            Debug.WriteLine("Deleted");
        }
    }
}
