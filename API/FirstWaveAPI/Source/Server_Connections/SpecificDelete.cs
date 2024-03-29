﻿using System;
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

        /// <summary>
        /// Function in charge of deleting a contact
        /// </summary>
        /// <param name="personSsn">
        /// </param>
        /// <param name="patientSsn"></param>
        public void makeSpecificContactDelete(string personSsn, string patientSsn)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(@"DeleteContact @PersonSsn = '" + personSsn + "', @PatientSsn = '" + patientSsn + "'", connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
            }
        }

        /// <summary>
        /// Function in charge of deleting a country measure
        /// </summary>
        /// <param name="id"></param>
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

        /// <summary>
        /// Function in charge of deleting a hospital
        /// </summary>
        /// <param name="id"></param>
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

        /// <summary>
        /// Function in charge of deleting medication
        /// </summary>
        /// <param name="id"></param>
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

        /// <summary>
        /// Function in charge of deleting a pathology
        /// </summary>
        /// <param name="id"></param>
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

        /// <summary>
        /// Function in charge of deleting a patient
        /// </summary>
        /// <param name="id"></param>
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

        /// <summary>
        /// Function in charge of deleting a patient medication
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
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

        /// <summary>
        /// Function in charge of deleting a patient pathology
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
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

        /// <summary>
        /// Function in charge of deleting a patient state
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
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

        /// <summary>
        /// Function in charge of deleting a person
        /// </summary>
        /// <param name="id"></param>
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

        /// <summary>
        /// Function in charge of deleting a region
        /// </summary>
        /// <param name="id"></param>
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

        /// <summary>
        /// Function in charge of deleting a sanitary measure
        /// </summary>
        /// <param name="id"></param>
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

        /// <summary>
        /// Function in charge of deleting a state
        /// </summary>
        /// <param name="id"></param>
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