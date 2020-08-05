CREATE TABLE Hospital (
  Id Int UNIQUE NOT NULL,
  PRIMARY KEY (Id)
);

CREATE TABLE Procedure (
  Id SERIAL UNIQUE NOT NULL,
  Name Varchar (15) NOT NULL,
  Duration Int NOT NULL,
  PRIMARY KEY (Id)
);

CREATE TABLE Equipment (
  Id SERIAL UNIQUE NOT NULL,
  Name Varchar (15) NOT NULL,
  Provider Varchar (15) NOT NULL,
  PRIMARY KEY (Id)
);

CREATE TABLE Patient (
  Ssn Varchar (15) UNIQUE NOT NULL,
  EMail Varchar (15) UNIQUE,
  PRIMARY KEY (Ssn)
);

CREATE TABLE Health_Worker (
  Ssn Varchar (15) UNIQUE NOT NULL,
  FName Varchar (15) NOT NULL,
  LName Varchar (15) NOT NULL,
  Phone Varchar (15) NOT NULL,
  BirthDate Date NOT NULL,
  Role Varchar (15) NOT NULL,
  Hospital_ID Int NOT NULL,
  Sex Char (1) NOT NULL,
  EMail Varchar (15) UNIQUE NOT NULL,
  PRIMARY KEY (Ssn, Hospital_ID),
  FOREIGN KEY (Hospital_ID) REFERENCES Hospital (Id)
);

CREATE TABLE Lounge (
  Number Int UNIQUE NOT NULL,
  Floor Int NOT NULL,
  Name Varchar (15) NOT NULL,
  Type Varchar (15) NOT NULL,
  Hospital_ID Int NOT NULL,
  PRIMARY KEY (Number, Hospital_ID),
  FOREIGN KEY (Hospital_ID) REFERENCES Hospital (Id)
);

CREATE TABLE Person (
  Ssn Varchar (15) UNIQUE NOT NULL,
  EMail Varchar (15) UNIQUE NOT NULL,
  PRIMARY KEY (Ssn)
);

CREATE TABLE Bed (
  Number SERIAL UNIQUE NOT NULL,
  Icu Bit NOT NULL,
  Lounge_Number Int NOT NULL,
  PRIMARY KEY (Number, Lounge_Number),
  FOREIGN KEY (Lounge_Number) REFERENCES Lounge (Number)
);

CREATE TABLE Hospital_Procedure (
  Procedure_ID Int NOT NULL,
  Hospital_ID Int NOT NULL,
  Id SERIAL UNIQUE NOT NULL,
  PRIMARY KEY (Id, Procedure_ID, Hospital_ID),
  FOREIGN KEY (Procedure_ID) REFERENCES Procedure (Id),
  FOREIGN KEY (Hospital_ID) REFERENCES Hospital (Id)
);

CREATE TABLE Bed_Equipment (
  Bed_Number Int NOT NULL,
  Equipment_ID Int NOT NULL,
  Id SERIAL UNIQUE NOT NULL,
  PRIMARY KEY (Id, Bed_Number, Equipment_ID),
  FOREIGN KEY (Bed_Number) REFERENCES Bed (Number),
  FOREIGN KEY (Equipment_ID) REFERENCES Equipment (Id)
);

CREATE TABLE Reservation (
  Id SERIAL UNIQUE NOT NULL,
  Procedure_ID Int NOT NULL,
  StartDate Date NOT NULL,
  Hospital_ID Int NOT NULL,
  Patient_ID Varchar (15) NOT NULL,
  PRIMARY KEY (Id, Hospital_ID, Patient_ID, Procedure_ID),
  FOREIGN KEY (Hospital_ID) REFERENCES Hospital (Id),
  FOREIGN KEY (Patient_ID) REFERENCES Patient (Ssn),
  FOREIGN KEY (Procedure_ID) REFERENCES Procedure (Id)
);

CREATE TABLE Reservation_Procedures (
  Id SERIAL UNIQUE NOT NULL,
  Procedure_ID Int NOT NULL,
  Reservation_ID Int NOT NULL,
  PRIMARY KEY (Id, Procedure_ID, Reservation_ID),
  FOREIGN KEY (Procedure_ID) REFERENCES Procedure (Id),
  FOREIGN KEY (Reservation_ID) REFERENCES Reservation (Id)
);