using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using StoreProcedures.PostgreModels;
using StoreProcedures.Source.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StoreProcedures.Controllers
{
    public class HospitalProcedureController : ApiController
    {
        PostgreContext postgreContext = new PostgreContext();

        /// <summary>
        /// Function in charge of recopilating all the procedures of a hospital in the database
        /// </summary>
        /// <param name="hospital">
        /// Hospital id
        /// </param>
        /// <returns>
        /// A list with all the procedures found
        /// </returns>
        [Route("api/Procedure/{hospital:int}")]
        [HttpGet]
        public IEnumerable<ProcedureView> Get(int hospital)
        {
            try
            {
                var hospitalIdParam = new Npgsql.NpgsqlParameter("@hospitalid", hospital);

                var procedures = postgreContext.Procedure
                    .FromSqlRaw("SELECT * from proceduresbyhospital(@hospitalid);", hospitalIdParam)
                    .ToList();

                List<ProcedureView> result = new List<ProcedureView>();

                foreach (Procedure p in procedures)
                {
                    ProcedureView procedureView = new ProcedureView();
                    procedureView.Id = p.Id;
                    procedureView.Name = p.Name;
                    procedureView.Duration = p.Duration;
                    result.Add(procedureView);
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
        /// Function in charge receiving an procedure to store it in the database
        /// </summary>
        /// <param name="procedure">
        /// Procedure to be added
        /// </param>
        [Route("api/Procedure")]
        [HttpPost]
        public void Post(JObject procedure)
        {
            try
            {
                var hospitalIdParameter = new Npgsql.NpgsqlParameter("@hospitalid", (int)procedure.GetValue("HospitalId"));
                var procedureNameParameter = new Npgsql.NpgsqlParameter("@procedurename", (string)procedure.GetValue("Name"));
                var procedureDurationParameter = new Npgsql.NpgsqlParameter("@procedureduration", (int)procedure.GetValue("Duration"));

                var procedures = postgreContext.Database
                    .ExecuteSqlRaw("SELECT addproceduretohospital(@hospitalid, @procedurename, @procedureduration)", hospitalIdParameter, procedureNameParameter, procedureDurationParameter);

                postgreContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
            }
        }

        /// <summary>
        /// Function in charge receiving updated data of an bed equipment
        /// </summary>
        /// <param name="id">
        /// Procedure id
        /// </param>
        /// <param name="procedure">
        /// Procedure to be updated
        /// </param>
        [Route("api/Procedure/{id:int}")]
        [HttpPut]
        public void Put(int id, Procedure procedure)
        {
            try
            {
                var oldProcedure = postgreContext.Procedure
                                    .Where(p => p.Id == id)
                                    .Single();

                oldProcedure.Name = procedure.Name;
                oldProcedure.Duration = procedure.Duration;

                postgreContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
            }
        }

        /// <summary>
        /// Function in charge deleting an hospital procedure
        /// </summary>
        /// <param name="hospitalId">
        /// Hospital id
        /// </param>
        /// <param name="procedureId">
        /// Procedure id
        /// </param>
        [Route("api/Procedure/{hospitalId:int}/{procedureId:int}")]
        [HttpDelete]
        public void Delete(int hospitalId, int procedureId)
        {
            try
            {
                var procedureIdParameter = new Npgsql.NpgsqlParameter("@procedureid", procedureId);
                var hospitalIdParameter = new Npgsql.NpgsqlParameter("@hospitalid", hospitalId);

                var procedures = postgreContext.Database
                    .ExecuteSqlRaw("SELECT deleteprocedure(@procedureid, @hospitalid)", procedureIdParameter, hospitalIdParameter);

                postgreContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
            }
        }
    }
}
