
INSERT INTO PATIENT (Ssn, FirstName, LastName, BirthDate, Hospitalized, ICU, Country, Region, Nationality, Hospital, Sex)
VALUES (117270690,'Jose','Sánchez','2000-06-24',1,1,'Costa Rica','Alajuela','Costa Rica',1,'M')

INSERT INTO PATIENT (Ssn, FirstName, LastName, BirthDate, Hospitalized, ICU, Country, Region, Nationality, Hospital, Sex)
VALUES (117270691,'Eithel','Sánchez','1980-06-24',1,0,'Costa Rica','Alajuela','Costa Rica',2,'M')

INSERT INTO PATIENT (Ssn, FirstName, LastName, BirthDate, Hospitalized, ICU, Country, Region, Nationality, Hospital, Sex)
VALUES (117270692,'Kevin','Cordero','1984-06-24',0,0,'Costa Rica','San José','Nicaragua',2,'M')

INSERT INTO PATIENT (Ssn, FirstName, LastName, BirthDate, Hospitalized, ICU, Country, Region, Nationality, Hospital, Sex)
VALUES (117270693,'Ale','Ibarra','1999-06-24',0,0,'Costa Rica','San José','Costa Rica',1,'M')

INSERT INTO PATIENT (Ssn, FirstName, LastName, BirthDate, Hospitalized, ICU, Country, Region, Nationality, Hospital, Sex)
VALUES (117270694,'José Daniel','Acuña','2015-06-24',1,1,'Costa Rica','Heredia','Costa Rica',1,'M')

INSERT INTO PATIENT (Ssn, FirstName, LastName, BirthDate, Hospitalized, ICU, Country, Region, Nationality, Hospital, Sex)
VALUES (117270695,'Miguel','Sánchez','2006-06-24',1,1,'Nicaragua','Alajuela','Costa Rica',1,'M')

INSERT INTO PATIENT (Ssn, FirstName, LastName, BirthDate, Hospitalized, ICU, Country, Region, Nationality, Hospital, Sex)
VALUES (117270696,'Jesus','Sandoval','1960-06-24',1,0,'Nicaragua','Alajuela','Costa Rica',2,'M')

INSERT INTO PATIENT (Ssn, FirstName, LastName, BirthDate, Hospitalized, ICU, Country, Region, Nationality, Hospital, Sex)
VALUES (117270697,'Jess','Espinoza','1998-06-24',0,0,'Nicaragua','San José','Nicaragua',2,'F')

INSERT INTO PATIENT (Ssn, FirstName, LastName, BirthDate, Hospitalized, ICU, Country, Region, Nationality, Hospital, Sex)
VALUES (117270698,'Don','Omar','1977-06-24',0,0,'Costa Rica','San José','Nicaragua',1,'M')

INSERT INTO PATIENT (Ssn, FirstName, LastName, BirthDate, Hospitalized, ICU, Country, Region, Nationality, Hospital, Sex)
VALUES (117270699,'El MP','R','1956-06-24',1,1,'Costa Rica','Heredia','Nicaragua',1,'M')

INSERT INTO STATE (Name) VALUES ('recovered')
INSERT INTO STATE (Name) VALUES ('active')
INSERT INTO STATE (Name) VALUES ('dead')

INSERT INTO PATIENT_STATE (State, Patient, Date) VALUES (2,117270690,'2020-06-25')
INSERT INTO PATIENT_STATE (State, Patient, Date) VALUES (2,117270691,'2020-06-27')
INSERT INTO PATIENT_STATE (State, Patient, Date) VALUES (1,117270692,'2020-06-26')
INSERT INTO PATIENT_STATE (State, Patient, Date) VALUES (1,117270693,'2020-06-26')
INSERT INTO PATIENT_STATE (State, Patient, Date) VALUES (3,117270694,'2020-06-24')
INSERT INTO PATIENT_STATE (State, Patient, Date) VALUES (2,117270695,'2020-06-27')
INSERT INTO PATIENT_STATE (State, Patient, Date) VALUES (2,117270696,'2020-06-26')
INSERT INTO PATIENT_STATE (State, Patient, Date) VALUES (1,117270697,'2020-06-24')
INSERT INTO PATIENT_STATE (State, Patient, Date) VALUES (1,117270698,'2020-06-25')
INSERT INTO PATIENT_STATE (State, Patient, Date) VALUES (2,117270699,'2020-06-25')

insert into PATHOLOGY (Name, Symptoms, Description, Treatment) values ('tos','dolor de cabeza','mega F','se muere')
insert into PATHOLOGY (Name, Symptoms, Description, Treatment) values ('estornudos','dolor de cuerpo','no mega F','puede vivir')

insert into PATIENT_PATHOLOGIES (Patient, Pathology) values (117270690,1);
insert into PATIENT_PATHOLOGIES (Patient, Pathology) values (117270691,1);
insert into PATIENT_PATHOLOGIES (Patient, Pathology) values (117270692,1);
insert into PATIENT_PATHOLOGIES (Patient, Pathology) values (117270693,2);
insert into PATIENT_PATHOLOGIES (Patient, Pathology) values (117270694,2);
insert into PATIENT_PATHOLOGIES (Patient, Pathology) values (117270695,1);
insert into PATIENT_PATHOLOGIES (Patient, Pathology) values (117270696,1);
insert into PATIENT_PATHOLOGIES (Patient, Pathology) values (117270697,1);
insert into PATIENT_PATHOLOGIES (Patient, Pathology) values (117270698,2);
insert into PATIENT_PATHOLOGIES (Patient, Pathology) values (117270699,2);
