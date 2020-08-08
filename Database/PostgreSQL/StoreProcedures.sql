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

-- Selects all procedures for the given patient returns the start and final date
create or replace function usp_patient_reservation_duration(patientId varchar(15))
returns table (
                ReservationId integer,
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
        inner join patient on r.patient_id = patient.ssn
        where patient.ssn = patientId;
end;
$$;

-- Inserts a reservation_procedures value
create or replace function usp_insert_reservation(reservationId integer, procedureName varchar(15)) returns integer
language plpgsql
as $$
declare procedureId integer := udf_get_procedure(procedureName);
begin

    if udf_verify_reservation_in_range(reservationId, procedureId)
    then
        insert into reservation_procedures (procedure_id, reservation_id)
        values (procedureId, reservationId);
    else
        raise exception 'No reservation available for this date';
    end if;
    return 1;
end;
$$;

-- Inserts a bed_equipment value
create or replace function usp_insert_bed_equipment(bedNumber integer, equipmentName varchar(15)) returns integer
language plpgsql
as $$
begin
    insert into bed_equipment (bed_number, equipment_id)
    values (bedNumber, udf_get_equipment(equipmentName));
    return 1;
end;
$$;

-- Selects all equipments for the given bed
create or replace function usp_equipments_from_bed(bednumber integer)
returns table (
                Id integer,
                Name varchar(15),
                Provider varchar(15),
                Quantity integer
              )
language plpgsql
as $$
begin
    return query
        select e.id, e.name, e.provider, e.quantity
        from bed_equipment as be
        inner join equipment e on be.equipment_id = e.id
        inner join bed b on be.bed_number = b.number
        where b.number = bednumber;
end;
$$;

-- Updates an old equipment for a new one from bed
create or replace function usp_update_equipment_from_bed
    (
    bedNumber integer,
    oldEquipment varchar(15),
    newEquipment varchar(15)
    ) returns integer
language plpgsql
as $$
begin
    update bed_equipment
    set equipment_id = udf_get_equipment(newEquipment)
    where equipment_id = udf_get_equipment(oldEquipment) and
          bed_number = bedNumber;
    return 1;
end;
$$;

-- Deletes a relationship in bed_equipment
create or replace function usp_delete_bed_equipment(bedNumber integer, equipmentId integer) returns integer
language plpgsql
as $$
begin
    delete from bed_equipment
    where bed_number = bedNumber and
          equipment_id = equipmentId;
    return 1;
end;
$$;

-- Makes a reservation if date and hospital are available
create or replace function usp_make_reservation
    (
    patientSsn varchar(15),
    reservationDate varchar(15),
    hospitalId integer
    ) returns integer
language plpgsql
as $$
begin
    if udf_verify_reservation(date(reservationDate), hospitalId)
    then
        insert into reservation (startdate, hospital_id, patient_id)
        values (date(reservationDate), hospitalId, patientSsn);
    else
        raise exception 'No reservation available for this date';
    end if;
    return 1;
end;
$$;

-- Updates a reservation if date and hospital are available
create or replace function usp_update_reservation
    (
    hospitalId integer,
    reservationId integer,
    reservationDate varchar (15)
    ) returns integer
language plpgsql
as $$
begin
    if udf_verify_reservation(date(reservationDate), hospitalId)
    then
        update reservation
        set startdate = date(reservationDate)
        where id = reservationId;
    else
        raise exception 'No reservation available for this date';
    end if;
    return 1;
end;
$$;

-- Selects all procedures for the given patient and reservation, ordered by date
create or replace function usp_patient_procedure(patientId varchar(15), reservationId integer)
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
        where patient.ssn = patientId and
              reservation_id = reservationId
        order by r.startdate desc;
end;
$$;