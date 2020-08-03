using DBManager.MSSQLModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DBManager.Controllers
{
    public class HospitalController : ApiController
    {
        MSSQLContext mSSQLContext = new MSSQLContext();

        [Route("api/Hospital/CostaRica")]
        [HttpGet]
        public IEnumerable<string> GetRegionsFromCostaRica()
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
    }
}
