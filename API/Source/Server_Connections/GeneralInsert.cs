using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace API.Source.Server_Connections
{
    public class GeneralInsert
    {
        public static SqlConnection connection = DatabaseDataHolder.connect_Database;

        public void makeContactInsert(string person, string patient)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(@"INSERT INTO CONTACT (Person,Patient) VALUES ('" + person + "','" + patient + "')", connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
            }
        }
        public void makeEnforcesIdInsert(string country, string measurement, string startDate, string finalDate)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(@"INSERT INTO ENFORCES (Country,Measurement,StartDate,FinalDate) VALUES ('" + country + "'," + measurement + ",'" + startDate + "','" + finalDate + "')", connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
            }
        }
        public void makeEnforcesNameInsert(string country, string measurement, string startDate, string finalDate)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(@"InsertCountryMeasures @Country = '" + country + "', @Measurement = '" + measurement + "', @StartDate = '" + startDate + "', @FinalDate = '" + finalDate + "'", connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
            }
        }
        public void makeHospitalInsert(string name, string phone, string managerName, string capacity, string icuCapacity, string country, string region, string eMail)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(@"INSERT INTO HOSPITAL (Name,Phone,ManagerName,Capacity,ICUCapacity,Country,Region,EMail) VALUES ('" + name + "'," + phone + ",'" + managerName + "'," + capacity + "," + icuCapacity + ",'" + country + "','" + region + "','" + eMail + "')", connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
            }
        }

        public void makeMedicationInsert(string id, string name, string pharmacy)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(@"INSERT INTO MEDICATION (Name,Pharmacy) VALUES ('" + name + "','" + pharmacy + "')", connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
            }
        }

        public void makePathologyInsert(string name, string symptoms, string description, string treatment)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(@"INSERT INTO PATHOLOGY (Name,Symptoms,Description,Treatment) VALUES ('" + name + "','" + symptoms + "','" + description + "','" + treatment + "')", connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
            }
        }

        public void makePatientInsert(string ssn, string firstName, string lastName, string birthDate, string hospitalized, string icu, string country, string region, string nationality, string hospital, string sex)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(@"INSERT INTO PATIENT (Ssn,FirstName,LastName,BirthDate,Hospitalized,ICU,Country,Region,Nationality,Hospital,Sex) VALUES ('" + ssn + "','" + firstName + "','" + lastName + "','" + birthDate + "','" + hospitalized + "','" + icu + "','" + country + "','" + region + "','" + nationality + "'," + hospital + ",'" + sex + "')", connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
            }
        }

        public void makePatientMedicationInsert(string patient, string medication)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(@"InsertMedicationsForPatient @Ssn = '" + patient + "', @MedicationName = '" + medication + "'", connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
            }
        }

        public void makePatientPathologiesInsert(string patient, string pathology)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(@"InsertPathologiesForPatient @Ssn = '" + patient + "', @PathologyName = '" + pathology + "'", connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
            }
        }

        public void makePatientStateInsert(string state, string patient, string date)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(@"InsertStateForPatient @Ssn = '" + patient + "', @Date = '" + date + "', @StateName = '" + state + "'", connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
            }
        }

        public void makePersonInsert(string personSsn, string firstName, string lastName, string birthDate, string eMail, string address, string sex, string contactDate, string patientSsn)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(@"InsertContact @PersonSsn = '" + personSsn + "', @FirstName = '" + firstName + "', @LastName = '" + lastName +"', @BirthDate = '" + birthDate + "', @EMail = '" + eMail + "', @Address = '" + address + "', @Sex = '" + sex + "', @ContactDate = '" + contactDate + "', @PatientSsn = '" + patientSsn + "'", connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
            }
        }

        public void makeProvinceStateRegionInsert(string name, string country)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(@"INSERT INTO PROVINCE_STATE_REGION (Name,Country) VALUES ('" + name + "','" + country + "')", connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
            }
        }

        public void makeSanitaryMeasurementsInsert(string name, string description)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(@"INSERT INTO SANITARY_MEASUREMENTS (Name,Description) VALUES ('" + name + "','" + description + "')", connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
            }
        }

        public void makeStateInsert(string name)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(@"INSERT INTO STATE (Name) VALUES ('" + name + "')", connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
            }
        }

    }
}