using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using StoreProcedures.PostgreModels;
using StoreProcedures.Source.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StoreProcedures.Controllers
{
    public class ReservationProcedureController : ApiController
    {
        PostgreContext postgreContext = new PostgreContext();

        /// <summary>
        /// Function in charge of recopilating all the patient reservations in the database
        /// </summary>
        /// <param name="ssn">
        /// Patient ssn
        /// </param>
        /// <param name="reservationId">
        /// Reservation id
        /// </param>
        /// <returns>
        /// A list with all the reservations found
        /// </returns>
        [Route("api/ReservationProcedure/{ssn}/{reservationId:int}")]
        [HttpGet]
        public IEnumerable<MedicalHistoryView> Get(string ssn, int reservationId)
        {
            try
            {
                var patientIdParam = new Npgsql.NpgsqlParameter("@patientid", ssn);
                var reservationIdParam = new Npgsql.NpgsqlParameter("@reservationId", reservationId);

                var medicalHistories = postgreContext.MedicalHistory
                    .FromSqlRaw("SELECT * from usp_patient_reservation_procedure(@patientid, @reservationId);", patientIdParam, reservationIdParam)
                    .ToList();

                List<MedicalHistoryView> result = new List<MedicalHistoryView>();

                foreach (MedicalHistory mh in medicalHistories)
                {
                    MedicalHistoryView medicalHistoryView = new MedicalHistoryView();
                    medicalHistoryView.Procedure = mh.Procedure;
                    medicalHistoryView.Startdate = (mh.Date).ToShortDateString();
                    medicalHistoryView.Duration = mh.Duration;
                    result.Add(medicalHistoryView);
                }

                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Function in charge receiving an reservation to store it in the database
        /// </summary>
        /// <param name="reservation">
        /// Reservation to be added
        /// </param>
        [Route("api/ReservationProcedure")]
        [HttpPost]
        public void Post(JObject reservation)
        {
            try
            {
                var reservationIdParameter = new Npgsql.NpgsqlParameter("@reservationid", (int)reservation.GetValue("ReservationId"));
                var procedureNameParameter = new Npgsql.NpgsqlParameter("@procedurename", (string)reservation.GetValue("ProcedureName"));

                var reservations = postgreContext.Database
                    .ExecuteSqlRaw("SELECT usp_insert_reservation(@reservationid, @procedurename)", reservationIdParameter, procedureNameParameter);

                postgreContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
            }
        }

        /// <summary>
        /// Function in charge receiving updated data of a reservation
        /// </summary>
        /// <param name="reservationId">
        /// Reservation id
        /// </param>
        /// <param name="procedure">
        /// Procedure with the updated data
        /// </param>
        [Route("api/ReservationProcedure/{reservationId:int}")]
        [HttpPut]
        public void Put(int reservationId, JObject procedure)
        {
            try
            {
                var reservationIdParameter = new Npgsql.NpgsqlParameter("@reservationid", reservationId);
                var oldProcedureParameter = new Npgsql.NpgsqlParameter("@oldprocedure", (string)procedure.GetValue("OldProcedure"));
                var newProcedureParameter = new Npgsql.NpgsqlParameter("@newprocedure", (string)procedure.GetValue("NewProcedure"));

                var procedures = postgreContext.Database
                    .ExecuteSqlRaw("CALL usp_update_reservation_procedures(@reservationid, @oldprocedure, @newprocedure)", reservationIdParameter, oldProcedureParameter, newProcedureParameter);

                postgreContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
            }
        }

        /// <summary>
        /// Function in charge deleting a reservation
        /// </summary>
        /// <param name="reservationId">
        /// Reservation id
        /// </param>
        /// <param name="procedureName">
        /// Procedure name
        /// </param>
        [Route("api/ReservationProcedure/{reservationId:int}/{procedureName}")]
        [HttpDelete]
        public void Delete(int reservationId, string procedureName)
        {
            try
            {
                var procedureNameParameter = new Npgsql.NpgsqlParameter("@procedurename", procedureName);
                var reservationIdParameter = new Npgsql.NpgsqlParameter("@reservationid", reservationId);

                var procedures = postgreContext.Database
                    .ExecuteSqlRaw("CALL usp_delete_reservation_procedures(@procedurename, @reservationid)", procedureNameParameter, reservationIdParameter);

                postgreContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
            }
        }
    }
}
