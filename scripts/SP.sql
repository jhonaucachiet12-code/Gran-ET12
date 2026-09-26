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

-- procedure para agregar y eliminar jugadores de la plantilla
delimiter $$
drop procedure if exists agregarJugadorAPlantilla$$
create procedure agregarJugadorAPlantilla(in unIdPlantilla int, in unIdJugador int, in unTitular boolean)
begin
    insert into jugador_plantilla(idPlantilla, idJugador, titulares) 
    values(unIdPlantilla, unIdJugador, unTitular);
end$$

drop procedure if exists eliminarJugadorDePlantilla$$
create procedure eliminarJugadorDePlantilla(in unIdPlantilla int, in unIdJugador int)
begin
    delete from jugador_plantilla 
    where idPlantilla = unIdPlantilla and idJugador = unIdJugador;
end$$

drop procedure if exists actualizarTitularidadJugador$$
create procedure actualizarTitularidadJugador(in unIdPlantilla int, in unIdJugador int, in unTitular boolean)
begin
    update jugador_plantilla 
    set titulares = unTitular
    where idPlantilla = unIdPlantilla and idJugador = unIdJugador;
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

drop procedure if exists actualizarJugador$$
create procedure actualizarJugador(in unIdJugador short, in unNombre varchar(50), in unApellido varchar(50), in unApodo varchar(50),in unNacimiento date, in unaCotizacion decimal(10,2), in unIdPosicion tinyint, in unIdEquipo tinyint)
begin
    update jugadores 
    set nombre = unNombre, apellido = unApellido, apodo = unApodo, nacimiento = unNacimiento, cotización = unaCotizacion, idPosicion = unIdPosicion, idEquipo = unIdEquipo
    where idJugador = unIdJugador;
end$$

drop procedure if exists eliminarJugador$$
create procedure eliminarJugador(in unIdJugador short)
begin
    delete from jugadores 
    where idJugador = unIdJugador;
end$$

-- procedure de plantilla para insertar, actualizar y eliminar registros en la base de datos de mysql
delimiter $$
drop procedure if exists insertarPlantilla$$
create procedure insertarPlantilla(out unIdPlantilla int, in unNombre varchar(50), in unPresupuesto decimal(10,2), in unaCantidadJugadores tinyint, in unIdUsuario smallint)
begin
    insert into plantillas(nombre, presupuesto, cantidadJugadores, idUsuario) 
    values(unNombre, unPresupuesto, unaCantidadJugadores, unIdUsuario);
    set unIdPlantilla = last_insert_id();
end$$

drop procedure if exists actualizarPlantilla$$
create procedure actualizarPlantilla(in unIdPlantilla int, in unNombre varchar(50), in unPresupuesto decimal(10,2), in unaCantidadJugadores tinyint, in unIdUsuario smallint)
begin
    update plantillas 
    set nombre = unNombre, presupuesto = unPresupuesto, cantidadJugadores = unaCantidadJugadores, idUsuario = unIdUsuario
    where idPlantilla = unIdPlantilla;
end$$

drop procedure if exists eliminarPlantilla$$
create procedure eliminarPlantilla(in unIdPlantilla int)
begin
    delete from plantillas 
    where idPlantilla = unIdPlantilla;
end$$