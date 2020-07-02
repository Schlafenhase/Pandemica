CREATE TABLE PATIENT_CHANGES (
    PatientSsn VARCHAR (15) NOT NULL ,
    FirstName VARCHAR (15) NOT NULL,
    LastName VARCHAR (15) NOT NULL,
    BirthDate DATE NOT NULL,
    Hospital INT NOT NULL,
    Date DATE NOT NULL,
    Operation CHAR (3) NOT NULL,
    Sex CHAR (1) NOT NULL,
    Id INT IDENTITY (1,1) NOT NULL,
    PRIMARY KEY (Id),
    UNIQUE (Id)
);
GO

CREATE TRIGGER PATIENT_INSERT_TRIGGER
ON PATIENT
AFTER INSERT
AS
BEGIN
    INSERT INTO PATIENT_CHANGES(
        PatientSsn,
        FirstName,
        LastName,
        BirthDate,
        Hospital,
        Date,
        Operation,
        Sex
    )
    SELECT
        i.Ssn,
        i.FirstName,
        i.LastName,
        i.BirthDate,
        i.Hospital,
        GETDATE(),
        'INS',
        i.Sex
    FROM
        inserted i
END;
GO

CREATE TRIGGER PATIENT_UPDATE_TRIGGER
ON PATIENT
AFTER UPDATE
AS
BEGIN
    INSERT INTO PATIENT_CHANGES(
        PatientSsn,
        FirstName,
        LastName,
        BirthDate,
        Hospital,
        Date,
        Operation,
        Sex
    )
    SELECT
        i.Ssn,
        i.FirstName,
        i.LastName,
        i.BirthDate,
        i.Hospital,
        GETDATE(),
        'UPD',
        i.Sex
    FROM
        inserted i
END;
GO

CREATE TRIGGER PATIENT_DELETE_TRIGGER
ON PATIENT
AFTER DELETE
AS
BEGIN
    INSERT INTO PATIENT_CHANGES(
        PatientSsn,
        FirstName,
        LastName,
        BirthDate,
        Hospital,
        Date,
        Operation,
        Sex
    )
    SELECT
        d.Ssn,
        d.FirstName,
        d.LastName,
        d.BirthDate,
        d.Hospital,
        GETDATE(),
        'DEL',
        d.Sex
    FROM
        deleted d
END;
GO