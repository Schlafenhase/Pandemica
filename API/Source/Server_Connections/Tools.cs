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
            SqlCommand cmd = new SqlCommand("spCasesByCountry", connection);
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

        public JObject spCasesByRegion(string country)
        {
            var objectList = new JObject();
            SqlCommand cmd = new SqlCommand("spCasesByRegion", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("Country", country));
            var sqlReader = cmd.ExecuteReader();

            while (sqlReader.Read())
            {
                var key = (string)sqlReader[1];
                var value = sqlReader[2];
                objectList.Add(new JProperty(key, value));
                for (var i = 0; i < objectList.Count; i++)
                {
                  
                }
            }
            Debug.WriteLine(objectList);
            return objectList;
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
            catch (Exception) { }
        }
    }
}