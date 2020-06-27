using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace API.Source.Server_Connections
{
    public class GeneralInsert
    {
        public static SqlConnection connection = DatabaseDataHolder.connect_Database;

        public void makeContactInsert(string person, string patient)
        {
            SqlCommand cmd = new SqlCommand(@"INSERT INTO CONTACT (Person,Patient) VALUES (" + person + "," + patient + ")", connection);
            cmd.ExecuteNonQuery();
        }

        public void makeContinentInsert(string name)
        {
            SqlCommand cmd = new SqlCommand(@"INSERT INTO CONTINENT (Name) VALUES (" + name + ")", connection);
            cmd.ExecuteNonQuery();
        }

        public void makeCountryInsert(string name, string continentName, string eMail)
        {
            SqlCommand cmd = new SqlCommand(@"INSERT INTO COUNTRY (Name,ContinentName,EMail) VALUES (" + name + "," + continentName + "," + eMail + ")", connection);
            cmd.ExecuteNonQuery();
        }

        public void makeEnforcesInsert(string country, string measurement, string startDate, string finalDate)
        {
            SqlCommand cmd = new SqlCommand(@"INSERT INTO ENFORCES (Country,Measurement,StartDate,FinalDate) VALUES (" + country + "," + measurement + "," + startDate + "," + finalDate + ")", connection);
            cmd.ExecuteNonQuery();
        }

        public void makeHospitalInsert(string id, string name, string phone, string managerName, string capacity, string icuCapacity, string country, string region, string eMail)
        {
            SqlCommand cmd = new SqlCommand(@"INSERT INTO HOSPITAL (Id,Name,Phone,ManagerName,Capacity,ICUCapacity,Country,Region,EMail) VALUES (" + id + "," + name + "," + phone + "," + managerName + "," + capacity + "," + icuCapacity + "," + country + "," + region + "," + eMail + ")", connection);
            cmd.ExecuteNonQuery();
        }

        public void makeMedicationInsert(string id, string name, string pharmacy)
        {
            SqlCommand cmd = new SqlCommand(@"INSERT INTO MEDICATION (Id,Name,Pharmacy) VALUES (" + id + "," + name + "," + pharmacy + ")", connection);
            cmd.ExecuteNonQuery();
        }

        public void makeMedicationSupplyInsert(string medication, string hospital, string quantity)
        {
            SqlCommand cmd = new SqlCommand(@"INSERT INTO MEDICATION_SUPPLY (Medication,Hospital,Quantity) VALUES (" + medication + "," + hospital + "," + quantity + ")", connection);
            cmd.ExecuteNonQuery();
        }

        public void makePathologyInsert(string name, string symptoms, string description, string treatment)
        {
            SqlCommand cmd = new SqlCommand(@"INSERT INTO PATHOLOGY (Name,Symptoms,Description,Treatment) VALUES (" + name + "," + symptoms + "," + description + "," + treatment + ")", connection);
            cmd.ExecuteNonQuery();
        }

        public void makePatientInsert(string ssn, string firstName, string lastName, string birthDate, string hospitalized, string icu, string country, string region, string nationality, string hospital)
        {
            SqlCommand cmd = new SqlCommand(@"INSERT INTO PATIENT (Ssn,FirstName,LastName,BirthDate,Hospitalized,ICU,Country,Region,Nationality,Hospital) VALUES (" + ssn + "," + firstName + "," + lastName + "," + birthDate + "," + hospitalized + "," + icu + "," + country + "," + region + "," + nationality + "," + hospital + ")", connection);
            cmd.ExecuteNonQuery();
        }

        public void makePatientMedicationInsert(string patient, string medication)
        {
            SqlCommand cmd = new SqlCommand(@"INSERT INTO PATIENT_MEDICATION (Patient,Medication) VALUES (" + patient + "," + medication + ")", connection);
            cmd.ExecuteNonQuery();
        }

        public void makePatientPathologiesInsert(string patient, string pathology)
        {
            SqlCommand cmd = new SqlCommand(@"INSERT INTO PATIENT_PATHOLOGIES (Patient,Pathology) VALUES (" + patient + "," + pathology + ")", connection);
            cmd.ExecuteNonQuery();
        }

        public void makePatientStateInsert(string state, string patient, string date)
        {
            SqlCommand cmd = new SqlCommand(@"INSERT INTO PATIENT_STATE (State,Patient,Date) VALUES (" + state + "," + patient + "," + date + ")", connection);
            cmd.ExecuteNonQuery();
        }

        public void makePersonInsert(string ssn, string firstName, string lastName, string birthDate, string eMail, string address)
        {
            SqlCommand cmd = new SqlCommand(@"INSERT INTO PERSON (Ssn,FirstName,LastName,BirthDate,EMail,Address) VALUES (" + ssn + "," + firstName + "," + lastName + "," + birthDate + "," + eMail + "," + address + ")", connection);
            cmd.ExecuteNonQuery();
        }

        public void makeProvinceStateRegionInsert(string name, string country)
        {
            SqlCommand cmd = new SqlCommand(@"INSERT INTO PROVINCE_STATE_REGION (Name,Country) VALUES (" + name + "," + country + ")", connection);
            cmd.ExecuteNonQuery();
        }

        public void makeSanitaryMeasurementsInsert(string id, string name, string description)
        {
            SqlCommand cmd = new SqlCommand(@"INSERT INTO SANITARY_MEASUREMENTS (Id,Name,Description) VALUES (" + id + "," + name + "," + description + ")", connection);
            cmd.ExecuteNonQuery();
        }

        public void makeStateInsert(string name)
        {
            SqlCommand cmd = new SqlCommand(@"INSERT INTO STATE (Name) VALUES (" + name + ")", connection);
            cmd.ExecuteNonQuery();
        }

    }
}