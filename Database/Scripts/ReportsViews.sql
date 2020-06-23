CREATE VIEW [PATIENTS STATE BY COUNTRY] AS
    SELECT C.Name AS Country,
           (SELECT COUNT(PATIENT.State)
                FROM PATIENT
                HAVING COUNT(PATIENT.State)='confirmed') AS Confirmed,
           (SELECT COUNT(PATIENT.State)
                FROM PATIENT
                HAVING COUNT(PATIENT.State)='active') AS Active,
           (SELECT COUNT(PATIENT.State)
                FROM PATIENT
                HAVING COUNT(PATIENT.State)='dead') AS Dead,
           (SELECT COUNT(PATIENT.State)
                FROM PATIENT
                HAVING COUNT(PATIENT.State)='recovered') AS Recovered
    FROM COUNTRY AS C, PATIENT AS P, PATIENT_STATE AS PS, STATE AS S
    JOIN PATIENT ON S.Name = PATIENT.State
    GROUP BY C.Name