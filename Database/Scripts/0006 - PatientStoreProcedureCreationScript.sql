CREATE PROCEDURE GetPatientsFromHospital @Id INT
AS
SELECT PATIENT.Ssn, PATIENT.FirstName, PATIENT.LastName, PATIENT.BirthDate, PATIENT.Hospitalized, PATIENT.ICU, PATIENT.Country, PATIENT.Region, PATIENT.Nationality, PATIENT.Sex FROM PATIENT
INNER JOIN HOSPITAL ON PATIENT.Hospital = Hospital.Id AND Hospital.Id = @Id;
GO

CREATE PROCEDURE GetContactsFromPatient @Ssn VARCHAR (15)
AS
SELECT Person.Ssn, Person.FirstName, Person.LastName, Person.BirthDate, Person.EMail, Person.Address, Person.Sex, CONTACT.Date DateOfContact FROM PERSON
INNER JOIN CONTACT ON PERSON.Ssn = CONTACT.Person AND CONTACT.Patient = @Ssn;
GO

CREATE PROCEDURE DeleteContact @PersonSsn VARCHAR(15), @PatientSsn VARCHAR (15)
AS
DELETE CONTACT
WHERE CONTACT.Person = @PersonSsn AND CONTACT.Patient = @PatientSsn
IF NOT EXISTS(SELECT * FROM CONTACT
              WHERE CONTACT.Person = @PersonSsn)
BEGIN
DELETE PERSON
WHERE PERSON.Ssn = @PersonSsn;
END;
GO

CREATE PROCEDURE InsertContact @PersonSsn VARCHAR (15), @FirstName VARCHAR (15), @LastName VARCHAR (15), @BirthDate DATE, @EMail VARCHAR (25), @Address TEXT, @Sex CHAR (1), @ContactDate DATE, @PatientSsn VARCHAR (15)
AS
IF NOT EXISTS(SELECT * FROM PERSON
          WHERE PERSON.Ssn = @PersonSsn)
BEGIN
INSERT INTO PERSON (Ssn, FirstName, LastName, BirthDate, EMail, Address, Sex)
VALUES (@PersonSsn, @FirstName, @LastName, @BirthDate, @EMail, @Address, @Sex)
END
INSERT INTO CONTACT (Person, Patient, Date)
VALUES (@PersonSsn, @PatientSsn, @ContactDate);
GO

CREATE PROCEDURE UpdateContact @PersonSsn VARCHAR (15), @FirstName VARCHAR (15), @LastName VARCHAR (15), @BirthDate DATE, @EMail VARCHAR (25), @Address TEXT, @Sex CHAR (1), @ContactDate DATE, @PatientSsn VARCHAR (15)
AS
UPDATE PERSON
SET PERSON.FirstName = @FirstName, PERSON.LastName = @LastName, PERSON.BirthDate = @BirthDate, PERSON.EMail = @EMail, PERSON.Address = @Address, PERSON.Sex = @Sex
WHERE PERSON.Ssn = @PersonSsn
UPDATE CONTACT
SET CONTACT.Date = @ContactDate
WHERE CONTACT.Person = @PersonSsn AND CONTACT.Patient = @PatientSsn;
GO

CREATE PROCEDURE GetStatesFromPatient @Ssn VARCHAR (15)
AS
SELECT STATE.Name, PATIENT_STATE.Date FROM STATE
INNER JOIN PATIENT_STATE ON PATIENT_STATE.State = State.Id AND PATIENT_STATE.Patient = @Ssn;
GO

CREATE PROCEDURE DeleteStateFromPatient @Ssn Varchar(15), @StateName VARCHAR (15)
AS
DECLARE @StateId AS INT
SELECT @StateId = Id FROM STATE
WHERE @StateName = STATE.Name
DELETE PATIENT_STATE
WHERE PATIENT_STATE.State = @StateId AND PATIENT_STATE.Patient = @Ssn;
GO

CREATE PROCEDURE InsertStateForPatient @Ssn VARCHAR (15), @Date DATE, @StateName VARCHAR (15)
AS
DECLARE @StateId AS INT
SELECT @StateId = STATE.Id FROM STATE
WHERE @StateName = STATE.Name
INSERT INTO PATIENT_STATE (State, Patient, Date)
VALUES (@StateId, @Ssn, @Date);
GO

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
GO

CREATE PROCEDURE GetMedicationsFromPatient @Ssn VARCHAR (15)
AS
SELECT MEDICATION.Name, MEDICATION.Pharmacy FROM MEDICATION
INNER JOIN PATIENT_MEDICATION ON Medication.Id = PATIENT_MEDICATION.Medication AND PATIENT_MEDICATION.Patient = @Ssn;
GO

CREATE PROCEDURE DeleteMedicationsFromPatient @Ssn Varchar(15), @MedicationName VARCHAR (15)
AS
DECLARE @MedicationId AS INT
SELECT @MedicationId = MEDICATION.Id FROM MEDICATION
WHERE @MedicationName = MEDICATION.Name
DELETE PATIENT_MEDICATION
WHERE PATIENT_MEDICATION.Medication = @MedicationId AND PATIENT_MEDICATION.Patient = @Ssn;
GO

CREATE PROCEDURE InsertMedicationsForPatient @Ssn VARCHAR (15), @MedicationName VARCHAR (15)
AS
DECLARE @MedicationId AS INT
SELECT @MedicationId = MEDICATION.Id FROM MEDICATION
WHERE @MedicationName = MEDICATION.Name
INSERT INTO PATIENT_MEDICATION (Patient, Medication)
VALUES (@Ssn, @MedicationId);
GO

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
GO

CREATE PROCEDURE GetPathologiesFromPatient @Ssn VARCHAR (15)
AS
SELECT PATHOLOGY.Name, PATHOLOGY.Symptoms, PATHOLOGY.Description, PATHOLOGY.Treatment FROM PATHOLOGY
INNER JOIN PATIENT_PATHOLOGIES ON Pathology.Id = PATIENT_PATHOLOGIES.Pathology AND PATIENT_PATHOLOGIES.Patient = @Ssn;
GO

CREATE PROCEDURE DeletePathologiesFromPatient @Ssn Varchar(15), @PathologyName VARCHAR (15)
AS
DECLARE @PathologyId AS INT
SELECT @PathologyId = PATHOLOGY.Id FROM PATHOLOGY
WHERE @PathologyName = PATHOLOGY.Name
DELETE PATIENT_PATHOLOGIES
WHERE PATIENT_PATHOLOGIES.Pathology = @PathologyId AND PATIENT_PATHOLOGIES.Patient = @Ssn;
GO

CREATE PROCEDURE InsertPathologiesForPatient @Ssn VARCHAR (15), @PathologyName VARCHAR (15)
AS
DECLARE @PathologyId AS INT
SELECT @PathologyId = PATHOLOGY.Id FROM PATHOLOGY
WHERE @PathologyName = PATHOLOGY.Name
INSERT INTO PATIENT_PATHOLOGIES (Patient, Pathology)
VALUES (@Ssn, @PathologyId);
GO

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
GO