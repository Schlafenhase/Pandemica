
-- Auxiliary function to add the duration to the start date
create or replace function udf_reservation_duration(start_date date, duration integer)
returns date
language plpgsql
as $$
begin
    if duration is null then
        return start_date;
    end if;

    return start_date + duration * interval'1 day';
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

-- Auxiliary function to get the procedure duration
create or replace function udf_get_procedure_duration(procedureId integer)
returns integer
language plpgsql
as $$
begin
    return
        (select p.duration
        from procedure as p
        where p.id = procedureId);
end;
$$;

-- Auxiliary function to get the hospital_id of the reservation
create or replace function udf_get_reservation_hospital(reservationId integer)
returns integer
language plpgsql
as $$
begin
    return
        (select r.hospital_id
        from reservation as r
        where r.id = reservationId);
end;
$$;

-- Auxiliary function to get the reservation start date
create or replace function udf_get_reservation_date(reservationId integer)
returns date
language plpgsql
as $$
begin
    return
        (select r.startdate
        from reservation as r
        where r.id = reservationId);
end;
$$;

-- Function that counts how many beds are in a hospital
create or replace function udf_count_beds(hospitalId integer)
returns integer
language plpgsql
as $$
begin
    return (select count(b.number)
            from hospital as h
            inner join lounge l on h.id = l.hospital_id
            inner join bed b on l.number = b.lounge_number
            where h.id = hospitalId);
end;
$$;

-- Auxiliary function to verify reservation availability and returns a boolean
create or replace function udf_verify_reservation(reservationDate date, hospitalId integer, reservationId integer)
returns bool
language plpgsql
as $$
declare available bool;
begin
    select (count(r.id) < udf_count_beds(hospitalId)) into available
    from reservation as r
    inner join hospital h on r.hospital_id = h.id
    where h.id = hospitalId and
          r.startdate = reservationDate and
          r.id != reservationId;

    return available;
end;
$$;

-- Auxiliary function to verify reservation availability and returns a boolean
create or replace function udf_verify_reservation(reservationDate date, hospitalId integer)
returns bool
language plpgsql
as $$
declare available bool;
begin
    select (count(r.id) < udf_count_beds(hospitalId)) into available
    from reservation as r
    inner join hospital h on r.hospital_id = h.id
    where h.id = hospitalId and
          r.startdate = reservationDate;

    return available;
end;
$$;

-- Auxiliary function to verify reservation availability in date range and returns a boolean
create or replace function udf_verify_reservation_in_range(reservationId integer, procedureId integer)
returns bool
language plpgsql
as $$
declare
    startDate date := udf_get_reservation_date(reservationId);
    duration integer := udf_get_procedure_duration(procedureId);
    hospitalId integer := udf_get_reservation_hospital(reservationId);
    currentDate date;
begin
    for index in 0..duration
    loop
        currentDate := udf_reservation_duration(startDate, index);
        if not udf_verify_reservation(currentDate, hospitalId, reservationId) then
            return false;
        end if;
    end loop;

    return true;
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

