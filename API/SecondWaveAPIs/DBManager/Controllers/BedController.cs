using DBManager.PostgreModels;
using DBManager.Source.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DBManager.Controllers
{
    public class BedController : ApiController
    {
        PostgreContext postgreContext = new PostgreContext();

        [Route("api/Bed")]
        [HttpGet]
        public IEnumerable<BedView> Get()
        {
            var beds = postgreContext.Bed
                .Select(b => new BedView
                {
                    Number = b.Number,
                    Icu = b.Icu,
                    LoungeNumber = b.LoungeNumber
                })
                .ToList();
            return beds;
        }

        [Route("api/Bed")]
        [HttpPost]
        public void Post(Bed bed)
        {
            postgreContext.Add(bed);
            postgreContext.SaveChanges();
        }

        [Route("api/Bed/{number:int}")]
        [HttpPut]
        public void Put(int number, Bed bed)
        {
            var oldBed = postgreContext.Bed
                .Where(b => b.Number == number)
                .Single();


            oldBed.LoungeNumber = bed.LoungeNumber;
            oldBed.Icu = bed.Icu;
            postgreContext.SaveChanges();
        }

        [Route("api/Bed/{number:int}")]
        [HttpDelete]
        public void Delete(int number)
        {
            postgreContext.Remove(postgreContext.Bed.Single(b => b.Number == number));
            postgreContext.SaveChanges();
        }
    }
}
