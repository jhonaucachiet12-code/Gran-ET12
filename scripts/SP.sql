-- procedure para insertar, actualizar y eliminar registros en la base de datos de mysql 

delimiter $$
drop procedure if exists insertarRol$$
create procedure insertarRol(out unIdRol tinyint UNSIGNED, in unNombre varchar(50))
begin
    insert into rol(nombreRol) 
    values(unNombre);
    set unIdRol = last_insert_id();
end$$

drop procedure if exists actualizarRol$$
create procedure actualizarRol(in unIdRol tinyint UNSIGNED, in unNombre varchar(50))
begin
    update rol
    set nombreRol = unNombre
    where idRol = unIdRol;
end$$

drop procedure if exists eliminarRol$$
create procedure eliminarRol(in unIdRol tinyint UNSIGNED)
begin
    delete from rol
    where idRol = unIdRol;
end$$

--procedure de equipo para insertar, actualizar y eliminar registros en la base de datos de mysql
delimiter $$
drop procedure if exists insertarEquipo$$
create procedure insertarEquipo(out unIdEquipo tinyint UNSIGNED, in unNombre varchar(50))
begin
    insert into equipo(nombre) 
    values(unNombre);
    set unIdEquipo = last_insert_id();
end$$

drop procedure if exists actualizarEquipo$$
create procedure actualizarEquipo(in unIdEquipo tinyint UNSIGNED, in unNombre varchar(50))
begin
    update equipo 
    set nombre = unNombre
    where idEquipo = unIdEquipo;
end$$

drop procedure if exists eliminarEquipo$$
create procedure eliminarEquipo(in unIdEquipo tinyint UNSIGNED)
begin
    delete from equipo 
    where idEquipo = unIdEquipo;
end$$

--procedure de posicion para insertar, actualizar y eliminar registros en la base de datos de mysql
delimiter $$
drop procedure if exists insertarPosicion$$
create procedure insertarPosicion(out unIdPosicion tinyint UNSIGNED, in unNombre varchar(50))
begin
    insert into posicion(nombre) 
    values(unNombre);
    set unIdPosicion = last_insert_id();
end$$

drop procedure if exists actualizarPosicion$$
create procedure actualizarPosicion(in unIdPosicion tinyint UNSIGNED, in unNombre varchar(50))
begin
    update posicion 
    set nombre = unNombre
    where idPosicion = unIdPosicion;
end$$

drop procedure if exists eliminarPosicion$$
create procedure eliminarPosicion(in unIdPosicion tinyint UNSIGNED)
begin
    delete from posicion 
    where idPosicion = unIdPosicion;
end$$

-- procedure para agregar y eliminar jugadores de la plantilla
delimiter $$
drop procedure if exists agregarJugadorAPlantilla$$
create procedure agregarJugadorAPlantilla(in unIdPlantilla int unsigned, in unIdJugador int unsigned, in unTitular boolean)
begin
    insert into jugador_plantilla(idPlantilla, idJugador, titulares) 
    values(unIdPlantilla, unIdJugador, unTitular);
end$$

drop procedure if exists eliminarJugadorDePlantilla$$
create procedure eliminarJugadorDePlantilla(in unIdPlantilla int unsigned, in unIdJugador int unsigned)
begin
    delete from jugador_plantilla 
    where idPlantilla = unIdPlantilla and idJugador = unIdJugador;
end$$

drop procedure if exists actualizarTitularidadJugador$$
create procedure actualizarTitularidadJugador(in unIdPlantilla int unsigned, in unIdJugador int unsigned, in unTitular boolean)
begin
    update jugador_plantilla 
    set titulares = unTitular
    where idPlantilla = unIdPlantilla and idJugador = unIdJugador;
end$$

-- procedure de jugador para insertar, actualizar y eliminar registros en la base de datos de mysql
delimiter $$
drop procedure if exists insertarJugador$$
create procedure insertarJugador(out unIdJugador smallint UNSIGNED, in unNombre varchar(50), in unApellido varchar(50), in unApodo varchar(50),in unNacimiento date, in unaCotizacion decimal(10,2), in unIdPosicion tinyint, in unIdEquipo tinyint)
begin
    insert into jugadores(nombre, apellido, apodo, nacimiento, cotización, idPosicion, idEquipo) 
    values(unNombre, unApellido, unApodo, unNacimiento, unaCotizacion, unIdPosicion, unIdEquipo);
    set unIdJugador = last_insert_id();
end$$

drop procedure if exists actualizarJugador$$
create procedure actualizarJugador(in unIdJugador smallint UNSIGNED, in unNombre varchar(50), in unApellido varchar(50), in unApodo varchar(50),in unNacimiento date, in unaCotizacion decimal(10,2), in unIdPosicion tinyint, in unIdEquipo tinyint)
begin
    update jugadores 
    set nombre = unNombre, apellido = unApellido, apodo = unApodo, nacimiento = unNacimiento, cotización = unaCotizacion, idPosicion = unIdPosicion, idEquipo = unIdEquipo
    where idJugador = unIdJugador;
end$$

drop procedure if exists eliminarJugador$$
create procedure eliminarJugador(in unIdJugador smallint UNSIGNED)
begin
    delete from jugadores 
    where idJugador = unIdJugador;
end$$

-- procedure de plantilla para insertar, actualizar y eliminar registros en la base de datos de mysql
delimiter $$
drop procedure if exists insertarPlantilla$$
create procedure insertarPlantilla(out unIdPlantilla int unsigned, in unNombre varchar(50), in unPresupuesto decimal(10,2), in unaCantidadJugadores tinyint, in unIdUsuario smallint UNSIGNED)
begin
    insert into plantillas(nombre, presupuesto, cantidadJugadores, idUsuario) 
    values(unNombre, unPresupuesto, unaCantidadJugadores, unIdUsuario);
    set unIdPlantilla = last_insert_id();
end$$

drop procedure if exists actualizarPlantilla$$
create procedure actualizarPlantilla(in unIdPlantilla int unsigned, in unNombre varchar(50), in unPresupuesto decimal(10,2), in unaCantidadJugadores tinyint, in unIdUsuario smallint UNSIGNED)
begin
    update plantillas 
    set nombre = unNombre, presupuesto = unPresupuesto, cantidadJugadores = unaCantidadJugadores, idUsuario = unIdUsuario
    where idPlantilla = unIdPlantilla;
end$$

drop procedure if exists eliminarPlantilla$$
create procedure eliminarPlantilla(in unIdPlantilla int unsigned)
begin
    delete from plantillas 
    where idPlantilla = unIdPlantilla;
end$$

-- procedure de puntuacion para insertar, actualizar y eliminar registros en la base de datos de mysql
delimiter $$
drop procedure if exists insertarPuntuacion$$
create procedure insertarPuntuacion(in unIdJugador smallint UNSIGNED, in unaFecha tinyint, in unaPuntuacion decimal(4,2))
begin
    insert into puntuacion(idJugador, Fecha, puntuacion) 
    values(unIdJugador, unaFecha, unaPuntuacion);
end$$

drop procedure if exists actualizarPuntuacion$$
create procedure actualizarPuntuacion(in unIdJugador smallint UNSIGNED, in unaFecha tinyint, in unaPuntuacion decimal(4,2))
begin
    update puntuacion 
    set puntuacion = unaPuntuacion
    where idJugador = unIdJugador and Fecha = unaFecha;
end$$

drop procedure if exists eliminarPuntuacion$$
create procedure eliminarPuntuacion(in unIdJugador smallint UNSIGNED, in unaFecha tinyint)
begin
    delete from puntuacion 
    where idJugador = unIdJugador and Fecha = unaFecha;
end$$
 
-- procedure de usuario para insertar, actualizar y eliminar registros en la base de datos de mysql
delimiter $$
drop procedure if exists insertarUsuario$$
create procedure insertarUsuario(out unIdUsuario smallint UNSIGNED, in unNombre varchar(50), in unApellido varchar(50), in unEmail varchar(50), in unPassword varchar(50), in unIdRol tinyint)
begin
    insert into usuarios(nombre, apellido, email, PasswordHash, idRol) 
    values(unNombre, unApellido, unEmail, unPassword, unIdRol);
    set unIdUsuario = last_insert_id();
end$$ 

drop procedure if exists actualizarUsuario$$
create procedure actualizarUsuario(in unIdUsuario smallint UNSIGNED, in unNombre varchar(50), in unApellido varchar(50), in unEmail varchar(50), in unPassword varchar(50), in unIdRol tinyint)
begin
    update usuarios 
    set nombre = unNombre, apellido = unApellido, email = unEmail, PasswordHash = unPassword, idRol = unIdRol
    where idUsuario = unIdUsuario;
end$$

drop procedure if exists eliminarUsuario$$
create procedure eliminarUsuario(in unIdUsuario smallint UNSIGNED)
begin
    delete from usuarios 
    where idUsuario = unIdUsuario;
end$$