using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace API.Source.Server_Connections
{
    public class SpecificDelete
    {
        public static SqlConnection connection = DatabaseDataHolder.connect_Database;

        public void makeSpecificContactDelete(string id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(@"DeleteContact @Ssn = '" + id + "'", connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
            }
        }
        public void makeSpecificEnforcesDelete(string id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(@"DELETE FROM ENFORCES WHERE Id = " + id, connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
            }
        }
        public void makeSpecificHospitalDelete(string id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(@"DELETE FROM HOSPITAL WHERE Id = " + id, connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
            }
        }
        public void makeSpecificMedicationDelete(string id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(@"DELETE FROM MEDICATION WHERE Id = " + id, connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
            }
        }
        public void makeSpecificPathologyDelete(string id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(@"DELETE FROM PATHOLOGY WHERE Id = " + id, connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
            }
        }
        public void makeSpecificPatientDelete(string id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(@"DELETE FROM PATIENT WHERE Ssn = '" + id + "'", connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
            }
        }
        public void makeSpecificPatientMedicationDelete(string id, string name)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(@"DeleteMedicationsFromPatient @Ssn = '" + id + "', @MedicationName = '" + name + "'", connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
            }
        }
        public void makeSpecificPatientPathologiesDelete(string id, string name)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(@"DeletePathologiesFromPatient @Ssn = '" + id + "', @PathologyName = '" + name + "'", connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
            }
        }
        public void makeSpecificPatientStateDelete(string id, string name)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(@"DeleteStateFromPatient @Ssn = '" + id  + "', @StateName = '" + name + "'", connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
            }
        }
        public void makeSpecificPersonDelete(string id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(@"DELETE FROM PERSON WHERE Ssn = '" + id + "'", connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
            }
        }
        public void makeSpecificProvinceStateRegionDelete(string id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(@"DELETE FROM PROVINCE_STATE_REGION WHERE Id = " + id, connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
            }
        }
        public void makeSpecificSanitaryMeasurementsDelete(string id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(@"DELETE FROM SANITARY_MEASUREMENTS WHERE Id = " + id, connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
            }
        }
        public void makeSpecificStateDelete(string id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(@"DELETE FROM STATE WHERE Id = " + id, connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
            }
            
        }
    }
}