-- Function to get patients count by state
CREATE FUNCTION udfPatientByState (@State varchar(15))
RETURNS TABLE
    AS
    RETURN
    (
        SELECT C.Name           AS Country,
               COUNT(S.Name)    AS State
        FROM PATIENT AS P
            INNER JOIN PATIENT_STATE PS ON PS.Patient = P.Ssn
            INNER JOIN STATE S ON PS.State = S.Id
            INNER JOIN COUNTRY C ON P.Country = C.Name
        WHERE S.Name = @State OR @State IS NULL
        GROUP BY C.Name
    )
GO

-- Function to get patients cases in the last date
CREATE FUNCTION udfPatientByDate (@State varchar(15), @Date date)
RETURNS TABLE
    AS
    RETURN
    (
        SELECT C.Name           AS Country,
               COUNT(S.Name)    AS State,
               PS.Date          AS Date
        FROM PATIENT AS P
            INNER JOIN PATIENT_STATE PS ON PS.Patient = P.Ssn
            INNER JOIN STATE S ON PS.State = S.Id
            INNER JOIN COUNTRY C ON P.Country = C.Name
        WHERE (PS.Date >= @Date OR @Date IS NULL) AND
              (S.Name = @State OR @State IS NULL)
        GROUP BY PS.Date, C.Name
    )
GO

-- Patients states by country
CREATE VIEW [PATIENT STATE BY COUNTRY] AS
    SELECT C.Country    AS Country,
           C.State      AS Confirmed,
           A.State      AS Active,
           D.State      AS Dead,
           R.State      AS Recovered
    FROM udfPatientByState(NULL) AS C
        FULL OUTER JOIN  udfPatientByState('active') A ON C.Country = A.Country
        FULL OUTER JOIN  udfPatientByState('dead') D ON C.Country = D.Country
        FULL OUTER JOIN  udfPatientByState('recovered') R ON C.Country = R.Country
GO

-- Cases and deaths per day and by country
CREATE VIEW [CASES AND DEATHS BY COUNTRY] AS
    SELECT C.Country    AS Country,
           A.State      AS Active,
           D.State      AS Dead,
           C.Date       AS Date
    FROM udfPatientByDate (NULL,DATEADD(day, -6, GETDATE())) AS C
        FULL OUTER JOIN  udfPatientByDate ('active',DATEADD(day, -6, GETDATE())) A ON C.Country = A.Country
        FULL OUTER JOIN  udfPatientByDate ('dead',DATEADD(day, -6, GETDATE())) D ON C.Country = D.Country
GO

-- Total world cases
CREATE VIEW [WORLD ACCUMULATED] AS
    SELECT SUM(C.State) AS Confirmed,
           SUM(A.State) AS Active,
           SUM(D.State) AS Dead,
           SUM(R.State) AS Recovered
    FROM udfPatientByState(NULL) AS C
        FULL OUTER JOIN  udfPatientByState('active') A ON C.Country = A.Country
        FULL OUTER JOIN  udfPatientByState('dead') D ON C.Country = D.Country
        FULL OUTER JOIN  udfPatientByState('recovered') R ON C.Country = R.Country
GO