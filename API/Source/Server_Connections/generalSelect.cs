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

namespace API.Source.Server_Connections
{
    public class GeneralSelect
    {
        
        public static SqlConnection connection = DatabaseDataHolder.connect_Database;
       
        public IEnumerable<Contact> makeContactSelect(string firstCol, string secondCol)
        {
            var objectList = new List<Contact>();
            string sql = @"SELECT " + firstCol + "," + secondCol + " FROM CONTACT";
            SqlCommand cmd = new SqlCommand(sql, connection);
            var sqlReader = cmd.ExecuteReader();
            
            while (sqlReader.Read())
            {
                var contact = new Contact();
                contact.person = (int)sqlReader[0];
                contact.patient = (int)sqlReader[1];
                objectList.Add(contact);
            }
            return objectList;
        }

        public IEnumerable<Continent> makeContinentSelect(string firstCol)
        {
            var objectList = new List<Continent>();
            string sql = @"SELECT " + firstCol + " FROM CONTINENT";
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

        public IEnumerable<Country> makeCountrySelect(string firstCol, string secondCol)
        {
            var objectList = new List<Country>();
            string sql = @"SELECT " + firstCol + "," + secondCol + " FROM COUNTRY";
            SqlCommand cmd = new SqlCommand(sql, connection);
            var sqlReader = cmd.ExecuteReader();

            while (sqlReader.Read())
            {
                var country = new Country();
                country.name = (string)sqlReader[0];
                country.continentName = (string)sqlReader[1];
                objectList.Add(country);
            }
            return objectList;
        }

        public IEnumerable<Enforces> makeEnforcesSelect()
        {
            var objectList = new List<Enforces>();
            string sql = @"SELECT * FROM ENFORCES";
            SqlCommand cmd = new SqlCommand(sql, connection);
            var sqlReader = cmd.ExecuteReader();

            while (sqlReader.Read())
            {
                var enforce = new Enforces();
                enforce.country = (string)sqlReader[0];
                enforce.measurement = (int)sqlReader[1];
                enforce.startDate = (string)sqlReader[2];
                enforce.finalDate = (string)sqlReader[3];
                objectList.Add(enforce);
            }
            return objectList;
        }

        public IEnumerable<Hospital> makeHospitalSelect()
        {
            var objectList = new List<Hospital>();
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
                objectList.Add(hospital);
            }
            return objectList;
        }

        public IEnumerable<Medication> makeMedicationSelect()
        {
            var objectList = new List<Medication>();
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

        public IEnumerable<MedicationSupply> makeMedicationSupplySelect()
        {
            var objectList = new List<MedicationSupply>();
            string sql = @"SELECT * FROM MEDICATION_SUPPLY";
            SqlCommand cmd = new SqlCommand(sql, connection);
            var sqlReader = cmd.ExecuteReader();

            while (sqlReader.Read())
            {
                var medicationSupply = new MedicationSupply();
                medicationSupply.medication = (int)sqlReader[0];
                medicationSupply.hospital = (int)sqlReader[1];
                medicationSupply.quantity = (int)sqlReader[2];
                objectList.Add(medicationSupply);
            }
            return objectList;
        }

        public IEnumerable<Pathology> makePathologySelect()
        {
            var objectList = new List<Pathology>();
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
                objectList.Add(pathology);
            }
            return objectList;
        }

        public IEnumerable<Patient> makePatientSelect()
        {
            var objectList = new List<Patient>();
            string sql = @"SELECT * FROM PATIENT";
            SqlCommand cmd = new SqlCommand(sql, connection);
            var sqlReader = cmd.ExecuteReader();

            while (sqlReader.Read())
            {
                var patient = new Patient();
                patient.ssn = (int)sqlReader[0];
                patient.firstName = (string)sqlReader[1];
                patient.lastName = (string)sqlReader[2];
                patient.age = (int)sqlReader[3];
                patient.hospitalized = (bool)sqlReader[4];
                patient.icu = (bool)sqlReader[5];
                patient.state = (string)sqlReader[6];
                patient.country = (string)sqlReader[7];
                patient.region = (string)sqlReader[8];
                patient.nationality = (string)sqlReader[9];
                patient.hospital = (int)sqlReader[10];
                objectList.Add(patient);
            }
            return objectList;
        }

        public IEnumerable<PatientMedication> makePatientMedicationSelect()
        {
            var objectList = new List<PatientMedication>();
            string sql = @"SELECT * FROM PATIENT_MEDICATION";
            SqlCommand cmd = new SqlCommand(sql, connection);
            var sqlReader = cmd.ExecuteReader();

            while (sqlReader.Read())
            {
                var patientMedication = new PatientMedication();
                patientMedication.patient = (int)sqlReader[0];
                patientMedication.medication = (int)sqlReader[1];
                objectList.Add(patientMedication);
            }
            return objectList;
        }

        public IEnumerable<PatientPathologies> makePatientPathologiesSelect()
        {
            var objectList = new List<PatientPathologies>();
            string sql = @"SELECT * FROM PATIENT_PATHOLOGIES";
            SqlCommand cmd = new SqlCommand(sql, connection);
            var sqlReader = cmd.ExecuteReader();

            while (sqlReader.Read())
            {
                var patientPathologie = new PatientPathologies();
                patientPathologie.patient = (int)sqlReader[0];
                patientPathologie.pathology = (string)sqlReader[1];
                objectList.Add(patientPathologie);
            }
            return objectList;
        }

        public IEnumerable<PatientState> makePatientStateSelect()
        {
            var objectList = new List<PatientState>();
            string sql = @"SELECT * FROM PATIENT_STATE";
            SqlCommand cmd = new SqlCommand(sql, connection);
            var sqlReader = cmd.ExecuteReader();

            while (sqlReader.Read())
            {
                var patientState = new PatientState();
                patientState.state = (string)sqlReader[0];
                patientState.patient = (int)sqlReader[1];
                patientState.date = (string)sqlReader[2];
                objectList.Add(patientState);
            }
            return objectList;
        }

        public IEnumerable<Person> makePersonSelect()
        {
            var objectList = new List<Person>();
            string sql = @"SELECT * FROM PERSON";
            SqlCommand cmd = new SqlCommand(sql, connection);
            var sqlReader = cmd.ExecuteReader();

            while (sqlReader.Read())
            {
                var person = new Person();
                person.ssn = (int)sqlReader[0];
                person.firstName = (string)sqlReader[1];
                person.lastName = (string)sqlReader[2];
                person.age = (int)sqlReader[3];
                person.eMail = (string)sqlReader[4];
                person.address = (string)sqlReader[5];
                objectList.Add(person);
            }
            return objectList;
        }

        public IEnumerable<ProvinceStateRegion> makeProvinceStateRegionSelect()
        {
            var objectList = new List<ProvinceStateRegion>();
            string sql = @"SELECT * FROM PROVINCE_STATE_REGION";
            SqlCommand cmd = new SqlCommand(sql, connection);
            var sqlReader = cmd.ExecuteReader();

            while (sqlReader.Read())
            {
                var provinceStateRegion = new ProvinceStateRegion();
                provinceStateRegion.name = (string)sqlReader[0];
                provinceStateRegion.country = (string)sqlReader[1];
                objectList.Add(provinceStateRegion);
            }
            return objectList;
        }

        public IEnumerable<SanitaryMeasurements> makeSanitaryMeasurementsSelect()
        {
            var objectList = new List<SanitaryMeasurements>();
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

        public IEnumerable<State> makeStateSelect()
        {
            var objectList = new List<State>();
            string sql = @"SELECT * FROM STATE";
            SqlCommand cmd = new SqlCommand(sql, connection);
            var sqlReader = cmd.ExecuteReader();

            while (sqlReader.Read())
            {
                var state = new State();
                state.name = (string)sqlReader[0];
                objectList.Add(state);
            }
            return objectList;
        }
    } 
}
