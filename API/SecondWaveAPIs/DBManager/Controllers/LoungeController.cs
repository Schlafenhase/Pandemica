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

        /// <summary>
        /// Function in charge of recopilating all the lounges in the database
        /// </summary>
        /// <param name="hospital">
        /// Id of the hospital that the lounge belongs
        /// </param>
        /// <returns>
        /// A list with all the lounges found
        /// </returns>
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

        /// <summary>
        /// Function in charge of returning all the lounges number of a hospital
        /// </summary>
        /// <param name="hospital">
        /// Id of the hospital that the lounge belongs
        /// </param>
        /// <returns>
        /// A list with all the lounges found
        /// </returns>
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

        /// <summary>
        /// Function in charge receiving a lounge to store it in the database
        /// </summary>
        /// <param name="lounge">
        /// Lounge to be added
        /// </param>
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

        /// <summary>
        /// Function in charge receiving updated data of a lounge
        /// </summary>
        /// <param name="number">
        /// Lounge number
        /// </param>
        /// <param name="lounge">
        /// Lounge to be updated
        /// </param>
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

        /// <summary>
        /// Function in charge deleting a lounge
        /// </summary>
        /// <param name="number">
        /// Lounge number
        /// </param>
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
