using DBManager.PostgreModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DBManager.Controllers
{
    public class LoungesController : ApiController
    {
        PostgreContext postgreContext = new PostgreContext();

        [Route("api/Lounges")]
        [HttpPost]
        public void Post(Lounge lounge)
        {
            postgreContext.Add(lounge);
            postgreContext.SaveChanges();
        }
    }
}
