--Function in charge of creating a trigger for the patient table

CREATE TRIGGER CAPACITY_CHECK_TRIGGER
ON PATIENT
AFTER INSERT, UPDATE
AS
BEGIN
IF (SELECT i.Hospitalized FROM inserted AS i) = 1
    BEGIN
    DECLARE @HospitalCapacity AS INT
    DECLARE @CurrentPatients AS INT
    DECLARE @HospitalICUCapacity AS INT
    DECLARE @CurrentICUPatients AS INT

    SELECT @HospitalCapacity = HOSPITAL.Capacity, @HospitalICUCapacity = HOSPITAL.ICUCapacity FROM inserted AS i
    INNER JOIN HOSPITAL ON i.Hospital = HOSPITAL.Id

    SELECT @CurrentPatients = COUNT(PATIENT.Hospitalized), @CurrentICUPatients = COUNT(PATIENT.ICU) FROM inserted AS i
    INNER JOIN HOSPITAL ON i.Hospital = HOSPITAL.Id
    INNER JOIN PATIENT ON HOSPITAL.Id = PATIENT.Hospital

    IF (@HospitalCapacity <= @CurrentPatients - 1) OR (@HospitalICUCapacity <= @CurrentICUPatients - 1)
        BEGIN
        ROLLBACK TRANSACTION
        RETURN
        END
    END
ELSE
    BEGIN
        IF (SELECT i.ICU FROM inserted AS i) = 1
            BEGIN
            ROLLBACK TRANSACTION
            RETURN
            END
    END
END;
GO
