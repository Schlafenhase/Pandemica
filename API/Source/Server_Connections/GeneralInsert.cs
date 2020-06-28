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

        public void makeContactInsert(string firstCol, string secondCol)
        {
            SqlCommand cmd = new SqlCommand(@"INSERT INTO CONTACT (Person,Patient) VALUES (" + firstCol + "," + secondCol + ")", connection);
            cmd.ExecuteNonQuery();
        }

        public void makeContinentInsert(string firstCol)
        {
            SqlCommand cmd = new SqlCommand(@"INSERT INTO CONTINENT (Name) VALUES (" + firstCol + ")", connection);
            cmd.ExecuteNonQuery();
        }

        public void makeCountryInsert(string firstCol, string secondCol, string thirdCol)
        {
            SqlCommand cmd = new SqlCommand(@"INSERT INTO COUNTRY (Name,ContinentName,EMail) VALUES (" + firstCol + "," + secondCol + "," + thirdCol + ")", connection);
            cmd.ExecuteNonQuery();
        }

        public void makeEnforcesInsert(string firstCol, string secondCol, string thirdCol, string fourthCol)
        {
            SqlCommand cmd = new SqlCommand(@"INSERT INTO ENFORCES (Country,Measurement,StartDate,FinalDate) VALUES (" + firstCol + "," + secondCol + "," + thirdCol + "," + fourthCol + ")", connection);
            cmd.ExecuteNonQuery();
        }

        public void makeHospitalInsert(string firstCol, string secondCol, string thirdCol, string fourthCol, string fifthCol, string sixthCol, string seventhCol, string eigthCol, string ninthCol)
        {
            SqlCommand cmd = new SqlCommand(@"INSERT INTO HOSPITAL (Id,Name,Phone,ManagerName,Capacity,ICUCapacity,Country,Region,EMail) VALUES (" + firstCol + "," + secondCol + "," + thirdCol + "," + fourthCol + "," + fifthCol + "," + sixthCol + "," + seventhCol + "," + eigthCol + "," + ninthCol + ")", connection);
            cmd.ExecuteNonQuery();
        }

        public void makeMedicationInsert(string firstCol, string secondCol, string thirdCol)
        {
            SqlCommand cmd = new SqlCommand(@"INSERT INTO MEDICATION (Id,Name,Pharmacy) VALUES (" + firstCol + "," + secondCol + "," + thirdCol + ")", connection);
            cmd.ExecuteNonQuery();
        }

        public void makeMedicationSupplyInsert(string firstCol, string secondCol, string thirdCol)
        {
            SqlCommand cmd = new SqlCommand(@"INSERT INTO MEDICATION_SUPPLY (Medication,Hospital,Quantity) VALUES (" + firstCol + "," + secondCol + "," + thirdCol + ")", connection);
            cmd.ExecuteNonQuery();
        }

        public void makePathologyInsert(string firstCol, string secondCol, string thirdCol, string fourthCol)
        {
            SqlCommand cmd = new SqlCommand(@"INSERT INTO PATHOLOGY (Name,Symptoms,Description,Treatment) VALUES (" + firstCol + "," + secondCol + "," + thirdCol + "," + fourthCol + ")", connection);
            cmd.ExecuteNonQuery();
        }

        public void makePatientInsert(string firstCol, string secondCol, string thirdCol, string fourthCol, string fifthCol, string sixthCol, string seventhCol, string eigthCol, string ninethCol, string tenthCol)
        {
            SqlCommand cmd = new SqlCommand(@"INSERT INTO PATIENT (Ssn,FirstName,LastName,BirthDate,Hospitalized,ICU,Country,Region,Nationality,Hospital) VALUES (" + firstCol + "," + secondCol + "," + thirdCol + "," + fourthCol + "," + fifthCol + "," + sixthCol + "," + seventhCol + "," + eigthCol + "," + ninethCol + "," + tenthCol + ")", connection);
            cmd.ExecuteNonQuery();
        }

        public void makePatientMedicationInsert(string firstCol, string secondCol)
        {
            SqlCommand cmd = new SqlCommand(@"INSERT INTO PATIENT_MEDICATION (Patient,Medication) VALUES (" + firstCol + "," + secondCol + ")", connection);
            cmd.ExecuteNonQuery();
        }

        public void makePatientPathologiesInsert(string firstCol, string secondCol)
        {
            SqlCommand cmd = new SqlCommand(@"INSERT INTO PATIENT_PATHOLOGIES (Patient,Pathology) VALUES (" + firstCol + "," + secondCol + ")", connection);
            cmd.ExecuteNonQuery();
        }

        public void makePatientStateInsert(string firstCol, string secondCol, string thirdCol)
        {
            SqlCommand cmd = new SqlCommand(@"INSERT INTO PATIENT_STATE (State,Patient,Date) VALUES (" + firstCol + "," + secondCol + "," + thirdCol + ")", connection);
            cmd.ExecuteNonQuery();
        }

        public void makePersonInsert(string firstCol, string secondCol, string thirdCol, string fourthCol, string fifthCol, string sixthCol)
        {
            SqlCommand cmd = new SqlCommand(@"INSERT INTO PERSON (Ssn,FirstName,LastName,BirthDate,EMail,Address) VALUES (" + firstCol + "," + secondCol + "," + thirdCol + "," + fourthCol + "," + fifthCol + "," + sixthCol + ")", connection);
            cmd.ExecuteNonQuery();
        }

        public void makeProvinceStateRegionInsert(string firstCol, string secondCol)
        {
            SqlCommand cmd = new SqlCommand(@"INSERT INTO PROVINCE_STATE_REGION (Name,Country) VALUES (" + firstCol + "," + secondCol + ")", connection);
            cmd.ExecuteNonQuery();
        }

        public void makeSanitaryMeasurementsInsert(string firstCol, string secondCol, string thirdCol)
        {
            SqlCommand cmd = new SqlCommand(@"INSERT INTO SANITARY_MEASUREMENTS (Id,Name,Description) VALUES (" + firstCol + "," + secondCol + "," + thirdCol + ")", connection);
            cmd.ExecuteNonQuery();
        }

        public void makeStateInsert(string firstCol)
        {
            SqlCommand cmd = new SqlCommand(@"INSERT INTO STATE (Name) VALUES (" + firstCol + ")", connection);
            cmd.ExecuteNonQuery();
        }

    }
}