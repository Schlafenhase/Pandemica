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
    public class HospitalController : ApiController
    {
        GeneralInsert insert = new GeneralInsert();
        GeneralSelect select = new GeneralSelect();
        SpecificSelect specificSelect = new SpecificSelect();
        SpecificDelete delete = new SpecificDelete();
        SpecificUpdate update = new SpecificUpdate();

        DatabaseDataHolder connection = new DatabaseDataHolder();

        /// <summary>
        /// Function in charge of recopilating all the hospitals in the database
        /// </summary>
        /// <returns>
        /// A list with all the hospitals found
        /// </returns>
        [Route("api/Hospital")]
        [HttpGet]
        public IEnumerable<Hospital> Get()
        {
            connection.openConnection();
            Hospital[] allrecords;
            allrecords = select.makeHospitalSelect().ToArray();
            connection.closeConnection();
            return allrecords;
        }

        /// <summary>
        /// Function in charge of searching an hospital through the id
        /// </summary>
        /// <param name="id">
        /// Id of the hospital
        /// </param>
        /// <returns>
        /// A list with the found hospital
        /// </returns>
        [Route("api/Hospital/{id:int}")]
        [HttpGet]
        public IEnumerable<Hospital> GetHospitalFromID(int id)
        {
            connection.openConnection();
            Hospital[] allrecords;
            allrecords = specificSelect.makeSpecificHospitalSelectById(id.ToString()).ToArray();
            connection.closeConnection();
            return allrecords;
        }

        /// <summary>
        /// Function in charge of searching an hospital through the email
        /// </summary>
        /// <param name="email">
        /// Email of the hospital
        /// </param>
        /// <returns>
        /// A list with the hospital found
        /// </returns>
        [Route("api/Hospital/{email}")]
        [HttpGet]
        public IEnumerable<Hospital> GetHospitalFromEMail(string email)
        {
            connection.openConnection();
            Hospital[] allrecords;
            allrecords = specificSelect.makeSpecificHospitalSelectByEMail(email).ToArray();
            connection.closeConnection();
            return allrecords;
        }

        /// <summary>
        /// Function in charge of inserting an hospital to the database
        /// </summary>
        /// <param name="hospital">
        /// Hospital to be added
        /// </param>
        [Route("api/Hospital")]
        [HttpPost]
        public void Post(Hospital hospital)
        {
            connection.openConnection();
            insert.makeHospitalInsert(hospital.name, hospital.phone.ToString(), hospital.managerName, hospital.capacity.ToString(), hospital.icuCapacity.ToString(), hospital.country, hospital.region, hospital.eMail);
            connection.closeConnection();
            Debug.WriteLine("Inserted");
        }

        /// <summary>
        /// Function in charge of searching an hospital through the email
        /// </summary>
        /// <param name="hospital">
        /// Hospital with the email
        /// </param>
        /// <returns>
        /// Hospital found
        /// </returns>
        [Route("api/Hospital/Email")]
        [HttpPost]
        public IEnumerable<Hospital> GetHospitalFromName(Hospital hospital)
        {
            connection.openConnection();
            Hospital[] allrecords;
            allrecords = specificSelect.makeSpecificHospitalSelectByEMail(hospital.eMail).ToArray();
            connection.closeConnection();
            return allrecords;
        }

        /// <summary>
        /// Function in charge updating an hospital
        /// </summary>
        /// <param name="id">
        /// Id of the hospital to be updated
        /// </param>
        /// <param name="hospital">
        /// Hospital with the updated data
        /// </param>
        [Route("api/Hospital/{id:int}")]
        [HttpPut]
        public void Put(int id, Hospital hospital)
        {
            connection.openConnection();
            update.makeSpecificHospitalUpdate(id.ToString(), hospital.name, hospital.phone.ToString(), hospital.managerName, hospital.capacity.ToString(), hospital.icuCapacity.ToString(), hospital.country, hospital.region, hospital.eMail);
            connection.closeConnection();
            Debug.WriteLine("Updated");
        }

        /// <summary>
        /// Function in charge of deleting an hospital
        /// </summary>
        /// <param name="id">
        /// Id of the hospital to be deleted
        /// </param>
        [Route("api/Hospital/{id:int}")]
        [HttpDelete]
        public void Delete(int id)
        {
            connection.openConnection();
            delete.makeSpecificHospitalDelete(id.ToString());
            connection.closeConnection();
            Debug.WriteLine("Deleted");
        }
    }
}
