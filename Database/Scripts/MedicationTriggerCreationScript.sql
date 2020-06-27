CREATE TABLE MEDICATION_CHANGES (
    Patient VARCHAR (15) NOT NULL,
    AuditData VARCHAR (150) NOT NULL
);

ALTER TABLE MEDICATION_CHANGES
ADD CONSTRAINT MEDICATION_CHANGES_PATIENT_FK FOREIGN KEY (Patient)
REFERENCES PATIENT (Ssn);

CREATE TRIGGER MEDICATION_CHANGES_TRIGGER
ON PATIENT_MEDICATION
FOR UPDATE
AS
BEGIN
    DECLARE @Patient VARCHAR (15)
    SELECT @Patient = Patient from inserted

    declare @OldMedication INT
    SELECT @OldMedication = Medication from deleted

    declare @NewMedication INT
    SELECT @NewMedication = Medication from inserted

    INSERT INTO MEDICATION_CHANGES(Patient, AuditData)
    VALUES (@Patient,
            CURRENT_USER +
            ' changed medication from ' +
            CAST(@OldMedication AS VARCHAR (20)) +
            ' to ' +
            CAST(@NewMedication AS VARCHAR (20))+
            ' for this patient at ' +
            CAST(GETDATE() AS VARCHAR (20)))
END;