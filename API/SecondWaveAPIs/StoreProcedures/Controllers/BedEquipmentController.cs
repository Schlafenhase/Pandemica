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
                .FromSqlRaw("SELECT * from proceduresbyhospital(@bednumber);", bedNumberParam) //Cambiar la llamada del sp
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
                .ExecuteSqlRaw("SELECT addproceduretohospital(@bednumber, @equipmentName)", bedNumberParameter, equipmentNameParameter); //Cambiar la llamada al sp

            postgreContext.SaveChanges();
        }

        [Route("api/BedEquipment/{bedNumber:int}")]
        [HttpPut]
        public void Put(int number, JObject bedEquipment)
        {
            var bedNumberParameter = new Npgsql.NpgsqlParameter("@bednumber", number);
            var oldEquipmentNameParameter = new Npgsql.NpgsqlParameter("@oldequipmentname", (string)bedEquipment.GetValue("EquipmentName"));
            var newEquipmentNameParameter = new Npgsql.NpgsqlParameter("@newequipmentname", (string)bedEquipment.GetValue("EquipmentName"));

            var procedures = postgreContext.Database
                .ExecuteSqlRaw("SELECT addproceduretohospital(@bednumber, @oldequipmentname, @newequipmentname)", bedNumberParameter, oldEquipmentNameParameter, newEquipmentNameParameter); //Cambiar la llamada al sp

            postgreContext.SaveChanges();
        }

        [Route("api/Procedure/{bedNumber:int}/{equipmentId:int}")]
        [HttpDelete]
        public void Delete(int bedNumber, int equipmentId)
        {
            var bedNumberParameter = new Npgsql.NpgsqlParameter("@bednumber", bedNumber);
            var equipmentIdParameter = new Npgsql.NpgsqlParameter("@equipmentid", equipmentId);

            var procedures = postgreContext.Database
                .ExecuteSqlRaw("SELECT deleteprocedure(@bednumber, @equipmentid)", bedNumberParameter, equipmentIdParameter); //Cambiar la llamada al sp

            postgreContext.SaveChanges();
        }
    }
}
