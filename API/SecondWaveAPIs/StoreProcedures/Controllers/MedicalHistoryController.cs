using Microsoft.EntityFrameworkCore;
using StoreProcedures.PostgreModels;
using StoreProcedures.Source.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StoreProcedures.Controllers
{
    public class MedicalHistoryController : ApiController
    {
        PostgreContext postgreContext = new PostgreContext();

        [Route("api/MedicalHistory/{ssn}")]
        [HttpGet]
        public IEnumerable<MedicalHistoryView> Get(string ssn)
        {
            var patientIdParam = new Npgsql.NpgsqlParameter("@patientid", ssn);

            var medicalHistories = postgreContext.MedicalHistory
                .FromSqlRaw("SELECT * from usp_patient_procedure(@patientid);", patientIdParam)
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
    }
}
