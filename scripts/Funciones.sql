-- procedure para insertar, actualizar y eliminar registros en la base de datos de mysql 


delimiter $$
drop procedure if exists insertarRol$$
create procedure insertarRol(out unIdRol int, in unNombre varchar(50))
begin
    insert into roles(nombreRol) 
    values(unNombre);
    set unIdRol = last_insert_id();
end$$

create procedure actualizarRol(in unIdRol int, in unNombre varchar(50))
begin
    update roles 
    set nombreRol = unNombre
    where idRol = unIdRol;
end$$

create procedure eliminarRol(in unIdRol int)
begin
    delete from roles 
    where idRol = unIdRol;
end$$

--procedure de equipo para insertar, actualizar y eliminar registros en la base de datos de mysql
delimiter $$
drop procedure if exists insertarEquipo$$
create procedure insertarEquipo(out unIdEquipo int, in unNombre varchar(50))
begin
    insert into equipo(nombre) 
    values(unNombre);
    set unIdEquipo = last_insert_id();
end$$

create procedure actualizarEquipo(in unIdEquipo int, in unNombre varchar(50))
begin
    update equipo 
    set nombre = unNombre
    where idEquipo = unIdEquipo;
end$$

create procedure eliminarEquipo(in unIdEquipo int)
begin
    delete from equipo 
    where idEquipo = unIdEquipo;
end$$

--procedure de posicion para insertar, actualizar y eliminar registros en la base de datos de mysql
delimiter $$
drop procedure if exists insertarPosicion$$
create procedure insertarPosicion(out unIdPosicion int, in unNombre varchar(50))
begin
    insert into posiciones(nombre) 
    values(unNombre);
    set unIdPosicion = last_insert_id();
end$$

create procedure actualizarPosicion(in unIdPosicion int, in unNombre varchar(50))
begin
    update posiciones 
    set nombre = unNombre
    where idPosicion = unIdPosicion;
end$$

create procedure eliminarPosicion(in unIdPosicion int)
begin
    delete from posiciones 
    where idPosicion = unIdPosicion;
end$$
