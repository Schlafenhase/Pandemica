CREATE PROCEDURE GetCountryMeasures
AS
SELECT ENFORCES.Country, SANITARY_MEASUREMENTS.Name, ENFORCES.StartDate, ENFORCES.FinalDate, ENFORCES.Id FROM ENFORCES
INNER JOIN SANITARY_MEASUREMENTS ON SANITARY_MEASUREMENTS.Id = ENFORCES.Measurement;

GetCountryMeasures;

CREATE PROCEDURE InsertCountryMeasures @Country VARCHAR(30), @Measurement VARCHAR(15), @StartDate DATE, @FinalDate DATE
AS
DECLARE @SMID AS INT
SELECT @SMID = SANITARY_MEASUREMENTS.Id FROM SANITARY_MEASUREMENTS
WHERE @Measurement = SANITARY_MEASUREMENTS.Name
INSERT INTO ENFORCES (Country, Measurement, StartDate, FinalDate)
VALUES (@Country, @SMID, @StartDate, @FinalDate);

InsertCountryMeasures @Country = 'United States', @Measurement = 'Fumigation', @StartDate = '1999-12-12', @FinalDate = '2100-12-12';

CREATE PROCEDURE UpdateCountryMeasures @Country VARCHAR(30), @Measurement VARCHAR(15), @StartDate DATE, @FinalDate DATE, @Id INT
AS
DECLARE @SMID AS INT
SELECT @SMID = SANITARY_MEASUREMENTS.Id FROM SANITARY_MEASUREMENTS
WHERE @Measurement = SANITARY_MEASUREMENTS.Name
UPDATE ENFORCES
SET ENFORCES.Country = @Country, ENFORCES.Measurement = @SMID, ENFORCES.StartDate = @StartDate, ENFORCES.FinalDate = @FinalDate
WHERE ENFORCES.Id = @Id;

UpdateCountryMeasures @Country = 'Angola', @Measurement = 'Fumigation', @StartDate = '2000-12-12', @FinalDate = '2010-12-12', @Id = 3;