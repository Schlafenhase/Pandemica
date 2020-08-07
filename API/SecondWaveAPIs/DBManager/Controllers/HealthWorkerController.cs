using DBManager.PostgreModels;
using DBManager.Source.Entities;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DBManager.Controllers
{
    public class HealthWorkerController : ApiController
    {
        PostgreContext postgreContext = new PostgreContext();

        [Route("api/HealthWorker/{hospital:int}")]
        [HttpGet]
        public IEnumerable<HealthWorkerView> Get(int hospital)
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

        [Route("api/HealthWorker")]
        [HttpPost]
        public void Post(HealthWorker healthWorker)
        {
            postgreContext.Add(healthWorker);
            postgreContext.SaveChanges();
        }

        [Route("api/HealthWorker/{ssn}")]
        [HttpPut]
        public void Put(string ssn, HealthWorker healthWorker)
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

        [Route("api/HealthWorker/{ssn}")]
        [HttpDelete]
        public void Delete(string ssn)
        {
            postgreContext.Remove(postgreContext.HealthWorker.Single(hw => hw.Ssn == ssn));
            postgreContext.SaveChanges();
        }
    }
}
