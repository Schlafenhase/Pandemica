using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using StoreProcedures.PostgreModels;
using StoreProcedures.Source.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StoreProcedures.Controllers
{
    public class BedEquipmentController : ApiController
    {
        PostgreContext postgreContext = new PostgreContext();

        [Route("api/BedEquipment/{bed:int}")]
        [HttpGet]
        public IEnumerable<EquipmentView> Get(int bed)
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

        [Route("api/BedEquipment")]
        [HttpPost]
        public void Post(JObject bedEquipment)
        {
            var bedNumberParameter = new Npgsql.NpgsqlParameter("@bednumber", (int)bedEquipment.GetValue("BedNumber"));
            var equipmentNameParameter = new Npgsql.NpgsqlParameter("@equipmentname", (string)bedEquipment.GetValue("EquipmentName"));

            var procedures = postgreContext.Database
                .ExecuteSqlRaw("SELECT usp_insert_bed_equipment(@bednumber, @equipmentName)", bedNumberParameter, equipmentNameParameter);

            postgreContext.SaveChanges();
        }

        [Route("api/BedEquipment/{bedNumber:int}")]
        [HttpPut]
        public void Put(int number, JObject bedEquipment)
        {
            var bedNumberParameter = new Npgsql.NpgsqlParameter("@bednumber", number);
            var oldEquipmentNameParameter = new Npgsql.NpgsqlParameter("@oldequipmentname", (string)bedEquipment.GetValue("OldEquipmentName"));
            var newEquipmentNameParameter = new Npgsql.NpgsqlParameter("@newequipmentname", (string)bedEquipment.GetValue("NewEquipmentName"));

            var procedures = postgreContext.Database
                .ExecuteSqlRaw("SELECT usp_update_equipment_from_bed(@bednumber, @oldequipmentname, @newequipmentname)", bedNumberParameter, oldEquipmentNameParameter, newEquipmentNameParameter);

            postgreContext.SaveChanges();
        }

        [Route("api/BedEquipment/{bedNumber:int}/{equipmentId:int}")]
        [HttpDelete]
        public void Delete(int bedNumber, int equipmentId)
        {
            var bedNumberParameter = new Npgsql.NpgsqlParameter("@bednumber", bedNumber);
            var equipmentIdParameter = new Npgsql.NpgsqlParameter("@equipmentid", equipmentId);

            var procedures = postgreContext.Database
                .ExecuteSqlRaw("SELECT usp_delete_bed_equipment(@bednumber, @equipmentid)", bedNumberParameter, equipmentIdParameter);

            postgreContext.SaveChanges();
        }
    }
}
