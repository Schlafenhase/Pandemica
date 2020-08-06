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
        select patient.ssn, p.name, r.startdate, p.duration
        from reservation_procedures as rp
        inner join procedure p on rp.procedure_id = p.id
        inner join reservation r on rp.reservation_id = r.id
        inner join patient on r.patient_id = patient.ssn
        where patient.ssn = patientId
        order by r.startdate desc;
end;
$$;
