-- Selects all procedures for the given patient ordered by date
create or replace function usp_patient_procedure(patientId varchar(15))
returns table (
                Procedure varchar(15),
                Date date,
                Duration integer
              )
language plpgsql
as $$
begin
    return query
        select p.name,
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

-- Auxiliary function to get the procedure_id using the name
create or replace function udf_get_procedure(procedureName varchar(15))
returns integer
language plpgsql
as $$
begin
    return
        (select p.id
        from procedure as p
        where p.name = procedureName);
end;
$$;

-- Inserts a reservation_procedures value
create or replace procedure usp_insert_reservation(reservationId integer, procedureName varchar(15))
language plpgsql
as $$
begin
    insert into reservation_procedures (procedure_id, reservation_id)
    values (udf_get_procedure(procedureName), reservationId);
end;
$$;

-- Auxiliary function to get the equipment_id using the name
create or replace function udf_get_equipment(equipmentName varchar(15))
returns integer
language plpgsql
as $$
begin
    return
        (select e.id
        from equipment as e
        where e.name = equipmentName);
end;
$$;

-- Inserts a bed_equipment value
create or replace procedure usp_insert_bed_equipment(bedNumber integer, equipmentName varchar(15))
language plpgsql
as $$
begin
    insert into bed_equipment (bed_number, equipment_id)
    values (bedNumber, udf_get_equipment(equipmentName));
end;
$$;

-- Selects all equipments for the given bed
create or replace function usp_equipments_from_bed(bedNumber integer)
returns table (Equipment varchar(15))
language plpgsql
as $$
begin
    return query
        select e.name
        from bed_equipment as be
        inner join equipment e on be.equipment_id = e.id
        inner join bed b on be.bed_number = b.number
        where b.number = bed_number;
end;
$$;

-- Updates an old equipment for a new one from bed
create or replace procedure usp_update_equipment_from_bed
    (
    bedNumber integer,
    oldEquipment varchar(15),
    newEquipment varchar(15)
    )
language plpgsql
as $$
begin
    update bed_equipment
    set equipment_id = udf_get_equipment(newEquipment)
    where equipment_id = udf_get_equipment(oldEquipment) and
          bed_number = bedNumber;
end;
$$;

-- Deletes a relationship in bed_equipment
create or replace procedure usp_delete_bed_equipment(bedNumber integer, equipmentId integer)
language plpgsql
as $$
begin
    delete from bed_equipment
    where bed_number = bedNumber and
          equipment_id = equipmentId;
end;
$$;