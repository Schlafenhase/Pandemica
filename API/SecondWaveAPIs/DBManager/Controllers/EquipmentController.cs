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
    public class EquipmentController : ApiController
    {
        PostgreContext postgreContext = new PostgreContext();

        /// <summary>
        /// Function in charge of recopilating all the equipments in the database
        /// </summary>
        /// <returns>
        /// A list with all the equipments found
        /// </returns>
        [Route("api/Equipment")]
        [HttpGet]
        public IEnumerable<EquipmentView> Get()
        {
            try
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
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Function in charge of recopilating all the equipment names in the database
        /// </summary>
        /// <returns>
        /// A list with all the equipment names found
        /// </returns>
        [Route("api/Equipment/Name")]
        [HttpGet]
        public IEnumerable<string> GetEquipmentNamesFromHospital()
        {
            try
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
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Function in charge receiving a equipment to store it in the database
        /// </summary>
        /// <param name="equipment">
        /// Equipment to be added
        /// </param>
        [Route("api/Equipment")]
        [HttpPost]
        public void Post(Equipment equipment)
        {
            try
            {
                postgreContext.Add(equipment);
                postgreContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
            }
        }

        /// <summary>
        /// Function in charge receiving updated data of an equipment
        /// </summary>
        /// <param name="id">
        /// Id of the equipment to be updated
        /// </param>
        /// <param name="equipment">
        /// Equipment with the data to be updated
        /// </param>
        [Route("api/Equipment/{id:int}")]
        [HttpPut]
        public void Put(int id, Equipment equipment)
        {
            try
            {
                var oldEquipment = postgreContext.Equipment
                                    .Where(e => e.Id == id)
                                    .Single();

                oldEquipment.Name = equipment.Name;
                oldEquipment.Provider = equipment.Provider;
                oldEquipment.Quantity = equipment.Quantity;

                postgreContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
            }
        }

        /// <summary>
        /// Function in charge deleting an equipment
        /// </summary>
        /// <param name="id">
        /// Id of the equipment
        /// </param>
        [Route("api/Equipment/{id:int}")]
        [HttpDelete]
        public void Delete(int id)
        {
            try
            {
                postgreContext.Remove(postgreContext.Equipment.Single(e => e.Id == id));
                postgreContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
            }
        }
    }
}
