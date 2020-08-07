using DBManager.PostgreModels;
using DBManager.Source.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DBManager.Controllers
{
    public class LoungeController : ApiController
    {
        PostgreContext postgreContext = new PostgreContext();

        [Route("api/Lounge/{hospital:int}")]
        [HttpGet]
        public IEnumerable<LoungeView> Get(int hospital)
        {
            var lounges = postgreContext.Lounge
                .Where(l => l.HospitalId == hospital)
                .Select(l => new LoungeView { 
                    Number = l.Number, 
                    Floor = l.Floor,
                    Name = l.Name,
                    Type = l.Type,
                    BedCapacity = l.BedCapacity})
                .ToList();
            return lounges;
        }

        [Route("api/Lounge/Number/{hospital:int}")]
        [HttpGet]
        public IEnumerable<int> GetLoungesNumberFromHospital (int hospital)
        {
            var lounges = postgreContext.Lounge
                .Where(l => l.HospitalId == hospital)
                .Select(l => new LoungeView
                {
                    Number = l.Number,
                    Floor = l.Floor,
                    Name = l.Name,
                    Type = l.Type,
                    BedCapacity = l.BedCapacity
                })
                .ToList();

            List<int> numbers = new List<int>();

            foreach (LoungeView lv in lounges)
            {
                numbers.Add(lv.Number);
            }

            return numbers;
        }

        [Route("api/Lounge")]
        [HttpPost]
        public void Post(Lounge lounge)
        {
            postgreContext.Add(lounge);
            postgreContext.SaveChanges();
        }

        [Route("api/Lounge/{number:int}")]
        [HttpPut]
        public void Put(int number, Lounge lounge)
        {
            var oldLounge = postgreContext.Lounge
                .Where(l => l.Number == number)
                .Single();

            oldLounge.Floor = lounge.Floor;
            oldLounge.Name = lounge.Name;
            oldLounge.Type = lounge.Type;
            oldLounge.BedCapacity = lounge.BedCapacity;

            postgreContext.SaveChanges();
        }

        [Route("api/Lounge/{number:int}")]
        [HttpDelete]
        public void Delete(int number)
        {
            postgreContext.Remove(postgreContext.Lounge.Single(l => l.Number == number));
            postgreContext.SaveChanges();
        }
    }
}
