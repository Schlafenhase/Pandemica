using DBManager.PostgreModels;
using DBManager.Source.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DBManager.Controllers
{
    public class HealthWorkerController : ApiController
    {
        PostgreContext postgreContext = new PostgreContext();

        /// <summary>
        /// Function in charge of recopilating all the health workers in the database
        /// </summary>
        /// <param name="hospital">
        /// Id of the hospital
        /// </param>
        /// <returns>
        /// A list with all the hospitals found
        /// </returns>
        [Route("api/HealthWorker/{hospital:int}")]
        [HttpGet]
        public IEnumerable<HealthWorkerView> Get(int hospital)
        {
            try
            {
                var healthWorkers = postgreContext.HealthWorker
                .Where(hw => hw.HospitalId == hospital)
                .Select(hw => new HealthWorkerView
                {
                    Ssn = hw.Ssn,
                    Fname = hw.Fname,
                    Lname = hw.Lname,
                    Phone = hw.Phone,
                    Birthdate = (hw.Birthdate).ToShortDateString(),
                    Role = hw.Role,
                    Sex = hw.Sex,
                    Email = hw.Email,
                    Address = hw.Address,
                    Startdate = (hw.Startdate).ToShortDateString()
                })
                .ToList();
                return healthWorkers;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Function in charge of returning a health worker based on an email
        /// </summary>
        /// <param name="healthworker">
        /// Health worker with the email
        /// </param>
        /// <returns>
        /// A list with the health worker found
        /// </returns>
        [Route("api/HealthWorker/Email")]
        [HttpPost]
        public JObject GetPatientFromEmail(JObject healthworker)
        {
            try
            {
                var postgreHealthWorkers = postgreContext.HealthWorker
                .Where(hw => hw.Email == (string)healthworker.GetValue("Email"));

                HealthWorker healthWorker = null;

                foreach (HealthWorker hw in postgreHealthWorkers)
                {
                    healthWorker = hw;
                }

                if (healthWorker == null)
                {
                    return null;
                }
                else if (healthWorker.Role != "Doctor")
                {
                    return null;
                }
                else
                {
                    var foundPatient = new JObject
                    {
                        new JProperty("Ssn", healthWorker.Ssn),
                        new JProperty("FirstName", healthWorker.Fname),
                        new JProperty("LastName", healthWorker.Lname),
                        new JProperty("Phone", healthWorker.Phone),
                        new JProperty("BirthDate",((DateTime) healthWorker.Birthdate).ToShortDateString()),
                        new JProperty("Role", healthWorker.Role),
                        new JProperty("HospitalId", healthWorker.HospitalId),
                        new JProperty("Address", healthWorker.Address),
                        new JProperty("StartDate", healthWorker.Startdate),
                        new JProperty("Sex", (healthWorker.Sex).ToString()),
                        new JProperty("Email", healthWorker.Email)
                    };

                    return foundPatient;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Function in charge receiving a health worker to store it in the database
        /// </summary>
        /// <param name="healthWorker">
        /// Health worker to be added
        /// </param>
        [Route("api/HealthWorker")]
        [HttpPost]
        public void Post(HealthWorker healthWorker)
        {
            try
            {
                postgreContext.Add(healthWorker);
                postgreContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
            }
        }

        /// <summary>
        /// Function in charge receiving updated data of a health worker
        /// </summary>
        /// <param name="ssn">
        /// Ssn of a health worker
        /// </param>
        /// <param name="healthWorker">
        /// Health worker with the data to be updated
        /// </param>
        [Route("api/HealthWorker/{ssn}")]
        [HttpPut]
        public void Put(string ssn, HealthWorker healthWorker)
        {
            try
            {
                var oldHealthWorker = postgreContext.HealthWorker
                .Where(hw => hw.Ssn == ssn)
                .Single();

                oldHealthWorker.Fname = healthWorker.Fname;
                oldHealthWorker.Lname = healthWorker.Lname;
                oldHealthWorker.Phone = healthWorker.Phone;
                oldHealthWorker.Birthdate = healthWorker.Birthdate;
                oldHealthWorker.Role = healthWorker.Role;
                oldHealthWorker.Sex = healthWorker.Sex;
                oldHealthWorker.Email = healthWorker.Email;
                oldHealthWorker.Address = healthWorker.Address;
                oldHealthWorker.Startdate = healthWorker.Startdate;

                postgreContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
            }
        }

        /// <summary>
        /// Function in charge deleting a health worker
        /// </summary>
        /// <param name="ssn">
        /// Ssn of the health worker
        /// </param>
        [Route("api/HealthWorker/{ssn}")]
        [HttpDelete]
        public void Delete(string ssn)
        {
            try
            {
                postgreContext.Remove(postgreContext.HealthWorker.Single(hw => hw.Ssn == ssn));
                postgreContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
            }
        }
    }
}
