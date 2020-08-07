using DBManager.PostgreModels;
using DBManager.Source.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            try
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
                return lounges;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
                return null;
            }
        }

        [Route("api/Lounge/Number/{hospital:int}")]
        [HttpGet]
        public IEnumerable<int> GetLoungesNumberFromHospital (int hospital)
        {
            try
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
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
                return null;
            }
        }

        [Route("api/Lounge")]
        [HttpPost]
        public void Post(Lounge lounge)
        {
            try
            {
                postgreContext.Add(lounge);
                postgreContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
            }
        }

        [Route("api/Lounge/{number:int}")]
        [HttpPut]
        public void Put(int number, Lounge lounge)
        {
            try
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
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
            }
        }

        [Route("api/Lounge/{number:int}")]
        [HttpDelete]
        public void Delete(int number)
        {
            try
            {
                postgreContext.Remove(postgreContext.Lounge.Single(l => l.Number == number));
                postgreContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
            }
        }
    }
}
