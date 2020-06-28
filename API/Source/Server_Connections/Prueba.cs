using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace API.Source.Server_Connections
{
    public class Prueba
    {
        public static SqlConnection connection = DatabaseDataHolder.connect_Database;

        public IEnumerable<DataInfo> spPrueba(string country)
        {
            var objectList = new List<DataInfo>();
            SqlCommand cmd = new SqlCommand("spPrueba2", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("Country",country));
            var sqlReader = cmd.ExecuteReader();

            while (sqlReader.Read())
            {                
                var data = new DataInfo();
                data.requestedData = (string)sqlReader[0];
                data.resultData = (int)sqlReader[1];
                Debug.WriteLine(data.requestedData, data.resultData);
                objectList.Add(data);
            }
            return objectList;
        }
    }
}