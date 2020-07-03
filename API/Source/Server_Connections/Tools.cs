using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using Newtonsoft.Json.Linq;

namespace API.Source.Server_Connections
{
    public class Tools
    {
        public static SqlConnection connection = DatabaseDataHolder.connect_Database;

        public JObject spCasesByCountry(string country)
        {
            var objectList = new JObject();
            try
            {
                SqlCommand cmd = new SqlCommand("spCasesByCountry", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("Country", country));
                var sqlReader = cmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    var key = (string)sqlReader[0];
                    var value = sqlReader[1];
                    objectList.Add(new JProperty(key, value));
                }
                // Adds regions
                objectList.Add(spCasesByRegion(country));
                // Add measurements
                objectList.Add(spMeasurementsByCountry(country));
                // Adds accumulated
                objectList.Add(spAccumulatedCasesByCountry(country));
                
                Debug.WriteLine(objectList);
                return objectList;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
                return objectList;
            }
        }

        public JProperty spCasesByRegion(string country)
        {
            var objectList = new JArray();
            try
            {
                DatabaseDataHolder.restartConnection();
                SqlCommand cmd = new SqlCommand("spCasesByRegion", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("Country", country));
                var sqlReader = cmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    var region = new JObject
                    {
                        new JProperty("region", sqlReader[0]),
                        new JProperty("confirmed", sqlReader[1]),
                        new JProperty("active", sqlReader[2]),
                        new JProperty("dead", sqlReader[3]),
                        new JProperty("recovered", sqlReader[4])
                    };

                    objectList.Add(region);
                }
                Debug.WriteLine(objectList);
                return new JProperty("regions", objectList);;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
                return null;
            }
        }

        public JProperty spAccumulatedCasesByCountry(string country)
        {
            var objectList = new JArray();
            try
            {
                DatabaseDataHolder.restartConnection();
                SqlCommand cmd = new SqlCommand("spAccumulatedCasesByCountry", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("Country", country));
                var sqlReader = cmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    var accumulated = new JObject();
                    var date = "date";
                    var dateValue = sqlReader[0];
                    var quantity = "quantity";
                    var quantityValue = sqlReader[1];
                    var cases = "cases";
                    var casesValue = sqlReader[2];

                    accumulated.Add(new JProperty(date, dateValue));
                    accumulated.Add(new JProperty(cases, casesValue));
                    accumulated.Add(new JProperty(quantity, quantityValue));

                    objectList.Add(accumulated);
                }
                Debug.WriteLine(objectList);
                return new JProperty("accumulated", objectList);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
                return null;
            }
        }
        
        public JProperty spMeasurementsByCountry(string country)
        {
            var objectList = new JArray();
            try
            {
                DatabaseDataHolder.restartConnection();
                SqlCommand cmd = new SqlCommand("spMeasurementsState", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("Country", country));
                var sqlReader = cmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    var accumulated = new JObject
                    {
                        new JProperty("name", sqlReader[0]),
                        new JProperty("state", sqlReader[1]),
                        new JProperty("until", sqlReader[2])
                    };
                    
                    objectList.Add(accumulated);
                }
                Debug.WriteLine(objectList);
                return new JProperty("measurements", objectList);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
                return null;
            }
        }

        public void Email(string email)
        {
            try
            {
                var fromAddress = new MailAddress("pandemicadb@gmail.com", "Schlafenhase Team");
                var toAddress = new MailAddress(email, "To User");
                const string fromPassword = "pandemicadb2020";
                const string subject = "WARNING - Possible COVID-19 USER";
                const string body = "Hi from Schlafenhase Team, recently you made contact with someone with COVID-19, it may be possible that you are infected, please go and request a COVID-19 test as soon as posible.";

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
            }
            catch (Exception ex) 
            {
                Debug.WriteLine("An error happened", ex.Message);
            }
        }
    }
}