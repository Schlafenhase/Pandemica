using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;

namespace API.Source.Server_Connections
{
    public class Prueba
    {
        public static SqlConnection connection = DatabaseDataHolder.connect_Database;

        public JObject spPrueba(string country)
        {
            var objectList = new JObject();
            SqlCommand cmd = new SqlCommand("spPrueba2", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("Country",country));
            var sqlReader = cmd.ExecuteReader();

            while (sqlReader.Read())
            {
                var key = (string) sqlReader[0];
                var value = sqlReader[1];
                objectList.Add(new JProperty(key, value));
            }
            Debug.WriteLine(objectList);
            return objectList;
        }
    }
}