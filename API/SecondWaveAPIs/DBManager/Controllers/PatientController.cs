using DBManager.MSSQLModels;
using DBManager.PostgreModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DBManager.Controllers
{
    public class PatientController : ApiController
    {
        MSSQLContext mSSQLContext = new MSSQLContext();
        PostgreContext postgreContext = new PostgreContext();

        /// <summary>
        /// Function in charge of returning a patient from an email
        /// </summary>
        /// <param name="patient">
        /// Patient with the email
        /// </param>
        /// <returns>
        /// Patient found
        /// </returns>
        [Route("api/Patient/Email")]
        [HttpPost]
        public JObject GetPatientFromEmail(JObject patient)
        {
            try
            {
                var postgrePatients = postgreContext.Patient
                .Where(p => p.Email == (string)patient.GetValue("Email"));

                PostgreModels.Patient postgrePatient = null;

                foreach (PostgreModels.Patient p in postgrePatients)
                {
                    postgrePatient = p;
                }

                if (postgrePatient != null)
                {
                    var mssqlPatients = mSSQLContext.Patient
                    .Where(p => p.Ssn == postgrePatient.Ssn);

                    MSSQLModels.Patient mssqlPatient = null;

                    foreach (MSSQLModels.Patient p in mssqlPatients)
                    {
                        mssqlPatient = p;
                    }

                    var foundPatient = new JObject
                    {
                        new JProperty("Ssn", mssqlPatient.Ssn),
                        new JProperty("FirstName", mssqlPatient.FirstName),
                        new JProperty("LastName", mssqlPatient.LastName),
                        new JProperty("BirthDate",((DateTime) mssqlPatient.BirthDate).ToShortDateString()),
                        new JProperty("Hospitalized", mssqlPatient.Hospitalized),
                        new JProperty("Icu", mssqlPatient.Icu),
                        new JProperty("Country", mssqlPatient.Country),
                        new JProperty("Region", mssqlPatient.Region),
                        new JProperty("Nationality", mssqlPatient.Nationality),
                        new JProperty("Hospital", mssqlPatient.Hospital),
                        new JProperty("Sex", mssqlPatient.Sex),
                        new JProperty("Email", postgrePatient.Email)
                    };

                    return foundPatient;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
                return null;
            }  
        }

        /// <summary>
        /// Function in charge receiving a patient to store it in the database
        /// </summary>
        /// <param name="patient">
        /// Patient to be added
        /// </param>
        [Route("api/Patient")]
        [HttpPost]
        public void PostWithHospitalName(JObject patient)
        {
            try
            {
                string hospitalName = (string)patient.GetValue("Hospital");
                var hospitals = mSSQLContext.Hospital
                    .Where(h => h.Name == hospitalName);

                int hospitalId = -1;

                foreach (MSSQLModels.Hospital h in hospitals)
                {
                    hospitalId = h.Id;
                }

                MSSQLModels.Patient mssqlPatient = new MSSQLModels.Patient()
                {
                    Ssn = (string)patient.GetValue("Ssn"),
                    FirstName = (string)patient.GetValue("FirstName"),
                    LastName = (string)patient.GetValue("LastName"),
                    BirthDate = (DateTime)patient.GetValue("BirthDate"),
                    Hospitalized = (bool)patient.GetValue("Hospitalized"),
                    Icu = (bool)patient.GetValue("Icu"),
                    Country = (string)patient.GetValue("Country"),
                    Region = (string)patient.GetValue("Region"),
                    Nationality = (string)patient.GetValue("Nationality"),
                    Hospital = hospitalId,
                    Sex = (string)patient.GetValue("Sex")
                };

                PostgreModels.Patient postgrePatient = new PostgreModels.Patient()
                {
                    Ssn = (string)patient.GetValue("Ssn"),
                    Email = (string)patient.GetValue("Email")
                };

                mSSQLContext.Add(mssqlPatient);
                mSSQLContext.SaveChanges();

                postgreContext.Add(postgrePatient);
                postgreContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
            }
        }

        /// <summary>
        /// Function in charge receiving a patient to store it in the database
        /// </summary>
        /// <param name="patient">
        /// Patient to be added
        /// </param>
        [Route("api/Patient/Id")]
        [HttpPost]
        public void PostWithHospitalId(JObject patient)
        {
            try
            {
                MSSQLModels.Patient mssqlPatient = new MSSQLModels.Patient()
                {
                    Ssn = (string)patient.GetValue("Ssn"),
                    FirstName = (string)patient.GetValue("FirstName"),
                    LastName = (string)patient.GetValue("LastName"),
                    BirthDate = (DateTime)patient.GetValue("BirthDate"),
                    Hospitalized = (bool)patient.GetValue("Hospitalized"),
                    Icu = (bool)patient.GetValue("Icu"),
                    Country = (string)patient.GetValue("Country"),
                    Region = (string)patient.GetValue("Region"),
                    Nationality = (string)patient.GetValue("Nationality"),
                    Hospital = (int)patient.GetValue("Hospital"),
                    Sex = (string)patient.GetValue("Sex")
                };

                PostgreModels.Patient postgrePatient = new PostgreModels.Patient()
                {
                    Ssn = (string)patient.GetValue("Ssn"),
                    Email = (string)patient.GetValue("Email")
                };

                mSSQLContext.Add(mssqlPatient);
                mSSQLContext.SaveChanges();

                postgreContext.Add(postgrePatient);
                postgreContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
            }
        }
    }
}
