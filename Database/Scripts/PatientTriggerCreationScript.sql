CREATE TABLE PATIENT_CHANGES (
    PatientSsn VARCHAR (15) NOT NULL ,
    FirstName VARCHAR (15) NOT NULL,
    LastName VARCHAR (15) NOT NULL,
    BirthDate DATE NOT NULL,
    Hospital INT NOT NULL,
    Date DATE NOT NULL,
    Operation VARCHAR (3) NOT NULL,
    Sex VARCHAR (6) NOT NULL,
);

ALTER TABLE PATIENT_CHANGES
ADD CONSTRAINT PATIENT_CHANGES_PATIENT_FK FOREIGN KEY (PatientSsn)
REFERENCES PATIENT (Ssn);

ALTER TABLE PATIENT_CHANGES
ADD CONSTRAINT PATIENT_CHANGES_HOSPITAL_FK FOREIGN KEY (Hospital)
REFERENCES HOSPITAL (Id);

CREATE TRIGGER PATIENT_INSERTION_TRIGGER
ON PATIENT
AFTER INSERT, DELETE, UPDATE
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
    UNION ALL
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
    UNION ALL
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