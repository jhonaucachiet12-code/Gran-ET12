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

-- procedure de jugador para insertar, actualizar y eliminar registros en la base de datos de mysql
delimiter $$
drop procedure if exists insertarJugador$$
create procedure insertarJugador(out unIdJugador int, in unNombre varchar(50), in unApellido varchar(50), in unApodo varchar(50),in unNacimiento date, in unaCotizacion decimal(10,2), in unIdPosicion tinyint, in unIdEquipo tinyint)
begin
    insert into jugadores(nombre, apellido, apodo, nacimiento, cotización, idPosicion, idEquipo) 
    values(unNombre, unApellido, unApodo, unNacimiento, unaCotizacion, unIdPosicion, unIdEquipo);
    set unIdJugador = last_insert_id();
end$$

create procedure actualizarJugador(in unIdJugador short, in unNombre varchar(50), in unApellido varchar(50), in unApodo varchar(50),in unNacimiento date, in unaCotizacion decimal(10,2), in unIdPosicion tinyint, in unIdEquipo tinyint)
begin
    update jugadores 
    set nombre = unNombre, apellido = unApellido, apodo = unApodo, nacimiento = unNacimiento, cotización = unaCotizacion, idPosicion = unIdPosicion, idEquipo = unIdEquipo
    where idJugador = unIdJugador;
end$$

create procedure eliminarJugador(in unIdJugador short)
begin
    delete from jugadores 
    where idJugador = unIdJugador;
end$$
