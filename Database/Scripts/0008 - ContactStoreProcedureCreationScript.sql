--Script in charge of creating store procedure related to the contact table

CREATE PROCEDURE DeleteContactsOlderThan14Days
AS
DECLARE @PersonSsn VARCHAR (15)
SELECT @PersonSsn = CONTACT.Person FROM CONTACT
WHERE CONTACT.Date < GETDATE() - 14
DELETE FROM CONTACT
WHERE CONTACT.Person = @PersonSsn
IF NOT EXISTS(SELECT * FROM CONTACT
              WHERE CONTACT.Person = @PersonSsn)
BEGIN
DELETE PERSON
WHERE PERSON.Ssn = @PersonSsn;
END;
GO

CREATE PROCEDURE CheckContacts
AS
BEGIN
WHILE (1 = 1)
  BEGIN
    WAITFOR DELAY '12:00:00'
    EXEC DeleteContactsOlderThan14Days
  END
END;
GO

