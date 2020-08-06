-- Selects all procedures for the given patient ordered by date
create or replace function usp_patient_procedure(patientId varchar(15))
returns table (
                Patient varchar(15),
                Procedure varchar(15),
                Date date,
                Duration integer
              )
language plpgsql
as $$
begin
    return query
        select patient.ssn,
               p.name,
               r.startdate,
               p.duration
        from reservation_procedures as rp
        inner join procedure p on rp.procedure_id = p.id
        inner join reservation r on rp.reservation_id = r.id
        inner join patient on r.patient_id = patient.ssn
        where patient.ssn = patientId
        order by r.startdate desc;
end;
$$;

-- Auxiliary function to add the duration to the start date
create or replace function udf_reservation_duration(start_date date, duration integer)
returns date
language plpgsql
as $$
begin
    return start_date + duration * interval'1 day';
end;
$$;

-- Selects all procedures for the given patient returns the start and final date
create or replace function usp_patient_procedure_duration(patientId varchar(15))
returns table (
                Reservation integer,
                StartDate date,
                FinalDate date
              )
language plpgsql
as $$
begin
    return query
        select r.id,
               r.startdate,
               udf_reservation_duration(r.startdate, p.duration)
        from reservation_procedures as rp
        inner join procedure p on rp.procedure_id = p.id
        inner join reservation r on rp.reservation_id = r.id
        inner join patient on r.patient_id = patient.ssn;
end;
$$;


