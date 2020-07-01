using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace API.Source.Server_Connections
{
    public class SpecificUpdate
    {
        public static SqlConnection connection = DatabaseDataHolder.connect_Database;

        public void makeSpecificContactUpdate(string id, string person, string patient)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(@"UPDATE CONTACT SET Person = '" + person + "', Patient = '" + patient + "' WHERE Id = " + id, connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
            }
        }
        public void makeSpecificEnforcesIdUpdate(string id, string country, string measurement, string startDate, string finalDate)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(@"UPDATE ENFORCES SET Country = '" + country + "', Measurement = " + measurement + ", StartDate = '" + startDate + "', FinalDate = '" + finalDate + "' WHERE Id = " + id, connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
            }
        }
        public void makeSpecificEnforcesNameUpdate(string id, string country, string measurement, string startDate, string finalDate)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(@"UpdateCountryMeasures @Country = '" + country + "', @Measurement = '" + measurement + "', @StartDate = '" + startDate + "', @FinalDate = '" + finalDate + "', @Id = " + id, connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
            }
        }
        public void makeSpecificHospitalUpdate(string id, string name, string phone, string managerName, string capacity, string icuCapacity, string country, string region, string eMail)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(@"UPDATE HOSPITAL SET Name = '" + name + "', Phone = " + phone + ", ManagerName = '" + managerName + "', Capacity = " + capacity + ", ICUCapacity = " + icuCapacity + ", Country = '" + country + "', Region = '" + region + "', EMail = '" + eMail + "' WHERE Id = " + id, connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
            }
        }
        public void makeSpecificMedicationUpdate(string id, string name, string pharmacy)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(@"UPDATE MEDICATION SET Name = '" + name + "', Pharmacy = '" + pharmacy + "' WHERE Id = " + id, connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
            }
        }
        public void makeSpecificPathologyUpdate(string id, string name, string symptoms, string description, string treatment)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(@"UPDATE PATHOLOGY SET Name = '" + name + "', Symptoms = '" + symptoms + "', Description = '" + description + "', Treatment = '" + treatment + "' WHERE Id = " + id, connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
            }
        }
        public void makeSpecificPatientUpdate(string id, string firstName, string lastName, string birthDate, string hospitalized, string icu, string country, string region, string nationality, string hospital, string sex)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(@"UPDATE PATIENT SET FirstName = '" + firstName + "', LastName = '" + lastName + "', BirthDate = '" + birthDate + "', Hospitalized = '" + hospitalized + "', ICU = '" + icu + "', Country = '" + country + "', Region = '" + region + "', Nationality = '" + nationality + "', Hospital = " + hospital + ", Sex = '" + sex + "' WHERE Ssn = '" + id + "'", connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
            }
        }
        public void makeSpecificPatientMedicationUpdate(string ssn, string name, string prevName)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(@"UpdateMedicationsForPatient @Ssn = '" + ssn + "', @MedicationName = '" + name + "', @PrevName = '" + prevName + "'", connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
            }
        }
        public void makeSpecificPatientPathologiesUpdate(string ssn, string name, string prevName)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(@"UpdatePathologiesForPatient @Ssn = '" + ssn + "', @PathologyName = '" + name + "', @PrevName = '" + prevName + "'", connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
            }
        }
        public void makeSpecificPatientStateUpdate(string state, string patient, string date, string prevState, string prevDate)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(@"UpdateStateForPatient @Ssn = '" + patient + "', @Date = '" + date + "', @StateName = '" + state + "', @PrevDate = '" + prevDate + "', @PrevState = '" + prevState + "'", connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
            }
        }
        public void makeSpecificPersonUpdate(string personSsn, string firstName, string lastName, string birthDate, string eMail, string address, string sex, string contactDate, string patientSsn)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(@"UpdateContact @PersonSsn = '" + personSsn + "', @FirstName = '" + firstName + "', @LastName = '" + lastName + "', @BirthDate = '" + birthDate + "', @EMail = '" + eMail + "', @Address = '" + address + "', @Sex = '" + sex + "', @ContactDate = '" + contactDate + "', @PatientSsn = '" + patientSsn + "'", connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
            }
        }
        public void makeSpecificProvinceStateRegionUpdate(string id, string name, string country)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(@"UPDATE PROVINCE_STATE_REGION SET Name = '" + name + "', Country = '" + country + "' WHERE Id = " + id, connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
            }
        }
        public void makeSpecificSanitaryMeasurementsUpdate(string id, string name, string description)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(@"UPDATE SANITARY_MEASUREMENTS SET Name = '" + name + "', Description = '" + description + "' WHERE Id = " + id, connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
            }
        }
        public void makeSpecificStateUpdate(string id, string name)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(@"UPDATE STATE SET Name = '" + name + "' WHERE Id = " + id, connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
            }
        }
    }
}