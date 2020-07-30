using API.Source.Entities;
using CrystalDecisions.Web.HtmlReportRender;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace API.Source.Server_Connections.Specific_Selects
{
    public class SpecificSelect
    {
        public static SqlConnection connection = DatabaseDataHolder.connect_Database;

        /// <summary>
        /// Function in charge of selecting contacts from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<Contact> makeSpecificContactSelectByPerson(string id)
        {
            var objectList = new List<Contact>();
            try
            {
                string sql = @"SELECT * FROM CONTACT WHERE Person = " + id;
                SqlCommand cmd = new SqlCommand(sql, connection);
                var sqlReader = cmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    var contact = new Contact();
                    contact.person = (string)sqlReader[0];
                    contact.patient = (string)sqlReader[1];
                    contact.id = (int)sqlReader[2];
                    objectList.Add(contact);
                }
                return objectList;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
                return objectList;
            }
        }

        /// <summary>
        /// Function in charge of selecting contacts from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<Contact> makeSpecificContactSelectByPatient(string id)
        {
            var objectList = new List<Contact>();
            try
            {
                string sql = @"SELECT * FROM CONTACT WHERE Patient = " + id;
                SqlCommand cmd = new SqlCommand(sql, connection);
                var sqlReader = cmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    var contact = new Contact();
                    contact.person = (string)sqlReader[0];
                    contact.patient = (string)sqlReader[1];
                    contact.id = (int)sqlReader[2];
                    objectList.Add(contact);
                }
                return objectList;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
                return objectList;
            }
        }

        /// <summary>
        /// Function in charge of selecting continents from the database
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IEnumerable<Continent> makeSpecificContinentSelectByName(string name)
        {
            var objectList = new List<Continent>();
            try
            {
                string sql = @"SELECT * FROM CONTINENT WHERE Name = " + name;
                SqlCommand cmd = new SqlCommand(sql, connection);
                var sqlReader = cmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    var continent = new Continent();
                    continent.name = (string)sqlReader[0];
                    objectList.Add(continent);
                }
                return objectList;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
                return objectList;
            }
        }

        /// <summary>
        /// Function in charge of selecting countries from the database
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IEnumerable<Country> makeSpecificCountrySelectByName(string name)
        {
            var objectList = new List<Country>();
            try
            {
                string sql = @"SELECT * FROM COUNTRY WHERE Name = " + name;
                SqlCommand cmd = new SqlCommand(sql, connection);
                var sqlReader = cmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    var country = new Country();
                    country.name = (string)sqlReader[0];
                    country.continentName = (string)sqlReader[1];
                    country.eMail = (string)sqlReader[2];
                    objectList.Add(country);
                }
                return objectList;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
                return objectList;
            }
        }

        /// <summary>
        /// Function in charge of selecting countries from the database
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public IEnumerable<Country> makeSpecificCountrySelectByEMail(string email)
        {
            var objectList = new List<Country>();
            try
            {
                string sql = @"SELECT * FROM COUNTRY WHERE EMail = '" + email + "'";
                SqlCommand cmd = new SqlCommand(sql, connection);
                var sqlReader = cmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    var country = new Country();
                    country.name = (string)sqlReader[0];
                    country.continentName = (string)sqlReader[1];
                    country.eMail = (string)sqlReader[2];
                    objectList.Add(country);
                }
                return objectList;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
                return objectList;
            }
        }

        /// <summary>
        /// Function in charge of selecting country measures from the database
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IEnumerable<EnforcesId> makeSpecificEnforcesSelectByCountry(string name)
        {
            var objectList = new List<EnforcesId>();
            try
            {
                string sql = @"SELECT * FROM ENFORCES WHERE Country = " + name;
                SqlCommand cmd = new SqlCommand(sql, connection);
                var sqlReader = cmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    var enforces = new EnforcesId();
                    enforces.country = (string)sqlReader[0];
                    enforces.measurement = (int)sqlReader[1];
                    DateTime startDate = (DateTime)sqlReader[2];
                    enforces.startDate = startDate.ToShortDateString();
                    DateTime finalDate = (DateTime)sqlReader[3];
                    enforces.finalDate = finalDate.ToShortDateString();
                    enforces.id = (int)sqlReader[4];
                    objectList.Add(enforces);
                }
                return objectList;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
                return objectList;
            }
        }

        /// <summary>
        /// Function in charge of selecting country measures from the database 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<EnforcesId> makeSpecificEnforcesSelectByMeasurement(string id)
        {
            var objectList = new List<EnforcesId>();
            try
            {
                string sql = @"SELECT * FROM ENFORCES WHERE Measurement = " + id;
                SqlCommand cmd = new SqlCommand(sql, connection);
                var sqlReader = cmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    var enforces = new EnforcesId();
                    enforces.country = (string)sqlReader[0];
                    enforces.measurement = (int)sqlReader[1];
                    DateTime startDate = (DateTime)sqlReader[2];
                    enforces.startDate = startDate.ToShortDateString();
                    DateTime finalDate = (DateTime)sqlReader[3];
                    enforces.finalDate = finalDate.ToShortDateString();
                    enforces.id = (int)sqlReader[4];
                    objectList.Add(enforces);
                }
                return objectList;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
                return objectList;
            }
        }

        /// <summary>
        /// Function in charge of selecting hospitals from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<Hospital> makeSpecificHospitalSelectById(string id)
        {
            var objectList = new List<Hospital>();
            try
            {
                string sql = @"SELECT * FROM HOSPITAL WHERE Id = " + id;
                SqlCommand cmd = new SqlCommand(sql, connection);
                var sqlReader = cmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    var hospital = new Hospital();
                    hospital.id = (int)sqlReader[0];
                    hospital.name = (string)sqlReader[1];
                    hospital.phone = (int)sqlReader[2];
                    hospital.managerName = (string)sqlReader[3];
                    hospital.capacity = (int)sqlReader[4];
                    hospital.icuCapacity = (int)sqlReader[5];
                    hospital.country = (string)sqlReader[6];
                    hospital.region = (string)sqlReader[7];
                    hospital.eMail = (string)sqlReader[8];
                    objectList.Add(hospital);
                }
                return objectList;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
                return objectList;
            }
        }

        /// <summary>
        /// Function in charge of selecting an hospital from the database
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public IEnumerable<Hospital> makeSpecificHospitalSelectByEMail(string email)
        {
            var objectList = new List<Hospital>();
            try
            {
                string sql = @"SELECT * FROM HOSPITAL WHERE EMail = '" + email + "'";
                SqlCommand cmd = new SqlCommand(sql, connection);
                var sqlReader = cmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    var hospital = new Hospital();
                    hospital.id = (int)sqlReader[0];
                    hospital.name = (string)sqlReader[1];
                    hospital.phone = (int)sqlReader[2];
                    hospital.managerName = (string)sqlReader[3];
                    hospital.capacity = (int)sqlReader[4];
                    hospital.icuCapacity = (int)sqlReader[5];
                    hospital.country = (string)sqlReader[6];
                    hospital.region = (string)sqlReader[7];
                    hospital.eMail = (string)sqlReader[8];
                    objectList.Add(hospital);
                }
                return objectList;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
                return objectList;
            }
        }

        /// <summary>
        /// Function in charge of selecting a medication from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<Medication> makeSpecificMedicationSelectById(string id)
        {
            var objectList = new List<Medication>();
            try
            {
                string sql = @"SELECT * FROM MEDICATION WHERE Id = " + id.ToString();
                SqlCommand cmd = new SqlCommand(sql, connection);
                var sqlReader = cmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    var medication = new Medication();
                    medication.id = (int)sqlReader[0];
                    medication.name = (string)sqlReader[1];
                    medication.pharmacy = (string)sqlReader[2];
                    objectList.Add(medication);
                }
                return objectList;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
                return objectList;
            }
        }

        /// <summary>
        /// Function in charge of selecting pathologies from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<Pathology> makeSpecificPathologySelectById(string id)
        {
            var objectList = new List<Pathology>();
            try
            {
                string sql = @"SELECT * FROM PATHOLOGY WHERE Id = " + id;
                SqlCommand cmd = new SqlCommand(sql, connection);
                var sqlReader = cmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    var pathology = new Pathology();
                    pathology.name = (string)sqlReader[0];
                    pathology.symptoms = (string)sqlReader[1];
                    pathology.description = (string)sqlReader[2];
                    pathology.treatment = (string)sqlReader[3];
                    pathology.id = (int)sqlReader[4];
                    objectList.Add(pathology);
                }
                return objectList;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
                return objectList;
            }
        }

        /// <summary>
        /// Function in charge of selecting patients from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<PatientWithHospitalId> makeSpecificPatientSelectById(string id)
        {
            var objectList = new List<PatientWithHospitalId>();
            try
            {
                string sql = @"SELECT * FROM PATIENT WHERE Ssn = " + id;
                SqlCommand cmd = new SqlCommand(sql, connection);
                var sqlReader = cmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    var patient = new PatientWithHospitalId();
                    patient.ssn = (string)sqlReader[0];
                    patient.firstName = (string)sqlReader[1];
                    patient.lastName = (string)sqlReader[2];
                    DateTime birthDate = (DateTime)sqlReader[3];
                    patient.birthDate = birthDate.ToShortDateString();
                    patient.hospitalized = (bool)sqlReader[4];
                    patient.icu = (bool)sqlReader[5];
                    patient.country = (string)sqlReader[6];
                    patient.region = (string)sqlReader[7];
                    patient.nationality = (string)sqlReader[8];
                    patient.hospital = (int)sqlReader[9];
                    patient.sex = (string)sqlReader[10];
                    objectList.Add(patient);
                }
                return objectList;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
                return objectList;
            }
        }

        /// <summary>
        /// Function in charge of selecting patients from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<Patient> makeSpecificPatientSelectByHospital(string id)
        {
            var objectList = new List<Patient>();
            try
            {
                string sql = @"GetPatientsFromHospital @Id = " + id;
                SqlCommand cmd = new SqlCommand(sql, connection);
                var sqlReader = cmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    var patient = new Patient();
                    patient.ssn = (string)sqlReader[0];
                    patient.firstName = (string)sqlReader[1];
                    patient.lastName = (string)sqlReader[2];
                    DateTime birthDate = (DateTime)sqlReader[3];
                    patient.birthDate = birthDate.ToShortDateString();
                    patient.hospitalized = (bool)sqlReader[4];
                    patient.icu = (bool)sqlReader[5];
                    patient.country = (string)sqlReader[6];
                    patient.region = (string)sqlReader[7];
                    patient.nationality = (string)sqlReader[8];
                    patient.sex = (string)sqlReader[9];
                    objectList.Add(patient);
                }
                return objectList;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
                return objectList;
            }
        }

        /// <summary>
        /// Function in charge of selecting patient medications from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<PatientMedication> makeSpecificPatientMedicationSelectByPatient(string id)
        {
            var objectList = new List<PatientMedication>();
            try
            {
                string sql = @"SELECT * FROM PATIENT_MEDICATION WHERE Patient = " + id;
                SqlCommand cmd = new SqlCommand(sql, connection);
                var sqlReader = cmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    var patientMedication = new PatientMedication();
                    patientMedication.patient = (string)sqlReader[0];
                    patientMedication.medication = (int)sqlReader[1];
                    patientMedication.id = (int)sqlReader[2];
                    objectList.Add(patientMedication);
                }
                return objectList;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
                return objectList;
            }
        }

        /// <summary>
        /// Function in charge of selecting patient medications from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<PatientMedication> makeSpecificPatientMedicationSelectByMedication(string id)
        {
            var objectList = new List<PatientMedication>();
            try
            {
                string sql = @"SELECT * FROM PATIENT_MEDICATION WHERE Medication = " + id.ToString();
                SqlCommand cmd = new SqlCommand(sql, connection);
                var sqlReader = cmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    var patientMedication = new PatientMedication();
                    patientMedication.patient = (string)sqlReader[0];
                    patientMedication.medication = (int)sqlReader[1];
                    patientMedication.id = (int)sqlReader[2];
                    objectList.Add(patientMedication);
                }
                return objectList;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
                return objectList;
            }
        }

        /// <summary>
        /// Function in charge of selecting patient pathologies from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<PatientPathologies> makeSpecificPatientPathologiesSelectByPatient(string id)
        {
            var objectList = new List<PatientPathologies>();
            try
            {
                string sql = @"SELECT * FROM PATIENT_PATHOLOGIES WHERE Patient = " + id;
                SqlCommand cmd = new SqlCommand(sql, connection);
                var sqlReader = cmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    var patientPathologie = new PatientPathologies();
                    patientPathologie.patient = (string)sqlReader[0];
                    patientPathologie.pathology = (int)sqlReader[1];
                    patientPathologie.id = (int)sqlReader[2];
                    objectList.Add(patientPathologie);
                }
                return objectList;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
                return objectList;
            }
        }

        /// <summary>
        /// Function in charge of selecting patient pathologies from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<PatientPathologies> makeSpecificPatientPathologiesSelectByPathology(string id)
        {
            var objectList = new List<PatientPathologies>();
            try
            {
                string sql = @"SELECT * FROM PATIENT_PATHOLOGIES WHERE Pathology = " + id.ToString();
                SqlCommand cmd = new SqlCommand(sql, connection);
                var sqlReader = cmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    var patientPathologie = new PatientPathologies();
                    patientPathologie.patient = (string)sqlReader[0];
                    patientPathologie.pathology = (int)sqlReader[1];
                    patientPathologie.id = (int)sqlReader[2];
                    objectList.Add(patientPathologie);
                }
                return objectList;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
                return objectList;
            }
        }

        /// <summary>
        /// Function in charge of selecting patient states from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<PatientState> makeSpecificPatientStateSelectByState(string id)
        {
            var objectList = new List<PatientState>();
            try
            {
                string sql = @"SELECT * FROM PATIENT_STATE WHERE State = " + id.ToString();
                SqlCommand cmd = new SqlCommand(sql, connection);
                var sqlReader = cmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    var patientState = new PatientState();
                    patientState.state = (int)sqlReader[0];
                    patientState.patient = (string)sqlReader[1];
                    DateTime date = (DateTime)sqlReader[2];
                    patientState.date = date.ToShortDateString();
                    patientState.id = (int)sqlReader[3];
                    objectList.Add(patientState);
                }
                return objectList;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
                return objectList;
            }
        }

        /// <summary>
        /// Function in charge of selecting patient state from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<PatientState> makeSpecificPatientStateSelectByPatient(string id)
        {
            var objectList = new List<PatientState>();
            try
            {
                string sql = @"SELECT * FROM PATIENT_STATE WHERE Patient = " + id;
                SqlCommand cmd = new SqlCommand(sql, connection);
                var sqlReader = cmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    var patientState = new PatientState();
                    patientState.state = (int)sqlReader[0];
                    patientState.patient = (string)sqlReader[1];
                    DateTime date = (DateTime)sqlReader[2];
                    patientState.date = date.ToShortDateString();
                    patientState.id = (int)sqlReader[3];
                    objectList.Add(patientState);
                }
                return objectList;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
                return objectList;
            }
        }

        /// <summary>
        /// Function in charge of selecting persons from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<Person> makeSpecificPersonSelectById(string id)
        {
            var objectList = new List<Person>();
            try
            {
                string sql = @"SELECT * FROM PERSON WHERE Ssn = " + id;
                SqlCommand cmd = new SqlCommand(sql, connection);
                var sqlReader = cmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    var person = new Person();
                    person.ssn = (string)sqlReader[0];
                    person.firstName = (string)sqlReader[1];
                    person.lastName = (string)sqlReader[2];
                    DateTime birthDate = (DateTime)sqlReader[3];
                    person.birthDate = birthDate.ToShortDateString();
                    person.eMail = (string)sqlReader[4];
                    person.address = (string)sqlReader[5];
                    person.sex = (string)sqlReader[6];
                    objectList.Add(person);
                }
                return objectList;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
                return objectList;
            }
        }

        /// <summary>
        /// Function in charge of selecting regions from the database
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IEnumerable<ProvinceStateRegion> makeSpecificProvinceStateRegionSelectByName(string name)
        {
            var objectList = new List<ProvinceStateRegion>();
            try
            {
                string sql = @"SELECT * FROM PROVINCE_STATE_REGION WHERE Name = " + name;
                SqlCommand cmd = new SqlCommand(sql, connection);
                var sqlReader = cmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    var provinceStateRegion = new ProvinceStateRegion();
                    provinceStateRegion.name = (string)sqlReader[0];
                    provinceStateRegion.country = (string)sqlReader[1];
                    provinceStateRegion.id = (int)sqlReader[2];
                    objectList.Add(provinceStateRegion);
                }
                return objectList;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
                return objectList;
            }
        }

        /// <summary>
        /// Function in charge of selecting regions from the database
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IEnumerable<ProvinceStateRegion> makeSpecificProvinceStateRegionSelectByCountry(string name)
        {
            var objectList = new List<ProvinceStateRegion>();
            try
            {
                string sql = @"SELECT * FROM PROVINCE_STATE_REGION WHERE Country = " + name;
                SqlCommand cmd = new SqlCommand(sql, connection);
                var sqlReader = cmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    var provinceStateRegion = new ProvinceStateRegion();
                    provinceStateRegion.name = (string)sqlReader[0];
                    provinceStateRegion.country = (string)sqlReader[1];
                    provinceStateRegion.id = (int)sqlReader[2];
                    objectList.Add(provinceStateRegion);
                }
                return objectList;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
                return objectList;
            }
        }

        /// <summary>
        /// Function in charge of selecting sanitary measures from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<SanitaryMeasurements> makeSpecificSanitaryMeasurementsSelectById(string id)
        {
            var objectList = new List<SanitaryMeasurements>();
            try
            {
                string sql = @"SELECT * FROM SANITARY_MEASUREMENTS WHERE Id = " + id.ToString();
                SqlCommand cmd = new SqlCommand(sql, connection);
                var sqlReader = cmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    var sanitaryMeasurement = new SanitaryMeasurements();
                    sanitaryMeasurement.id = (int)sqlReader[0];
                    sanitaryMeasurement.name = (string)sqlReader[1];
                    sanitaryMeasurement.description = (string)sqlReader[2];
                    objectList.Add(sanitaryMeasurement);
                }
                return objectList;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
                return objectList;
            }
        }

        /// <summary>
        /// Function in charge of selecting state from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<State> makeSpecificStateSelectById(string id)
        {
            var objectList = new List<State>();
            try
            {
                string sql = @"SELECT * FROM STATE WHERE Id = " + id.ToString();
                SqlCommand cmd = new SqlCommand(sql, connection);
                var sqlReader = cmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    var state = new State();
                    state.name = (string)sqlReader[0];
                    state.id = (int)sqlReader[1];
                    objectList.Add(state);
                }
                return objectList;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error happened", ex.Message);
                return objectList;
            }
        }
    }
}