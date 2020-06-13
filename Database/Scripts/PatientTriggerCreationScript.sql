CREATE TABLE PATIENT_CHANGES (
    PatientSsn INT NOT NULL ,
    FirstName VARCHAR (15) NOT NULL,
    LastName VARCHAR (15) NOT NULL,
    Age INT NOT NULL,
    Hospital INT NOT NULL,
    Date DATE NOT NULL,
    Operation VARCHAR (3) NOT NULL
);

ALTER TABLE PATIENT_CHANGES
ADD CONSTRAINT MEDICATION_CHANGES_PATIENT_FK FOREIGN KEY (PatientSsn)
REFERENCES PATIENT (Ssn);

ALTER TABLE PATIENT_CHANGES
ADD CONSTRAINT MEDICATION_CHANGES_HOSPITAL_FK FOREIGN KEY (Hospital)
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
        Age,
        Hospital,
        Date,
        Operation
    )
    SELECT
        i.Ssn,
        i.FirstName,
        i.LastName,
        i.Age,
        i.Hospital,
        GETDATE(),
        'INS'
    FROM
        inserted i
    UNION ALL
    SELECT
        d.Ssn,
        d.FirstName,
        d.LastName,
        d.Age,
        d.Hospital,
        GETDATE(),
        'DEL'
    FROM
        deleted d
    UNION ALL
    SELECT
        i.Ssn,
        i.FirstName,
        i.LastName,
        i.Age,
        i.Hospital,
        GETDATE(),
        'UPD'
    FROM
        inserted i
END;