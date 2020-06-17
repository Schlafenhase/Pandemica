using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Web;
using API.Source.Server_Connections;

namespace API.Source.Server_Connections
{
    public class generalSelect
    {
        
        public static SqlConnection connection = DatabaseDataHolder.connect_Database;
       
        public void makeSelect(string firstCol, string secondCol, string thirdCol, string fourthCol, string fifthCol)
        {
            databaseInfo[] allRecords=null;
            string sql = @"SELECT " + firstCol + secondCol + " FROM CONTACT";
            Debug.WriteLine(firstCol);
            Debug.WriteLine(secondCol);
            using (var cmd = new SqlCommand(sql, connection))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    var list = new List<databaseInfo>();
                    while (reader.Read())
                        list.Add(new databaseInfo { Col1 = reader.GetInt32(0), Col2 = reader.GetInt32(1)});
                    allRecords = list.ToArray();
                    Debug.WriteLine(list);
                    Debug.WriteLine(allRecords);
                }
            }
            
            //SqlCommand cmd = new SqlCommand(@"INSERT INTO CONTACT ("+firstCol+","+secondCol+") VALUES (1777,797)", connection);
            
            
        }
    } 
}
