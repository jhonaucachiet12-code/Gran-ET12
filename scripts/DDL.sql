DROP DATABASE IF EXISTS bd_GranET;
CREATE DATABASE bd_GranET;
USE bd_GranET;

CREATE TABLE Rol
(
    idRol TINYINT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL
);

CREATE TABLE Usuario
(
    idUsuario SMALLINT AUTO_INCREMENT PRIMARY KEY, 
    nombre VARCHAR(50) NOT NULL,
    apellido VARCHAR(50) NOT NULL,
    email VARCHAR(100) UNIQUE NOT NULL UNIQUE,
    fechaNacimiento DATE NOT NULL,
    PasswordHash VARCHAR(65) NOT NULL,
    idRol TINYINT NOT NULL,

    CONSTRAINT FK_Usuario_Rol FOREIGN KEY (idRol)
        REFERENCES Rol (idRol)
);

CREATE TABLE Equipo
(
    idEquipo TINYINT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE Posicion
(
    idPosicion TINYINT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(64) UNIQUE
);

INSERT INTO Posicion (nombre) VALUES ('Arquero'), ('Defensor'), ('Mediocampista'), ('Delantero');

CREATE TABLE Jugador
(
    idJugador SMALLINT AUTO_INCREMENT PRIMARY KEY,
    idPosicion TINYINT NOT NULL,
    idEquipo TINYINT NOT NULL,
    nombre VARCHAR(50) NOT NULL,
    apellido VARCHAR(50) NOT NULL,
    apodo VARCHAR(50) NOT NULL,
    nacimiento DATE NOT NULL,
    cotización DECIMAL(10,2) NOT NULL,
    CONSTRAINT FK_Jugador_Posicion FOREIGN KEY (idPosicion)
        REFERENCES Posicion (idPosicion),
    CONSTRAINT FK_Jugador_Equipo FOREIGN KEY (idEquipo)
        REFERENCES Equipo (idEquipo)
);

CREATE TABLE Plantilla
(
    idPlantilla INT AUTO_INCREMENT PRIMARY KEY,
    idUsuario SMALLINT NOT NULL,
    nombre VARCHAR(50) NOT NULL,
    presupuesto DECIMAL(10,2) NOT NULL,
    cantidadJugadores TINYINT NOT NULL,

    CONSTRAINT FK_Plantilla_Usuario FOREIGN KEY (idUsuario)
        REFERENCES Usuario (idUsuario),
    CONSTRAINT FK_Plantilla_Jugador FOREIGN KEY (idJugador)
        REFERENCES Jugador (idJugador)
);

CREATE TABLE Puntuacion
(
    idJugador SMALLINT NOT NULL,
    Fecha TINYINT NOT NULL,
    puntuacion DECIMAL(4,2) NOT NULL,
    PRIMARY KEY(idJugador, Fecha),
    CONSTRAINT FK_Puntuacion_Jugador FOREIGN KEY (idJugador)
        REFERENCES Jugador (idJugador)
);

CREATE TABLE PlantillaJugadores
(
    idPlantilla INT NOT NULL,
    idJugador SMALLINT NOT NULL,
    titulares BOOLEAN NOT NULL,
    PRIMARY KEY (idPlantilla, idJugador),
    CONSTRAINT FK_PlantillaJugadores_Jugador FOREIGN KEY (idJugador)
        REFERENCES Jugador (idJugador),
    CONSTRAINT FK_PlantillaJugadores_Plantilla FOREIGN KEY (idPlantilla)
        REFERENCES Plantilla (idPlantilla)
);


-- consulta para traer la puntuacion e los jugadores de su plantilla 

SELECT AVG(Puntuacion)
from PlantillaJugadores L
INNER join Puntuacion U on L.idJugador = U.idJugador
WHERE L.idJugador = U.IdJugador AND L.idJugador = IdJugador

