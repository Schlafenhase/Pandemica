CREATE TABLE TEMPORARY_DATA(
    requested_data varchar(20),
    resulted_data int,
);

-- General inserts for stored procedure:
insert into TEMPORARY_DATA (requested_data,resulted_data) values  ('confirmedCases',null);
insert into TEMPORARY_DATA (requested_data,resulted_data) values  ('activeCases',null);
insert into TEMPORARY_DATA (requested_data,resulted_data) values  ('deadths',null);
insert into TEMPORARY_DATA (requested_data,resulted_data) values  ('recovered',null);
insert into TEMPORARY_DATA (requested_data,resulted_data) values  ('nationals',null);
insert into TEMPORARY_DATA (requested_data,resulted_data) values  ('foreigns',null);

-- Specific inserts from today for stored procedure:
insert into TEMPORARY_DATA (requested_data,resulted_data) values  ('todayNewCases',null);
insert into TEMPORARY_DATA (requested_data,resulted_data) values  ('todayRecovered',null);
insert into TEMPORARY_DATA (requested_data,resulted_data) values  ('todayDeceased',null);

-- Specific inserts from patients for stored procedure:
insert into TEMPORARY_DATA (requested_data,resulted_data) values  ('patientsAtHome',null);
insert into TEMPORARY_DATA (requested_data,resulted_data) values  ('patientsHospitalized',null);
insert into TEMPORARY_DATA (requested_data,resulted_data) values  ('patientsInICU',null);

-- Specific inserts from cases by sex for stored procedure:
insert into TEMPORARY_DATA (requested_data,resulted_data) values  ('femaleCases',null);
insert into TEMPORARY_DATA (requested_data,resulted_data) values  ('maleCases',null);

-- Specific inserts from cases by age group for stored procedure:
insert into TEMPORARY_DATA (requested_data,resulted_data) values  ('0-12',null);
insert into TEMPORARY_DATA (requested_data,resulted_data) values  ('13-20',null);
insert into TEMPORARY_DATA (requested_data,resulted_data) values  ('21-39',null);
insert into TEMPORARY_DATA (requested_data,resulted_data) values  ('40-59',null);
insert into TEMPORARY_DATA (requested_data,resulted_data) values  ('60-79',null);
insert into TEMPORARY_DATA (requested_data,resulted_data) values  ('+80',null);
GO

create procedure spCasesByCountry
@Country nvarchar(20)
as
Begin
    -- Adds confirmed cases from selected country to TEMPORARY_DATA:
    DECLARE @confirmedCasesVariable int;
    SET @confirmedCasesVariable=(select count(*) resulted_data from PATIENT where Country = @Country)
    update TEMPORARY_DATA
    set resulted_data = @confirmedCasesVariable
    where requested_data = 'confirmedCases'

    -- Adds active cases from selected country to TEMPORARY_DATA:
    DECLARE @activeCasesVariable int;
    SET @activeCasesVariable=(select count(*)
                                from PATIENT_STATE
                                inner join (select Patient, max(Date) date
                                            from PATIENT_STATE
                                            group by Patient) PS
                                on PATIENT_STATE.Patient = PS.Patient and PS.date = PATIENT_STATE.Date and PATIENT_STATE.State = 2
                                inner join PATIENT
                                on PATIENT.Country = @Country and PATIENT_STATE.Patient = PATIENT.Ssn)
    update TEMPORARY_DATA
    set resulted_data = @activeCasesVariable
    where requested_data = 'activeCases'

    -- Adds deceased cases from selected country to TEMPORARY_DATA:
    DECLARE @deadthsVariable int;
    SET @deadthsVariable=(select count(*)
                            from PATIENT_STATE
                            inner join (select Patient, max(Date) date
                                        from PATIENT_STATE
                                        group by Patient) PS
                            on PATIENT_STATE.Patient = PS.Patient and PS.date = PATIENT_STATE.Date and PATIENT_STATE.State = 3
                            inner join PATIENT
                            on PATIENT.Country = @Country and PATIENT_STATE.Patient = PATIENT.Ssn)
    update TEMPORARY_DATA
    set resulted_data = @deadthsVariable
    where requested_data = 'deadths'

    -- Adds recovered cases from selected country to TEMPORARY_DATA:
    DECLARE @recoveredVariable int;
    SET @recoveredVariable=(select count(*)
                            from PATIENT_STATE
                            inner join (select Patient, max(Date) date
                                        from PATIENT_STATE
                                        group by Patient) PS
                            on PATIENT_STATE.Patient = PS.Patient and PS.date = PATIENT_STATE.Date and PATIENT_STATE.State = 1
                            inner join PATIENT
                            on PATIENT.Country = @Country and PATIENT_STATE.Patient = PATIENT.Ssn)
    update TEMPORARY_DATA
    set resulted_data = @recoveredVariable
    where requested_data = 'recovered'

    -- Adds national cases from selected country to TEMPORARY_DATA:
    DECLARE @nationalsVariable int;
    SET @nationalsVariable=(select count(*) resulted_data from PATIENT where Country = @Country and Nationality = @Country)
    update TEMPORARY_DATA
    set resulted_data = @nationalsVariable
    where requested_data = 'nationals'

    -- Adds foreigns cases from selected country to TEMPORARY_DATA:
    DECLARE @foreignsVariable int;
    SET @foreignsVariable=(select count(*) resulted_data from PATIENT where Country = @Country and Nationality != @Country)
    update TEMPORARY_DATA
    set resulted_data = @foreignsVariable
    where requested_data = 'foreigns'

    -- Adds today's new cases from selected country to TEMPORARY_DATA:
    DECLARE @todayNewCasesVariable int;
    SET @todayNewCasesVariable=(select count(*) resulted_data
                                from PATIENT
                                inner join PATIENT_STATE
                                on PATIENT.Country = @Country and PATIENT.Ssn=PATIENT_STATE.Patient and PATIENT_STATE.Date = CONVERT(DATE, GETDATE()) and PATIENT_STATE.State=2)
    update TEMPORARY_DATA
    set resulted_data = @todayNewCasesVariable
    where requested_data = 'todayNewCases'

    -- Adds today's new recovered cases from selected country to TEMPORARY_DATA:
    DECLARE @todayRecoveredVariable int;
    SET @todayRecoveredVariable=(select count(*) resulted_data
                                from PATIENT
                                inner join PATIENT_STATE
                                on PATIENT.Country = @Country and PATIENT.Ssn=PATIENT_STATE.Patient and PATIENT_STATE.Date = CONVERT(DATE, GETDATE()) and PATIENT_STATE.State=1)
    update TEMPORARY_DATA
    set resulted_data = @todayRecoveredVariable
    where requested_data = 'todayRecovered'

    -- Adds today's new deceased cases from selected country to TEMPORARY_DATA:
    DECLARE @todayDeceasedVariable int;
    SET @todayDeceasedVariable=(select count(*) resulted_data
                                from PATIENT
                                inner join PATIENT_STATE
                                on PATIENT.Country = @Country and PATIENT.Ssn=PATIENT_STATE.Patient and PATIENT_STATE.Date = CONVERT(DATE, GETDATE()) and PATIENT_STATE.State=3)
    update TEMPORARY_DATA
    set resulted_data = @todayDeceasedVariable
    where requested_data = 'todayDeceased'

    -- Adds how many patients are at home from selected country to TEMPORARY_DATA:
    DECLARE @patientAtHomeVariable int;
    SET @patientAtHomeVariable=(select count(*) resulted_data from PATIENT where Country = @Country and Hospitalized != 1)
    update TEMPORARY_DATA
    set resulted_data = @patientAtHomeVariable
    where requested_data = 'patientsAtHome'

    -- Adds how many patients are hospitalized from selected country to TEMPORARY_DATA:
    DECLARE @patientsHospitalizedVariable int;
    SET @patientsHospitalizedVariable=(select count(*) resulted_data from PATIENT where Country = @Country and Hospitalized = 1)
    update TEMPORARY_DATA
    set resulted_data = @patientsHospitalizedVariable
    where requested_data = 'patientsHospitalized'

    -- Adds how many patients are in ICU from selected country to TEMPORARY_DATA:
    DECLARE @patientsInICUVariable int;
    SET @patientsInICUVariable=(select count(*) resulted_data from PATIENT where Country = @Country and ICU = 1)
    update TEMPORARY_DATA
    set resulted_data = @patientsInICUVariable
    where requested_data = 'patientsInICU'

    -- Adds how many patients are females from selected country to TEMPORARY_DATA:
    DECLARE @femaleCasesVariable int;
    SET @femaleCasesVariable=(select count(*) resulted_data from PATIENT where Country = @Country and Sex='F')
    update TEMPORARY_DATA
    set resulted_data = @femaleCasesVariable
    where requested_data = 'femaleCases'

    -- Adds how many patients are males from selected country to TEMPORARY_DATA:
    DECLARE @maleCasesVariable int;
    SET @maleCasesVariable=(select count(*) resulted_data from PATIENT where Country = @Country and Sex='M')
    update TEMPORARY_DATA
    set resulted_data = @maleCasesVariable
    where requested_data = 'maleCases'

    -- Adds how many patients are in the 0-12 age group from selected country to TEMPORARY_DATA:
    DECLARE @0_12Variable int;
    SET @0_12Variable=(select count(*) resulted_data
                        from PATIENT
                        where DATEDIFF(year,PATIENT.BirthDate,CONVERT(DATE, GETDATE())) <= 12 and Country=@Country)
    update TEMPORARY_DATA
    set resulted_data = @0_12Variable
    where requested_data = '0-12'

    -- Adds how many patients are in the 13-20 age group from selected country to TEMPORARY_DATA:
    DECLARE @13_20Variable int;
    SET @13_20Variable=(select count(*) resulted_data
                        from PATIENT
                        where DATEDIFF(year,PATIENT.BirthDate,CONVERT(DATE, GETDATE())) >= 13 and DATEDIFF(year,PATIENT.BirthDate,CONVERT(DATE, GETDATE())) <= 20 and Country=@Country)
    update TEMPORARY_DATA
    set resulted_data = @13_20Variable
    where requested_data = '13-20'

    -- Adds how many patients are in the 21-39 age group from selected country to TEMPORARY_DATA:
    DECLARE @21_39Variable int;
    SET @21_39Variable=(select count(*) resulted_data
                        from PATIENT
                        where DATEDIFF(year,PATIENT.BirthDate,CONVERT(DATE, GETDATE())) >= 21 and DATEDIFF(year,PATIENT.BirthDate,CONVERT(DATE, GETDATE())) <= 39 and Country=@Country)
    update TEMPORARY_DATA
    set resulted_data = @21_39Variable
    where requested_data = '21-39'

    -- Adds how many patients are in the 40-59 age group from selected country to TEMPORARY_DATA:
    DECLARE @40_59Variable int;
    SET @40_59Variable=(select count(*) resulted_data
                        from PATIENT
                        where DATEDIFF(year,PATIENT.BirthDate,CONVERT(DATE, GETDATE())) >= 40 and DATEDIFF(year,PATIENT.BirthDate,CONVERT(DATE, GETDATE())) <= 59 and Country=@Country)
    update TEMPORARY_DATA
    set resulted_data = @40_59Variable
    where requested_data = '40-59'

    -- Adds how many patients are in the 60-79 age group from selected country to TEMPORARY_DATA:
    DECLARE @60_79Variable int;
    SET @60_79Variable=(select count(*) resulted_data
                        from PATIENT
                        where DATEDIFF(year,PATIENT.BirthDate,CONVERT(DATE, GETDATE())) >= 60 and DATEDIFF(year,PATIENT.BirthDate,CONVERT(DATE, GETDATE())) <= 79 and Country=@Country)
    update TEMPORARY_DATA
    set resulted_data = @60_79Variable
    where requested_data = '60-79'

    -- Adds how many patients are in the +80 age group from selected country to TEMPORARY_DATA:
    DECLARE @plus80Variable int;
    SET @plus80Variable=(select count(*) resulted_data
                        from PATIENT
                        where DATEDIFF(year,PATIENT.BirthDate,CONVERT(DATE, GETDATE())) >= 80 and Country=@Country)
    update TEMPORARY_DATA
    set resulted_data = @plus80Variable
    where requested_data = '+80'

    select *
    from TEMPORARY_DATA

end
go


create procedure spCasesByRegion
@Country nvarchar(20)
as
Begin
    select Region, STATE.Name, count(*) cantidad
    from PATIENT_STATE
    inner join (select Patient, max(Date) date
                from PATIENT_STATE
                group by Patient) PS
    on PATIENT_STATE.Patient = PS.Patient and PS.date = PATIENT_STATE.Date
    inner join PATIENT
    on PATIENT.Country = @Country and PATIENT_STATE.Patient = PATIENT.Ssn
    inner join STATE
    on STATE.Id = PATIENT_STATE.State
    group by STATE.Name, Region
end
go

create procedure spAccumulatedCasesByCountry
@Country nvarchar(20)
as
Begin

    create table #Temp
    (
    activityDate Date,
    cantidad int,
    )

    insert into #Temp
    select Date, count(*) quantity
    from PATIENT
    inner join PATIENT_STATE
    on PATIENT.Country = @Country and PATIENT.Ssn=PATIENT_STATE.Patient and PATIENT_STATE.State=2 and (Date > (select dateadd(day, -30, getdate())))
    group by Date

    select *
    from #Temp

    select *, sum(cantidad) over (order by activityDate asc) as accumulatedCases
    from #Temp

    If(OBJECT_ID('tempdb..#temp') Is Not Null)
    Begin
        Drop Table #Temp
    End
end
go

create procedure spMeasurementsState
@Country nvarchar(20)
as
Begin
    select Name, CASE WHEN FinalDate >= (CONVERT(DATE, GETDATE())) THEN 'Active' ELSE 'Inactive' END as State, FinalDate
    from SANITARY_MEASUREMENTS
    inner join ENFORCES E
    on SANITARY_MEASUREMENTS.Id = E.Measurement where Country = @Country
    group by Name,FinalDate
end
go

create procedure spActiveDailyCases
@Country nvarchar(20)
as
Begin
    select PATIENT_STATE.Date, count(*) activeQuantity
    from PATIENT_STATE
    inner join (select Patient, max(Date) date
                from PATIENT_STATE
                group by Patient) PS
    on PATIENT_STATE.Patient = PS.Patient and PS.date = PATIENT_STATE.Date and PATIENT_STATE.State = 2 and (PATIENT_STATE.Date > (select dateadd(day, -30, getdate())))
    inner join PATIENT
    on PATIENT.Country = @Country and PATIENT_STATE.Patient = PATIENT.Ssn
    group by PATIENT_STATE.Date
end
go

create procedure spRecoveredDailyCases
@Country nvarchar(20)
as
Begin
    select PATIENT_STATE.Date, count(*) recoveredQuantity
    from PATIENT_STATE
    inner join (select Patient, max(Date) date
                from PATIENT_STATE
                group by Patient) PS
    on PATIENT_STATE.Patient = PS.Patient and PS.date = PATIENT_STATE.Date and PATIENT_STATE.State = 1 and (PATIENT_STATE.Date > (select dateadd(day, -30, getdate())))
    inner join PATIENT
    on PATIENT.Country = @Country and PATIENT_STATE.Patient = PATIENT.Ssn
    group by PATIENT_STATE.Date
end
go

create procedure spDeathsDailyCases
@Country nvarchar(20)
as
Begin
    select PATIENT_STATE.Date, count(*) deathQuantity
    from PATIENT_STATE
    inner join (select Patient, max(Date) date
                from PATIENT_STATE
                group by Patient) PS
    on PATIENT_STATE.Patient = PS.Patient and PS.date = PATIENT_STATE.Date and PATIENT_STATE.State = 3 and (PATIENT_STATE.Date > (select dateadd(day, -30, getdate())))
    inner join PATIENT
    on PATIENT.Country = @Country and PATIENT_STATE.Patient = PATIENT.Ssn
    group by PATIENT_STATE.Date
end
go

