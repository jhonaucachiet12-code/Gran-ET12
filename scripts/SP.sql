-- procedures para insertar, actualizar y eliminar registros en la base de datos bd_GranET

DELIMITER $$

-- ==================== ROL ====================
DROP PROCEDURE IF EXISTS insertarRol$$
CREATE PROCEDURE insertarRol(OUT unIdRol TINYINT UNSIGNED, IN unNombre VARCHAR(50))
BEGIN
    INSERT INTO Rol(nombre)
    VALUES (unNombre);
    SET unIdRol = LAST_INSERT_ID();
END$$

DROP PROCEDURE IF EXISTS actualizarRol$$
CREATE PROCEDURE actualizarRol(IN unIdRol TINYINT UNSIGNED, IN unNombre VARCHAR(50))
BEGIN
    UPDATE Rol
    SET nombre = unNombre
    WHERE idRol = unIdRol;
END$$

DROP PROCEDURE IF EXISTS eliminarRol$$
CREATE PROCEDURE eliminarRol(IN unIdRol TINYINT UNSIGNED)
BEGIN
    DELETE FROM Rol
    WHERE idRol = unIdRol;
END$$

-- ==================== EQUIPO ====================
DROP PROCEDURE IF EXISTS insertarEquipo$$
CREATE PROCEDURE insertarEquipo(OUT unIdEquipo TINYINT UNSIGNED, IN unNombre VARCHAR(50))
BEGIN
    INSERT INTO Equipo(nombre)
    VALUES (unNombre);
    SET unIdEquipo = LAST_INSERT_ID();
END$$

DROP PROCEDURE IF EXISTS actualizarEquipo$$
CREATE PROCEDURE actualizarEquipo(IN unIdEquipo TINYINT UNSIGNED, IN unNombre VARCHAR(50))
BEGIN
    UPDATE Equipo
    SET nombre = unNombre
    WHERE idEquipo = unIdEquipo;
END$$

DROP PROCEDURE IF EXISTS eliminarEquipo$$
CREATE PROCEDURE eliminarEquipo(IN unIdEquipo TINYINT UNSIGNED)
BEGIN
    DELETE FROM Equipo
    WHERE idEquipo = unIdEquipo;
END$$

-- ==================== POSICION ====================
DROP PROCEDURE IF EXISTS insertarPosicion$$
CREATE PROCEDURE insertarPosicion(OUT unIdPosicion TINYINT UNSIGNED, IN unNombre VARCHAR(50))
BEGIN
    INSERT INTO Posicion(nombre)
    VALUES (unNombre);
    SET unIdPosicion = LAST_INSERT_ID();
END$$

DROP PROCEDURE IF EXISTS actualizarPosicion$$
CREATE PROCEDURE actualizarPosicion(IN unIdPosicion TINYINT UNSIGNED, IN unNombre VARCHAR(50))
BEGIN
    UPDATE Posicion
    SET nombre = unNombre
    WHERE idPosicion = unIdPosicion;
END$$

DROP PROCEDURE IF EXISTS eliminarPosicion$$
CREATE PROCEDURE eliminarPosicion(IN unIdPosicion TINYINT UNSIGNED)
BEGIN
    DELETE FROM Posicion
    WHERE idPosicion = unIdPosicion;
END$$

-- ==================== PLANTILLAJUGADORES ====================
DROP PROCEDURE IF EXISTS agregarJugadorAPlantilla$$
CREATE PROCEDURE agregarJugadorAPlantilla(IN unIdPlantilla INT UNSIGNED, IN unIdJugador SMALLINT UNSIGNED, IN unTitular BOOLEAN)
BEGIN
    INSERT INTO PlantillaJugadores(idPlantilla, idJugador, titulares)
    VALUES (unIdPlantilla, unIdJugador, unTitular);
END$$

DROP PROCEDURE IF EXISTS eliminarJugadorDePlantilla$$
CREATE PROCEDURE eliminarJugadorDePlantilla(IN unIdPlantilla INT UNSIGNED, IN unIdJugador SMALLINT UNSIGNED)
BEGIN
    DELETE FROM PlantillaJugadores
    WHERE idPlantilla = unIdPlantilla AND idJugador = unIdJugador;
END$$

DROP PROCEDURE IF EXISTS actualizarTitularidadJugador$$
CREATE PROCEDURE actualizarTitularidadJugador(IN unIdPlantilla INT UNSIGNED, IN unIdJugador SMALLINT UNSIGNED, IN unTitular BOOLEAN)
BEGIN
    UPDATE PlantillaJugadores
    SET titulares = unTitular
    WHERE idPlantilla = unIdPlantilla AND idJugador = unIdJugador;
END$$

-- ==================== JUGADOR ====================
DROP PROCEDURE IF EXISTS insertarJugador$$
CREATE PROCEDURE insertarJugador(OUT unIdJugador SMALLINT UNSIGNED, IN unNombre VARCHAR(50), IN unApellido VARCHAR(50), IN unApodo VARCHAR(50), IN unNacimiento DATE, IN unaCotizacion DECIMAL(10,2), IN unIdPosicion TINYINT, IN unIdEquipo TINYINT)
BEGIN
    INSERT INTO Jugador(nombre, apellido, apodo, nacimiento, cotización, idPosicion, idEquipo)
    VALUES (unNombre, unApellido, unApodo, unNacimiento, unaCotizacion, unIdPosicion, unIdEquipo);
    SET unIdJugador = LAST_INSERT_ID();
END$$

DROP PROCEDURE IF EXISTS actualizarJugador$$
CREATE PROCEDURE actualizarJugador(IN unIdJugador SMALLINT UNSIGNED, IN unNombre VARCHAR(50), IN unApellido VARCHAR(50), IN unApodo VARCHAR(50), IN unNacimiento DATE, IN unaCotizacion DECIMAL(10,2), IN unIdPosicion TINYINT, IN unIdEquipo TINYINT)
BEGIN
    UPDATE Jugador
    SET nombre = unNombre, apellido = unApellido, apodo = unApodo, nacimiento = unNacimiento, cotización = unaCotizacion, idPosicion = unIdPosicion, idEquipo = unIdEquipo
    WHERE idJugador = unIdJugador;
END$$

DROP PROCEDURE IF EXISTS eliminarJugador$$
CREATE PROCEDURE eliminarJugador(IN unIdJugador SMALLINT UNSIGNED)
BEGIN
    DELETE FROM Jugador
    WHERE idJugador = unIdJugador;
END$$

-- ==================== PLANTILLA ====================
DROP PROCEDURE IF EXISTS insertarPlantilla$$
CREATE PROCEDURE insertarPlantilla(OUT unIdPlantilla INT UNSIGNED, IN unNombre VARCHAR(50), IN unPresupuesto DECIMAL(10,2), IN unaCantidadJugadores TINYINT, IN unIdUsuario SMALLINT UNSIGNED)
BEGIN
    INSERT INTO Plantilla(nombre, presupuesto, cantidadJugadores, idUsuario)
    VALUES (unNombre, unPresupuesto, unaCantidadJugadores, unIdUsuario);
    SET unIdPlantilla = LAST_INSERT_ID();
END$$

DROP PROCEDURE IF EXISTS actualizarPlantilla$$
CREATE PROCEDURE actualizarPlantilla(IN unIdPlantilla INT UNSIGNED, IN unNombre VARCHAR(50), IN unPresupuesto DECIMAL(10,2), IN unaCantidadJugadores TINYINT, IN unIdUsuario SMALLINT UNSIGNED)
BEGIN
    UPDATE Plantilla
    SET nombre = unNombre, presupuesto = unPresupuesto, cantidadJugadores = unaCantidadJugadores, idUsuario = unIdUsuario
    WHERE idPlantilla = unIdPlantilla;
END$$

DROP PROCEDURE IF EXISTS eliminarPlantilla$$
CREATE PROCEDURE eliminarPlantilla(IN unIdPlantilla INT UNSIGNED)
BEGIN
    DELETE FROM Plantilla
    WHERE idPlantilla = unIdPlantilla;
END$$

-- ==================== PUNTUACION ====================
DROP PROCEDURE IF EXISTS insertarPuntuacion$$
CREATE PROCEDURE insertarPuntuacion(IN unIdJugador SMALLINT UNSIGNED, IN unaFecha TINYINT, IN unaPuntuacion DECIMAL(4,2))
BEGIN
    INSERT INTO Puntuacion(idJugador, Fecha, puntuacion)
    VALUES (unIdJugador, unaFecha, unaPuntuacion);
END$$

DROP PROCEDURE IF EXISTS actualizarPuntuacion$$
CREATE PROCEDURE actualizarPuntuacion(IN unIdJugador SMALLINT UNSIGNED, IN unaFecha TINYINT, IN unaPuntuacion DECIMAL(4,2))
BEGIN
    UPDATE Puntuacion
    SET puntuacion = unaPuntuacion
    WHERE idJugador = unIdJugador AND Fecha = unaFecha;
END$$

DROP PROCEDURE IF EXISTS eliminarPuntuacion$$
CREATE PROCEDURE eliminarPuntuacion(IN unIdJugador SMALLINT UNSIGNED, IN unaFecha TINYINT)
BEGIN
    DELETE FROM Puntuacion
    WHERE idJugador = unIdJugador AND Fecha = unaFecha;
END$$

-- ==================== USUARIO ====================
DROP PROCEDURE IF EXISTS insertarUsuario$$
CREATE PROCEDURE insertarUsuario(OUT unIdUsuario SMALLINT UNSIGNED, IN unNombre VARCHAR(50), IN unApellido VARCHAR(50), IN unEmail VARCHAR(100), IN unFechaNacimiento DATE, IN unPasswordHash VARCHAR(65), IN unIdRol TINYINT)
BEGIN
    INSERT INTO Usuario(nombre, apellido, email, fechaNacimiento, PasswordHash, idRol)
    VALUES (unNombre, unApellido, unEmail, unFechaNacimiento, unPasswordHash, unIdRol);
    SET unIdUsuario = LAST_INSERT_ID();
END$$

DROP PROCEDURE IF EXISTS actualizarUsuario$$
CREATE PROCEDURE actualizarUsuario(IN unIdUsuario SMALLINT UNSIGNED, IN unNombre VARCHAR(50), IN unApellido VARCHAR(50), IN unEmail VARCHAR(100), IN unFechaNacimiento DATE, IN unPasswordHash VARCHAR(65), IN unIdRol TINYINT)
BEGIN
    UPDATE Usuario
    SET nombre = unNombre, apellido = unApellido, email = unEmail, fechaNacimiento = unFechaNacimiento, PasswordHash = unPasswordHash, idRol = unIdRol
    WHERE idUsuario = unIdUsuario;
END$$

DROP PROCEDURE IF EXISTS eliminarUsuario$$
CREATE PROCEDURE eliminarUsuario(IN unIdUsuario SMALLINT UNSIGNED)
BEGIN
    DELETE FROM Usuario
    WHERE idUsuario = unIdUsuario;
END$$

DELIMITER ;