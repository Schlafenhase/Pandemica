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
    public class ReservationController : ApiController
    {
        PostgreContext postgreContext = new PostgreContext();

        /// <summary>
        /// Function in charge of recopilating all the patient reservations in the database
        /// </summary>
        /// <param name="ssn">
        /// Patient ssn
        /// </param>
        /// <returns>
        /// A list with all the reservations found
        /// </returns>
        [Route("api/Reservation/{ssn:int}")]
        [HttpGet]
        public IEnumerable<ReservationView> Get(string ssn)
        {
            try
            {
                var patientSsnParam = new Npgsql.NpgsqlParameter("@patientid", ssn);

                var reservations = postgreContext.ReservationS
                    .FromSqlRaw("SELECT * from usp_patient_reservation_duration(@patientid);", patientSsnParam)
                    .ToList();

                List<ReservationView> result = new List<ReservationView>();

                foreach (Source.Entities.Reservation r in reservations)
                {
                    ReservationView reservationView = new ReservationView();
                    reservationView.Reservation = r.ReservationId;
                    reservationView.Startdate = (r.Startdate).ToShortDateString();
                    reservationView.Finaldate = (r.Finaldate).ToShortDateString();
                    result.Add(reservationView);
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
        [Route("api/Reservation")]
        [HttpPost]
        public void Post(JObject reservation)
        {
            try
            {
                var patientSsnParameter = new Npgsql.NpgsqlParameter("@patientssn", (string)reservation.GetValue("PatientSsn"));
                var reservationDateParameter = new Npgsql.NpgsqlParameter("@reservationdate", (string)reservation.GetValue("ReservationDate"));
                var hospitalIdParamenter = new Npgsql.NpgsqlParameter("@hospitalId", (int)reservation.GetValue("HospitalId"));

                var reservations = postgreContext.Database
                    .ExecuteSqlRaw("SELECT usp_make_reservation(@patientssn, @reservationdate, @hospitalId)", patientSsnParameter, reservationDateParameter, hospitalIdParamenter);

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
        /// <param name="reservation">
        /// Reservation to be updated
        /// </param>
        [Route("api/Reservation/{reservationId:int}")]
        [HttpPut]
        public void Put(int reservationId, JObject reservation)
        {
            try
            {
                var hospitalIdParameter = new Npgsql.NpgsqlParameter("@hospitalid", (int)reservation.GetValue("HospitalId"));
                var reservationIdParameter = new Npgsql.NpgsqlParameter("@reservationid", reservationId);
                var reservationDateParameter = new Npgsql.NpgsqlParameter("@reservationdate", (string)reservation.GetValue("ReservationDate"));

                var procedures = postgreContext.Database
                    .ExecuteSqlRaw("SELECT usp_update_reservation(@hospitalid, @reservationid, @reservationdate)", hospitalIdParameter, reservationIdParameter, reservationDateParameter);

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
        [Route("api/Reservation/{reservationId:int}")]
        [HttpDelete]
        public void Delete(int reservationId)
        {
            try
            {
                postgreContext.Remove(postgreContext.Reservation.Single(r => r.Id == reservationId));
                postgreContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
            }
        }
    }
}
