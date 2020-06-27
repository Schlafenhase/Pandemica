using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace API.Source.Server_Connections
{
    public class SpecificDelete
    {
        public static SqlConnection connection = DatabaseDataHolder.connect_Database;

        public void makeSpecificContactDeleteByPerson(string id)
        {
            SqlCommand cmd = new SqlCommand(@"DELETE FROM CONTACT WHERE Person = " + id, connection);
            cmd.ExecuteNonQuery();
        }
        public void makeSpecificContactDeleteByPatient(string id)
        {
            SqlCommand cmd = new SqlCommand(@"DELETE FROM CONTACT WHERE Patient = " + id, connection);
            cmd.ExecuteNonQuery();
        }
        public void makeSpecificContinentDeleteByName(string name)
        {
            SqlCommand cmd = new SqlCommand(@"DELETE FROM CONTINENT WHERE Name = " + name, connection);
            cmd.ExecuteNonQuery();
        }
        public void makeSpecificCountryDeleteByName(string name)
        {
            SqlCommand cmd = new SqlCommand(@"DELETE FROM COUNTRY WHERE Name = " + name, connection);
            cmd.ExecuteNonQuery();
        }
        public void makeSpecificEnforcesDeleteByCountry(string name)
        {
            SqlCommand cmd = new SqlCommand(@"DELETE FROM ENFORCES WHERE Country = " + name, connection);
            cmd.ExecuteNonQuery();
        }
        public void makeSpecificEnforcesDeleteByMeasurement(string id)
        {
            SqlCommand cmd = new SqlCommand(@"DELETE FROM ENFORCES WHERE Measurement = " + id, connection);
            cmd.ExecuteNonQuery();
        }
        public void makeSpecificHospitalDeleteById(string id)
        {
            SqlCommand cmd = new SqlCommand(@"DELETE FROM HOSPITAL WHERE Id = " + id, connection);
            cmd.ExecuteNonQuery();
        }
        public void makeSpecificMedicationDeleteById(string id)
        {
            SqlCommand cmd = new SqlCommand(@"DELETE FROM MEDICATION WHERE Id = " + id, connection);
            cmd.ExecuteNonQuery();
        }
        public void makeSpecificMedicationSupplyDeleteByMedication(string id)
        {
            SqlCommand cmd = new SqlCommand(@"DELETE FROM MEDICATION_SUPPLY WHERE Medication = " + id, connection);
            cmd.ExecuteNonQuery();
        }
        public void makeSpecificMedicationSupplyDeleteByHospital(string id)
        {
            SqlCommand cmd = new SqlCommand(@"DELETE FROM MEDICATION_SUPPLY WHERE Hospital = " + id, connection);
            cmd.ExecuteNonQuery();
        }
        public void makeSpecificPathologyDeleteById(string id)
        {
            SqlCommand cmd = new SqlCommand(@"DELETE FROM PATHOLOGY WHERE Id = " + id, connection);
            cmd.ExecuteNonQuery();
        }
        public void makeSpecificPatientDeleteBySsn(string id)
        {
            SqlCommand cmd = new SqlCommand(@"DELETE FROM PATIENT WHERE Ssn = " + id, connection);
            cmd.ExecuteNonQuery();
        }
        public void makeSpecificPatientMedicationDeleteByPatient(string id)
        {
            SqlCommand cmd = new SqlCommand(@"DELETE FROM PATIENT_MEDICATION WHERE Patient = " + id, connection);
            cmd.ExecuteNonQuery();
        }
        public void makeSpecificPatientMedicationDeleteByMedication(string id)
        {
            SqlCommand cmd = new SqlCommand(@"DELETE FROM PATIENT_MEDICATION WHERE Medication = " + id, connection);
            cmd.ExecuteNonQuery();
        }
        public void makeSpecificPatientPathologiesDeleteByPatient(string id)
        {
            SqlCommand cmd = new SqlCommand(@"DELETE FROM PATIENT_PATHOLOGIES WHERE Patient = " + id, connection);
            cmd.ExecuteNonQuery();
        }
        public void makeSpecificPatientPathologiesDeleteByPathology(string id)
        {
            SqlCommand cmd = new SqlCommand(@"DELETE FROM PATIENT_PATHOLOGIES WHERE Pathology = " + id, connection);
            cmd.ExecuteNonQuery();
        }
        public void makeSpecificPatientStateDeleteByState(string id)
        {
            SqlCommand cmd = new SqlCommand(@"DELETE FROM PATIENT_STATE WHERE State = " + id, connection);
            cmd.ExecuteNonQuery();
        }
        public void makeSpecificPatientStateDeleteByPatient(string id)
        {
            SqlCommand cmd = new SqlCommand(@"DELETE FROM PATIENT_STATE WHERE Patient = " + id, connection);
            cmd.ExecuteNonQuery();
        }
        public void makeSpecificPersonDeleteBySsn(string id)
        {
            SqlCommand cmd = new SqlCommand(@"DELETE FROM PERSON WHERE Ssn = " + id, connection);
            cmd.ExecuteNonQuery();
        }
        public void makeSpecificProvinceStateRegionDeleteByName(string name)
        {
            SqlCommand cmd = new SqlCommand(@"DELETE FROM PROVINCE_STATE_REGION WHERE Name = " + name, connection);
            cmd.ExecuteNonQuery();
        }
        public void makeSpecificProvinceStateRegionDeleteByCountry(string name)
        {
            SqlCommand cmd = new SqlCommand(@"DELETE FROM PROVINCE_STATE_REGION WHERE Country = " + name, connection);
            cmd.ExecuteNonQuery();
        }
        public void makeSpecificSanitaryMeasurementsDeleteById(string id)
        {
            SqlCommand cmd = new SqlCommand(@"DELETE FROM SANITARY_MEASUREMENTS WHERE Id = " + id, connection);
            cmd.ExecuteNonQuery();
        }
        public void makeSpecificStateDeleteById(string id)
        {
            SqlCommand cmd = new SqlCommand(@"DELETE FROM STATE WHERE Id = " + id, connection);
            cmd.ExecuteNonQuery();
        }
    }
}