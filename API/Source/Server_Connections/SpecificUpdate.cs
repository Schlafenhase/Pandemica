using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace API.Source.Server_Connections
{
    public class SpecificUpdate
    {
        public static SqlConnection connection = DatabaseDataHolder.connect_Database;

        public void makeSpecificContactUpdateByPerson(string id)
        {
           
        }
        public void makeSpecificContactUpdateByPatient(string id)
        {
           
        }
        public void makeSpecificContinentUpdateByName(string name)
        {
            
        }
        public void makeSpecificCountryUpdateByName(string name, string continentName, string eMail)
        {
            SqlCommand cmd = new SqlCommand(@"UPDATE COUNTRY SET ContinentName = " + continentName + ", EMail = " + eMail + "WHERE Name = " + name, connection);
            cmd.ExecuteNonQuery();
        }
        public void makeSpecificEnforcesUpdateByCountry(string name, string startDate, string finalDate)
        {
            SqlCommand cmd = new SqlCommand(@"UPDATE ENFORCES SET StartDate = " + startDate + ", FinalDate = " + finalDate + "WHERE Country = " + name, connection);
            cmd.ExecuteNonQuery();
        }
        public void makeSpecificEnforcesUpdateByMeasurement(string id, string startDate, string finalDate)
        {
            SqlCommand cmd = new SqlCommand(@"UPDATE ENFORCES SET StartDate = " + startDate + ", FinalDate = " + finalDate + "WHERE Measurement = " + id, connection);
            cmd.ExecuteNonQuery();
        }
        public void makeSpecificHospitalUpdateById(string id, string name, string phone, string managerName, string capacity, string icuCapacity, string country, string region, string eMail)
        {
            SqlCommand cmd = new SqlCommand(@"UPDATE HOSPITAL SET Name = " + name + ", Phone = " + phone + ", ManagerName = " + managerName + ", Capacity = " + capacity + ", ICUCapacity = " + icuCapacity + ", Country = " + country + ", Region = " + region + ", EMail = " + eMail + "WHERE Id = " + id, connection);
            cmd.ExecuteNonQuery();
        }
        public void makeSpecificMedicationUpdateById(string id, string name, string pharmacy)
        {
            SqlCommand cmd = new SqlCommand(@"UPDATE MEDICATION SET Name = " + name + ", Pharmacy = " + pharmacy + "WHERE Id = " + id, connection);
            cmd.ExecuteNonQuery();
        }
        public void makeSpecificMedicationSupplyUpdateByMedication(string id, string quantity)
        {
            SqlCommand cmd = new SqlCommand(@"UPDATE MEDICATION_SUPPLY SET Quantity = " + quantity + "WHERE Medication = " + id, connection);
            cmd.ExecuteNonQuery();
        }
        public void makeSpecificMedicationSupplyUpdateByHospital(string id, string quantity)
        {
            SqlCommand cmd = new SqlCommand(@"UPDATE MEDICATION_SUPPLY SET Quantity = " + quantity + "WHERE Hospital = " + id, connection);
            cmd.ExecuteNonQuery();
        }
        public void makeSpecificPathologyUpdateById(string id, string name, string symptoms, string description, string treatment)
        {
            SqlCommand cmd = new SqlCommand(@"UPDATE PATHOLOGY SET Name = " + name + ", Symptoms = " + symptoms + ", Description = " + description + ", Treatment = " + treatment + "WHERE Id = " + id, connection);
            cmd.ExecuteNonQuery();
        }
        public void makeSpecificPatientUpdateById(string id, string firstName, string lastName, string birthDate, string hospitalized, string icu, string country, string region, string nationality, string hospital)
        {
            SqlCommand cmd = new SqlCommand(@"UPDATE PATIENT SET FirstName = " + firstName + ", LastName = " + lastName + ", BirthDate = " + birthDate + ", Hospitalized = " + hospitalized + ", ICU = " + icu + ", Country = " + country + ", Region = " + region + ", Nationality = " + nationality + ", Hospital = " + hospital + "WHERE Ssn = " + id, connection);
            cmd.ExecuteNonQuery();
        }
        public void makeSpecificPatientMedicationUpdateByPatient(string id)
        {
            
        }
        public void makeSpecificPatientMedicationUpdateByMedication(string id)
        {
            
        }
        public void makeSpecificPatientPathologiesUpdateByPatient(string id)
        {
            
        }
        public void makeSpecificPatientPathologiesUpdateByPathology(string id)
        {
            
        }
        public void makeSpecificPatientStateUpdateByState(string id, string date)
        {
            SqlCommand cmd = new SqlCommand(@"UPDATE PATIENT_STATE SET Date = " + date + "WHERE State = " + id, connection);
            cmd.ExecuteNonQuery();
        }
        public void makeSpecificPatientStateUpdateByPatient(string id, string date)
        {
            SqlCommand cmd = new SqlCommand(@"UPDATE PATIENT_STATE SET Date = " + date + "WHERE Patient = " + id, connection);
            cmd.ExecuteNonQuery();
        }
        public void makeSpecificPersonUpdateById(string id, string firstName, string lastName, string birthDate, string eMail, string address)
        {
            SqlCommand cmd = new SqlCommand(@"UPDATE PERSON SET FirstName = " + firstName + ", LastName = " + lastName + ", BirthDate = " + birthDate + ", EMail = " + eMail + ", Address = " + address + "WHERE Ssn = " + id, connection);
            cmd.ExecuteNonQuery();
        }
        public void makeSpecificProvinceStateRegionUpdateByName(string name, string country)
        {
            SqlCommand cmd = new SqlCommand(@"UPDATE PROVINCE_STATE_REGION SET Country = " + country + "WHERE Name = " + name, connection);
            cmd.ExecuteNonQuery();
        }
        public void makeSpecificProvinceStateRegionUpdateByCountry(string country, string name)
        {
            SqlCommand cmd = new SqlCommand(@"UPDATE PROVINCE_STATE_REGION SET Name = " + name + "WHERE Country = " + country, connection);
            cmd.ExecuteNonQuery();
        }
        public void makeSpecificSanitaryMeasurementsUpdateById(string id, string name, string description)
        {
            SqlCommand cmd = new SqlCommand(@"UPDATE SANITARY_MEASUREMENTS SET Name = " + name + ", Description = " + description + "WHERE Id = " + id, connection);
            cmd.ExecuteNonQuery();
        }
        public void makeSpecificStateUpdateById(string id, string name)
        {
            SqlCommand cmd = new SqlCommand(@"UPDATE STATE SET Name = " + name + "WHERE Id = " + id, connection);
            cmd.ExecuteNonQuery();
        }
    }
}