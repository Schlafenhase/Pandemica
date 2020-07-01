CREATE PROCEDURE GetPatientsFromHospital @Id INT
AS
SELECT PATIENT.Ssn, PATIENT.FirstName, PATIENT.LastName, PATIENT.BirthDate, PATIENT.Hospitalized, PATIENT.ICU, PATIENT.Country, PATIENT.Region, PATIENT.Nationality, PATIENT.Sex FROM PATIENT
INNER JOIN HOSPITAL ON PATIENT.Hospital = Hospital.Id AND Hospital.Id = @Id;

GetPatientsFromHospital @Id = 2;

CREATE PROCEDURE GetContactsFromPatient @Ssn VARCHAR (15)
AS
SELECT Person.Ssn, Person.FirstName, Person.LastName, Person.BirthDate, Person.EMail, Person.Address, Person.Sex, CONTACT.Date DateOfContact FROM PERSON
INNER JOIN CONTACT ON PERSON.Ssn = CONTACT.Person AND CONTACT.Patient = @Ssn;

GetContactsFromPatient @Ssn = '111111111';

CREATE PROCEDURE DeleteContact @Ssn Varchar(15)
AS
DELETE CONTACT
WHERE CONTACT.Person = @Ssn
DELETE PERSON
WHERE PERSON.Ssn = @Ssn;

DeleteContact @Ssn = '111111111';

CREATE PROCEDURE InsertContact @PersonSsn VARCHAR (15), @FirstName VARCHAR (15), @LastName VARCHAR (15), @BirthDate DATE, @EMail VARCHAR (25), @Address TEXT, @Sex CHAR (1), @ContactDate DATE, @PatientSsn VARCHAR (15)
AS
INSERT INTO PERSON (Ssn, FirstName, LastName, BirthDate, EMail, Address, Sex)
VALUES (@PersonSsn, @FirstName, @LastName, @BirthDate, @EMail, @Address, @Sex)
INSERT INTO CONTACT (Person, Patient, Date)
VALUES (@PersonSsn, @PatientSsn, @ContactDate);

InsertContact @PersonSsn = '111111111', @FirstName = 'Leo', @LastName = 'Trujo', @BirthDate = '1950-12-12', @EMail = 'leo@gmail.com', @Address = 'Nose xd', @Sex = 'M', @ContactDate = '2020-06-30', @PatientSsn = '123456798';

CREATE PROCEDURE UpdateContact @PersonSsn VARCHAR (15), @FirstName VARCHAR (15), @LastName VARCHAR (15), @BirthDate DATE, @EMail VARCHAR (25), @Address TEXT, @Sex CHAR (1), @ContactDate DATE, @PatientSsn VARCHAR (15)
AS
UPDATE PERSON
SET PERSON.FirstName = @FirstName, PERSON.LastName = @LastName, PERSON.BirthDate = @BirthDate, PERSON.EMail = @EMail, PERSON.Address = @Address, PERSON.Sex = @Sex
WHERE PERSON.Ssn = @PersonSsn
UPDATE CONTACT
SET CONTACT.Date = @ContactDate
WHERE CONTACT.Person = @PersonSsn AND CONTACT.Patient = @PatientSsn;

UpdateContact @PersonSsn = '111111111', @FirstName = 'Leo', @LastName = 'Trujo', @BirthDate = '1950-12-12', @EMail = 'leo@gmail.com', @Address = 'Nose xd', @Sex = 'M', @ContactDate = '2020-06-30', @PatientSsn = '123456798';

CREATE PROCEDURE GetStatesFromPatient @Ssn VARCHAR (15)
AS
SELECT STATE.Name, PATIENT_STATE.Date FROM STATE
INNER JOIN PATIENT_STATE ON PATIENT_STATE.State = State.Id AND PATIENT_STATE.Patient = @Ssn;

GetStatesFromPatient @Ssn = '333333333';

CREATE PROCEDURE DeleteStateFromPatient @Ssn Varchar(15), @StateName VARCHAR (15)
AS
DECLARE @StateId AS INT
SELECT @StateId = Id FROM STATE
WHERE @StateName = STATE.Name
DELETE PATIENT_STATE
WHERE PATIENT_STATE.State = @StateId AND PATIENT_STATE.Patient = @Ssn;

DeleteStateFromPatient @Ssn = '333333333', @StateName = 'Dead';

CREATE PROCEDURE InsertStateForPatient @Ssn VARCHAR (15), @Date DATE, @StateName VARCHAR (15)
AS
DECLARE @StateId AS INT
SELECT @StateId = STATE.Id FROM STATE
WHERE @StateName = STATE.Name
INSERT INTO PATIENT_STATE (State, Patient, Date)
VALUES (@StateId, @Ssn, @Date);

InsertStateForPatient @Ssn = '333333333', @Date = '1999-12-12', @StateName = 'Recovered';

CREATE PROCEDURE UpdateStateForPatient @Ssn VARCHAR (15), @Date DATE, @StateName VARCHAR (15), @PrevDate DATE, @PrevState VARCHAR (15)
AS
DECLARE @PostStateId AS INT
DECLARE @PrevStateId AS INT
SELECT @PostStateId = STATE.Id FROM STATE
WHERE @StateName = STATE.Name
SELECT @PrevStateId = STATE.Id FROM STATE
WHERE @PrevState = STATE.Name
UPDATE PATIENT_STATE
SET PATIENT_STATE.State = @PostStateId, PATIENT_STATE.Date = @Date
WHERE PATIENT_STATE.Patient = @Ssn AND PATIENT_STATE.State = @PrevStateId AND PATIENT_STATE.Date = @PrevDate;

UpdateStateForPatient @Ssn = '333333333', @Date = '1999-12-12', @StateName = 'Dead', @PrevDate = 'x', @PrevState = 'x';

CREATE PROCEDURE GetMedicationsFromPatient @Ssn VARCHAR (15)
AS
SELECT MEDICATION.Name, MEDICATION.Pharmacy FROM MEDICATION
INNER JOIN PATIENT_MEDICATION ON Medication.Id = PATIENT_MEDICATION.Medication AND PATIENT_MEDICATION.Patient = @Ssn;

GetMedicationsFromPatient @Ssn = '333333333';

CREATE PROCEDURE DeleteMedicationsFromPatient @Ssn Varchar(15), @MedicationName VARCHAR (15)
AS
DECLARE @MedicationId AS INT
SELECT @MedicationId = MEDICATION.Id FROM MEDICATION
WHERE @MedicationName = MEDICATION.Name
DELETE PATIENT_MEDICATION
WHERE PATIENT_MEDICATION.Medication = @MedicationId AND PATIENT_MEDICATION.Patient = @Ssn;

DeleteMedicationsFromPatient @Ssn = '333333333', @MedicationName = 'Dead';

CREATE PROCEDURE InsertMedicationsForPatient @Ssn VARCHAR (15), @MedicationName VARCHAR (15)
AS
DECLARE @MedicationId AS INT
SELECT @MedicationId = MEDICATION.Id FROM MEDICATION
WHERE @MedicationName = MEDICATION.Name
INSERT INTO PATIENT_MEDICATION (Patient, Medication)
VALUES (@Ssn, @MedicationId);

InsertMedicationsForPatient @Ssn = '333333333', @MedicationName = 'Paracetamol';

CREATE PROCEDURE UpdateMedicationsForPatient @Ssn VARCHAR (15), @MedicationName VARCHAR (15), @PrevName VARCHAR (15)
AS
DECLARE @PostMedicationId AS INT
DECLARE @PrevMedicationId AS INT
SELECT @PostMedicationId = MEDICATION.Id FROM MEDICATION
WHERE @MedicationName = MEDICATION.Name
SELECT @PrevMedicationId = MEDICATION.Id FROM MEDICATION
WHERE @PrevName = MEDICATION.Name
UPDATE PATIENT_MEDICATION
SET PATIENT_MEDICATION.Medication = @PostMedicationId
WHERE PATIENT_MEDICATION.Patient = @Ssn AND PATIENT_MEDICATION.Medication = @PrevMedicationId;

UpdateMedicationsForPatient @Ssn = '333333333', @MedicationName = 'x', @PrevName = 'x';

CREATE PROCEDURE GetPathologiesFromPatient @Ssn VARCHAR (15)
AS
SELECT PATHOLOGY.Name, PATHOLOGY.Symptoms, PATHOLOGY.Description, PATHOLOGY.Treatment FROM PATHOLOGY
INNER JOIN PATIENT_PATHOLOGIES ON Pathology.Id = PATIENT_PATHOLOGIES.Pathology AND PATIENT_PATHOLOGIES.Patient = @Ssn;

GetPathologiesFromPatient @Ssn = '333333333';

CREATE PROCEDURE DeletePathologiesFromPatient @Ssn Varchar(15), @PathologyName VARCHAR (15)
AS
DECLARE @PathologyId AS INT
SELECT @PathologyId = PATHOLOGY.Id FROM PATHOLOGY
WHERE @PathologyName = PATHOLOGY.Name
DELETE PATIENT_PATHOLOGIES
WHERE PATIENT_PATHOLOGIES.Pathology = @PathologyId AND PATIENT_PATHOLOGIES.Patient = @Ssn;

DeletePathologiesFromPatient @Ssn = '333333333', @PathologyName = 'x';

CREATE PROCEDURE InsertPathologiesForPatient @Ssn VARCHAR (15), @PathologyName VARCHAR (15)
AS
DECLARE @PathologyId AS INT
SELECT @PathologyId = PATHOLOGY.Id FROM PATHOLOGY
WHERE @PathologyName = PATHOLOGY.Name
INSERT INTO PATIENT_PATHOLOGIES (Patient, Pathology)
VALUES (@Ssn, @PathologyId);

InsertPathologiesForPatient @Ssn = 'x', @PathologyName = 'x';

CREATE PROCEDURE UpdatePathologiesForPatient @Ssn VARCHAR (15), @PathologyName VARCHAR (15), @PrevName VARCHAR (15)
AS
DECLARE @PostPathologyId AS INT
DECLARE @PrevPathologyId AS INT
SELECT @PostPathologyId = PATHOLOGY.Id FROM PATHOLOGY
WHERE @PathologyName = PATHOLOGY.Name
SELECT @PrevPathologyId = PATHOLOGY.Id FROM PATHOLOGY
WHERE @PrevName = PATHOLOGY.Name
UPDATE PATIENT_PATHOLOGIES
SET PATIENT_PATHOLOGIES.Pathology = @PostPathologyId
WHERE PATIENT_PATHOLOGIES.Patient = @Ssn AND PATIENT_PATHOLOGIES.Pathology = @PrevPathologyId;

UpdatePathologiesForPatient @Ssn = '333333333', @PathologyName = 'x', @PrevName = 'x';