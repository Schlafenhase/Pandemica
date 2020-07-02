﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Diagnostics;

namespace API.Source.Server_Connections
{
    public class DatabaseDataHolder
    {

        string chain = "Data Source=.;Initial Catalog=PandemicaDB; Integrated Security=True";

        public static SqlConnection connect_Database = new SqlConnection();

        //Constructor
        public DatabaseDataHolder()
        {
            try
            {
                connect_Database.ConnectionString = chain;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("error openning database ", ex.Message);
            }
        }

        //Opening connection method
        public void openConnection()
        {
            try
            {
                connect_Database.Open();
                Debug.WriteLine("Database open ");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("error openning database ", ex.Message);
            }
        }

        //Closing connection method
        public void closeConnection()
        {
            try
            {
                connect_Database.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("error openning database ", ex.Message);
            }
        }

    }

}