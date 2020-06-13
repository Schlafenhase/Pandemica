﻿using API.Source.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class StateController : ApiController
    {
        [Route("api/State")]
        [HttpGet]
        public IEnumerable<State> Get()
        {
            State s1 = new State("Active");
            State s2 = new State("Dead");
            return new State[] { s1, s2 };
        }

        [Route("api/State/{name}")]
        [HttpGet]
        public string Get(string name)
        {
            return name;
        }

        [Route("api/State")]
        [HttpPost]
        public void Post(State state)
        {
            Debug.WriteLine("Inserted");
        }

        [Route("api/State/{name}")]
        [HttpPut]
        public void Put(string name, State state)
        {
            Debug.WriteLine("Updated");
        }

        [Route("api/State/{name}")]
        [HttpDelete]
        public void Delete(string name)
        {
            Debug.WriteLine("Deleted");
        }
    }
}
