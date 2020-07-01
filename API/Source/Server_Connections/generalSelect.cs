using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Web;
using API.Source.Server_Connections;
using API.Source.Entities;
using System.IO;
using System.Drawing.Printing;

namespace API.Source.Server_Connections
{
    public class GeneralSelect
    {
        
        public static SqlConnection connection = DatabaseDataHolder.connect_Database;
       
        public IEnumerable<Contact> makeContactSelect()
        {
            var objectList = new List<Contact>();
            try
            {
                string sql = @"SELECT * FROM CONTACT";
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

        public IEnumerable<Continent> makeContinentSelect()
        {
            var objectList = new List<Continent>();
            try
            {
                string sql = @"SELECT * FROM CONTINENT";
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

        public IEnumerable<Country> makeCountrySelect()
        {
            var objectList = new List<Country>();
            try
            {
                string sql = @"SELECT * FROM COUNTRY";
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

        public IEnumerable<string> makeCountryNamesSelect()
        {
            var objectList = new List<string>();
            try
            {
                string sql = @"SELECT Name FROM COUNTRY";
                SqlCommand cmd = new SqlCommand(sql, connection);
                var sqlReader = cmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    var country = (string)sqlReader[0];
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

        public IEnumerable<EnforcesId> makeEnforcesIDSelect()
        {
            var objectList = new List<EnforcesId>();
            try
            {
                string sql = @"SELECT * FROM ENFORCES";
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

        public IEnumerable<EnforcesName> makeEnforcesNameSelect()
        {
            var objectList = new List<EnforcesName>();
            try
            {
                string sql = @"GetCountryMeasures";
                SqlCommand cmd = new SqlCommand(sql, connection);
                var sqlReader = cmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    var enforces = new EnforcesName();
                    enforces.country = (string)sqlReader[0];
                    enforces.measurementName = (string)sqlReader[1];
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

        public IEnumerable<Hospital> makeHospitalSelect()
        {
            var objectList = new List<Hospital>();
            try
            {
                string sql = @"SELECT * FROM HOSPITAL";
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

        public IEnumerable<Medication> makeMedicationSelect()
        {
            var objectList = new List<Medication>();
            try
            {
                string sql = @"SELECT * FROM MEDICATION";
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

        public IEnumerable<string> makeMedicationNamesSelect()
        {
            var objectList = new List<string>();
            try
            {
                string sql = @"SELECT Name FROM MEDICATION";
                SqlCommand cmd = new SqlCommand(sql, connection);
                var sqlReader = cmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    var medication = (string)sqlReader[0];
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

        public IEnumerable<Pathology> makePathologySelect()
        {
            var objectList = new List<Pathology>();
            try
            {
                string sql = @"SELECT * FROM PATHOLOGY";
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

        public IEnumerable<string> makePathologyNamesSelect()
        {
            var objectList = new List<string>();
            try
            {
                string sql = @"SELECT Name FROM PATHOLOGY";
                SqlCommand cmd = new SqlCommand(sql, connection);
                var sqlReader = cmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    var pathology = (string)sqlReader[0];
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

        public IEnumerable<PatientWithHospitalId> makePatientSelect()
        {
            var objectList = new List<PatientWithHospitalId>();
            try
            {
                string sql = @"SELECT * FROM PATIENT";
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

        public IEnumerable<PatientMedication> makePatientMedicationSelect()
        {
            var objectList = new List<PatientMedication>();
            try
            {
                string sql = @"SELECT * FROM PATIENT_MEDICATION";
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

        public IEnumerable<SpecialPatientMedication> makeSpecialPatientMedicationSelect(string id)
        {
            var objectList = new List<SpecialPatientMedication>();
            try
            {
                string sql = @"GetMedicationsFromPatient @Ssn = '" + id + "'";
                SqlCommand cmd = new SqlCommand(sql, connection);
                var sqlReader = cmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    var patientMedication = new SpecialPatientMedication();
                    patientMedication.name = (string)sqlReader[0];
                    patientMedication.pharmacy = (string)sqlReader[1];
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

        public IEnumerable<PatientPathologies> makePatientPathologiesSelect()
        {
            var objectList = new List<PatientPathologies>();
            try
            {
                string sql = @"SELECT * FROM PATIENT_PATHOLOGIES";
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

        public IEnumerable<SpecialPatientPathologies> makeSpecialPatientPathologiesSelect(string id)
        {
            var objectList = new List<SpecialPatientPathologies>();
            try
            {
                string sql = @"GetPathologiesFromPatient @Ssn = '" + id + "'";
                SqlCommand cmd = new SqlCommand(sql, connection);
                var sqlReader = cmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    var patientPathologie = new SpecialPatientPathologies();
                    patientPathologie.name = (string)sqlReader[0];
                    patientPathologie.symptoms = (string)sqlReader[1];
                    patientPathologie.description = (string)sqlReader[2];
                    patientPathologie.treatment = (string)sqlReader[3];
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

        public IEnumerable<PatientState> makePatientStateSelect()
        {
            var objectList = new List<PatientState>();
            try
            {
                string sql = @"SELECT * FROM PATIENT_STATE";
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

        public IEnumerable<SpecialPatientState> makeSpecialPatientStateSelect(string id)
        {
            var objectList = new List<SpecialPatientState>();
            try
            {
                string sql = @"GetStatesFromPatient @Ssn = '" + id + "'";
                SqlCommand cmd = new SqlCommand(sql, connection);
                var sqlReader = cmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    var patientState = new SpecialPatientState();
                    patientState.name = (string)sqlReader[0];
                    DateTime date = (DateTime)sqlReader[1];
                    patientState.date = date.ToShortDateString();
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

        public IEnumerable<Person> makePersonSelect()
        {
            var objectList = new List<Person>();
            try
            {
                string sql = @"SELECT * FROM PERSON";
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

        public IEnumerable<PersonWithDateOfContact> makePersonSelectFromPatient(string id)
        {
            var objectList = new List<PersonWithDateOfContact>();
            try
            {
                string sql = @"GetContactsFromPatient @Ssn = '" + id + "'";
                SqlCommand cmd = new SqlCommand(sql, connection);
                var sqlReader = cmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    var person = new PersonWithDateOfContact();
                    person.ssn = (string)sqlReader[0];
                    person.firstName = (string)sqlReader[1];
                    person.lastName = (string)sqlReader[2];
                    DateTime birthDate = (DateTime)sqlReader[3];
                    person.birthDate = birthDate.ToShortDateString();
                    person.eMail = (string)sqlReader[4];
                    person.address = (string)sqlReader[5];
                    person.sex = (string)sqlReader[6];
                    DateTime contactDate = (DateTime)sqlReader[7];
                    person.contactDate = contactDate.ToShortDateString();
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

        public IEnumerable<ProvinceStateRegion> makeProvinceStateRegionSelect()
        {
            var objectList = new List<ProvinceStateRegion>();
            try
            {
                string sql = @"SELECT * FROM PROVINCE_STATE_REGION";
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

        public IEnumerable<string> makeProvinceStateRegionNamesSelect()
        {
            var objectList = new List<string>();
            try
            {
                string sql = @"SELECT Name FROM PROVINCE_STATE_REGION";
                SqlCommand cmd = new SqlCommand(sql, connection);
                var sqlReader = cmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    var provinceStateRegion = (string)sqlReader[0];
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

        public IEnumerable<SanitaryMeasurements> makeSanitaryMeasurementsSelect()
        {
            var objectList = new List<SanitaryMeasurements>();
            try
            {
                string sql = @"SELECT * FROM SANITARY_MEASUREMENTS";
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

        public IEnumerable<string> makeSanitaryMeasurementsNamesSelect()
        {
            var objectList = new List<string>();
            try
            {
                string sql = @"SELECT Name FROM SANITARY_MEASUREMENTS";
                SqlCommand cmd = new SqlCommand(sql, connection);
                var sqlReader = cmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    var sanitaryMeasurement = (string)sqlReader[0];
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

        public IEnumerable<State> makeStateSelect()
        {
            var objectList = new List<State>();
            try
            {
                string sql = @"SELECT * FROM STATE";
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

        public IEnumerable<string> makeStateNamesSelect()
        {
            var objectList = new List<string>();
            try
            {
                string sql = @"SELECT Name FROM STATE";
                SqlCommand cmd = new SqlCommand(sql, connection);
                var sqlReader = cmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    var state = (string)sqlReader[0];
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
