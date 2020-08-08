-- Auxiliary function that makes rollback on trigger
create or replace function udt_hospital_deleted()
returns trigger
language plpgsql
as $$
begin
    raise warning 'Hospitals can not be deleted';
    rollback;
end;
$$;

-- Trigger that stops hospitals from been deleted
create trigger hospital_delete
    before delete on hospital
    execute procedure udt_hospital_deleted();