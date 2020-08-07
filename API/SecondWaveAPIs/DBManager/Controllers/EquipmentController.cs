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
    public class EquipmentController : ApiController
    {
        PostgreContext postgreContext = new PostgreContext();

        [Route("api/Equipment")]
        [HttpGet]
        public IEnumerable<EquipmentView> Get()
        {
            var equipments = postgreContext.Equipment
                .Select(e => new EquipmentView
                {
                    Id = e.Id,
                    Name = e.Name,
                    Provider = e.Provider,
                    Quantity = e.Quantity
                })
                .ToList();
            return equipments;
        }

        [Route("api/Equipment/Name")]
        [HttpGet]
        public IEnumerable<string> GetEquipmentNamesFromHospital()
        {
            var equipments = postgreContext.Equipment
                .Select(e => new EquipmentView
                {
                    Id = e.Id,
                    Name = e.Name,
                    Provider = e.Provider,
                    Quantity = e.Quantity
                })
                .ToList();

            List<string> names = new List<string>();

            foreach (EquipmentView ev in equipments)
            {
                names.Add(ev.Name);
            }

            return names;
        }

        [Route("api/Equipment")]
        [HttpPost]
        public void Post(Equipment equipment)
        {
            postgreContext.Add(equipment);
            postgreContext.SaveChanges();
        }

        [Route("api/Equipment/{id:int}")]
        [HttpPut]
        public void Put(int id, Equipment equipment)
        {
            var oldEquipment = postgreContext.Equipment
                .Where(e => e.Id == id)
                .Single();

            oldEquipment.Name = equipment.Name;
            oldEquipment.Provider = equipment.Provider;
            oldEquipment.Quantity = equipment.Quantity;

            postgreContext.SaveChanges();
        }

        [Route("api/Equipment/{id:int}")]
        [HttpDelete]
        public void Delete(int id)
        {
            postgreContext.Remove(postgreContext.Equipment.Single(e => e.Id == id));
            postgreContext.SaveChanges();
        }
    }
}
