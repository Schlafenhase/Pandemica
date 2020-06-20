using API.Source.Entities;
using API.Source.Server_Connections;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class PersonController : ApiController
    {
        GeneralInsert insert = new GeneralInsert();
        DatabaseDataHolder connection = new DatabaseDataHolder();

        [Route("api/Person")]
        [HttpGet]
        public IEnumerable<Person> Get()
        {
            Person p1 = new Person(1, "Jesus", "Sandoval", 20, "chus@chus.com", "Alla en la verga");
            Person p2 = new Person(2, "Jose", "David", 21, "jose@jose.com", "Judiolandia");
            return new Person[] { p1, p2 };
        }

        [Route("api/Person/{id:int}")]
        [HttpGet]
        public int Get(int id)
        {
            return id;
        }

        [Route("api/Person")]
        [HttpPost]
        public void Post(Person person)
        {

            connection.openConnection();
            insert.makePersonInsert(person.ssn.ToString(), person.firstName, person.lastName, person.age.ToString(), person.eMail, person.address);
            connection.closeConnection();
            Debug.WriteLine("Inserted");
        }

        [Route("api/Person/{id:int}")]
        [HttpPut]
        public void Put(int id, Person person)
        {
            Debug.WriteLine("Updated");
        }

        [Route("api/Person/{id:int}")]
        [HttpDelete]
        public void Delete(int id)
        {
            Debug.WriteLine("Deleted");
        }
    }
}
