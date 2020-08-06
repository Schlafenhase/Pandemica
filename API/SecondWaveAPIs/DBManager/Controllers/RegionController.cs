using DBManager.MSSQLModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DBManager.Controllers
{
    public class RegionController : ApiController
    {
        MSSQLContext mSSQLContext = new MSSQLContext();

        [Route("api/Region/CostaRica")]
        [HttpGet]
        public IEnumerable<string> GetRegionsFromCostaRica()
        {
            var provinceStateRegions = mSSQLContext.ProvinceStateRegion
                .Where(p => p.Country == "Costa Rica");

            var regionNames = new List<string>();

            foreach (ProvinceStateRegion p in provinceStateRegions)
            {
                regionNames.Add(p.Name);
            }

            return regionNames;
        }
    }
}
