-- Patients states by country
CREATE VIEW [PATIENT STATE BY COUNTRY] AS
    SELECT C.Name                                   AS Country,
           COUNT(P.Ssn)                             AS Confirmed,
           COUNT(IIF(S.Name = 'active', 1, 0))      AS Active,
           COUNT(IIF(S.Name = 'dead', 1, 0))        AS Dead,
           COUNT(IIF(S.Name = 'recovered', 1, 0))   AS Recovered
    FROM PATIENT AS P
    INNER JOIN PATIENT_STATE PS ON PS.Patient = P.Ssn
    INNER JOIN STATE S          ON PS.State = S.Id
    INNER JOIN COUNTRY C        ON P.Country = C.Name
    GROUP BY C.Name
GO

-- Cases and deaths per day and by country
CREATE VIEW [CASES AND DEATHS BY COUNTRY] AS
    SELECT C.Name                               AS Country,
           COUNT(IIF(S.Name = 'active', 1, 0))  AS Active,
           COUNT(IIF(S.Name = 'dead', 1, 0))    AS Dead,
           PS.Date                              AS Date
    FROM PATIENT AS P
    INNER JOIN PATIENT_STATE PS ON PS.Patient = P.Ssn
    INNER JOIN STATE S          ON PS.State = S.Id
    INNER JOIN COUNTRY C        ON P.Country = C.Name
    WHERE PS.Date >= DATEADD(day, -6, GETDATE())
    GROUP BY PS.Date, C.Name
GO

CREATE VIEW [WORLD ACCUMULATED] AS
    SELECT COUNT(P.Ssn)                             AS Confirmed,
           COUNT(IIF(S.Name = 'active', 1, 0))      AS Active,
           COUNT(IIF(S.Name = 'dead', 1, 0))        AS Dead,
           COUNT(IIF(S.Name = 'recovered', 1, 0))   AS Recovered
    FROM PATIENT AS P
    INNER JOIN PATIENT_STATE PS ON PS.Patient = P.Ssn
    INNER JOIN STATE S          ON PS.State = S.Id