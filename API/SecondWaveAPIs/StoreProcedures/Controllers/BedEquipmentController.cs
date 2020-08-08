using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using StoreProcedures.PostgreModels;
using StoreProcedures.Source.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StoreProcedures.Controllers
{
    public class BedEquipmentController : ApiController
    {
        PostgreContext postgreContext = new PostgreContext();

        /// <summary>
        /// Function in charge of recopilating all the equipments of a bed in the database
        /// </summary>
        /// <param name="bed">
        /// Bed number
        /// </param>
        /// <returns>
        /// A list with all the equipment found
        /// </returns>
        [Route("api/BedEquipment/{bed:int}")]
        [HttpGet]
        public IEnumerable<EquipmentView> Get(int bed)
        {
            try
            {
                var bedNumberParam = new Npgsql.NpgsqlParameter("@bednumber", bed);

                var equipments = postgreContext.Equipment
                    .FromSqlRaw("SELECT * from usp_equipments_from_bed(@bednumber);", bedNumberParam)
                    .ToList();

                List<EquipmentView> result = new List<EquipmentView>();

                foreach (Equipment e in equipments)
                {
                    EquipmentView equipmentView = new EquipmentView();
                    equipmentView.Id = e.Id;
                    equipmentView.Name = e.Name;
                    equipmentView.Provider = e.Provider;
                    equipmentView.Quantity = e.Quantity;
                    result.Add(equipmentView);
                }

                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Function in charge receiving an equipment to store it in the database
        /// </summary>
        /// <param name="bedEquipment">
        /// Equipment to be added
        /// </param>
        [Route("api/BedEquipment")]
        [HttpPost]
        public void Post(JObject bedEquipment)
        {
            try
            {
                var bedNumberParameter = new Npgsql.NpgsqlParameter("@bednumber", (int)bedEquipment.GetValue("BedNumber"));
                var equipmentNameParameter = new Npgsql.NpgsqlParameter("@equipmentname", (string)bedEquipment.GetValue("EquipmentName"));

                var procedures = postgreContext.Database
                    .ExecuteSqlRaw("SELECT usp_insert_bed_equipment(@bednumber, @equipmentName)", bedNumberParameter, equipmentNameParameter);

                postgreContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
            }
        }

        /// <summary>
        /// Function in charge receiving updated data of an bed equipment
        /// </summary>
        /// <param name="bedNumber">
        /// Bed number
        /// </param>
        /// <param name="bedEquipment">
        /// Bed equipment to be updated
        /// </param>
        [Route("api/BedEquipment/{bedNumber:int}")]
        [HttpPut]
        public void Put(int bedNumber, JObject bedEquipment)
        {
            try
            {
                var bedNumberParameter = new Npgsql.NpgsqlParameter("@bednumber", bedNumber);
                var oldEquipmentNameParameter = new Npgsql.NpgsqlParameter("@oldequipmentname", (string)bedEquipment.GetValue("OldEquipmentName"));
                var newEquipmentNameParameter = new Npgsql.NpgsqlParameter("@newequipmentname", (string)bedEquipment.GetValue("NewEquipmentName"));

                var procedures = postgreContext.Database
                    .ExecuteSqlRaw("SELECT usp_update_equipment_from_bed(@bednumber, @oldequipmentname, @newequipmentname)", bedNumberParameter, oldEquipmentNameParameter, newEquipmentNameParameter);

                postgreContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
            }
        }

        /// <summary>
        /// Function in charge deleting a bed equipment
        /// </summary>
        /// <param name="bedNumber">
        /// Bed number
        /// </param>
        /// <param name="equipmentId">
        /// Equipment id
        /// </param>
        [Route("api/BedEquipment/{bedNumber:int}/{equipmentId:int}")]
        [HttpDelete]
        public void Delete(int bedNumber, int equipmentId)
        {
            try
            {
                var bedNumberParameter = new Npgsql.NpgsqlParameter("@bednumber", bedNumber);
                var equipmentIdParameter = new Npgsql.NpgsqlParameter("@equipmentid", equipmentId);

                var procedures = postgreContext.Database
                    .ExecuteSqlRaw("SELECT usp_delete_bed_equipment(@bednumber, @equipmentid)", bedNumberParameter, equipmentIdParameter);

                postgreContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
            }
        }
    }
}
