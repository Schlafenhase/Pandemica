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
    public class ProcedureController : ApiController
    {
        PostgreContext postgreContext = new PostgreContext();

        [Route("api/Procedure/Name")]
        [HttpGet]
        public IEnumerable<string> GetProcedureNames()
        {
            try
            {
                var procedures = postgreContext.Procedure
                .Select(p => new ProcedureView
                {
                    Id = p.Id,
                    Name = p.Name,
                    Duration = p.Duration
                })
                .ToList();

                List<string> names = new List<string>();

                foreach (ProcedureView pv in procedures)
                {
                    names.Add(pv.Name);
                }

                return names;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
                return null;
            }
        }
    }
}
