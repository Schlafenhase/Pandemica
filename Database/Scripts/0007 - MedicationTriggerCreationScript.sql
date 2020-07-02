CREATE TABLE MEDICATION_CHANGES (
    PatientSsn VARCHAR (15) NOT NULL,
    AuditData TEXT NOT NULL,
    Id INT IDENTITY (1,1) NOT NULL,
    PRIMARY KEY (Id),
    UNIQUE (Id)
);
GO

CREATE TRIGGER MEDICATION_CHANGES_TRIGGER
ON PATIENT_MEDICATION
FOR UPDATE
AS
BEGIN
    DECLARE @Patient VARCHAR (15)
    SELECT @Patient = Patient from inserted

    declare @OldMedication VARCHAR (15)
    SELECT @OldMedication = MEDICATION.Name from MEDICATION
    INNER JOIN deleted ON MEDICATION.Id = deleted.Medication

    declare @NewMedication VARCHAR (15)
    SELECT @NewMedication = MEDICATION.Name from MEDICATION
    INNER JOIN inserted ON MEDICATION.Id = inserted.Medication

    INSERT INTO MEDICATION_CHANGES(PatientSsn, AuditData)
    VALUES (@Patient,
            CURRENT_USER +
            ' changed medication from ' +
            @OldMedication +
            ' to ' +
            @NewMedication +
            ' for this patient at ' +
            CAST(GETDATE() AS VARCHAR (20)))
END;
GO