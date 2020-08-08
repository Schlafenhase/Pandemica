using DBManager.MSSQLModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DBManager.Controllers
{
    public class HospitalController : ApiController
    {
        MSSQLContext mSSQLContext = new MSSQLContext();

        /// <summary>
        /// Function in charge of recopilating all the hospitals of Costa Rica in the database
        /// </summary>
        /// <returns>
        /// A list with the hospitals found
        /// </returns>
        [Route("api/Hospital/CostaRica")]
        [HttpGet]
        public IEnumerable<string> GetRegionsFromCostaRica()
        {
            try
            {
                var hospitals = mSSQLContext.Hospital
                                    .Where(h => h.Country == "Costa Rica");

                var hospitalNames = new List<string>();

                foreach (Hospital h in hospitals)
                {
                    hospitalNames.Add(h.Name);
                }

                return hospitalNames;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
                return null;
            }
        }
    }
}
