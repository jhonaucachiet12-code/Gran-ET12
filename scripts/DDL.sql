DROP DATABASE if EXISTS bd_GranET;
CREATE DATABASE bd_GranET;
use bd_GranET;

CREATE TABLE Roles
(
    idRoles TINYINT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(50)NOT NULL ,

);
CREATE TABLE Usuario
(
    idUsuario SMALLINT AUTO_INCREMENT PRIMARY KEY, 
    nombre VARCHAR(50) NOT NULL,
    apellido VARCHAR (50) NOT NULL,
    email VARCHAR(100) UNIQUE NOT NULL,
    fechaNacimiento DATE NOT NULL,
    contraseña VARCHAR NOT NULL,
    idRoles TINYINT NOT NULL,

    CONSTRAINT FK_Usuario_Roles FOREIGN KEY (idRoles)
        REFERENCES Roles (idRoles),

);


CREATE TABLE Equipo
(
    idEquipo TINYINT AUTO_INCREMENT  PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL UNIQUE
    
);
CREATE TABLE Tipo
(
    idTipo TINYINT AUTO_INCREMENT PRIMARY KEY ,
    nombre VARCHAR(64) UNIQUE
);

INSERT INTO Tipo (nombre), VALUES ('Arquero'), ('Defensor'), ('Mediocampista'), ('Delantero');

CREATE TABLE Jugador
(
    idJugador SMALLINT AUTO_INCREMENT PRIMARY KEY,
    idTipo TINYINT AUTO_INCREMENT ,
    idEquipo TINYINT AUTO_INCREMENT ,
    nombre VARCHAR(50) NOT NULL,
    apellido VARCHAR (50) NOT NULL,
    apodo VARCHAR(50)NOT NULL,
    nacimiento DATE NOT NULL,
    cotización DECIMAL(2,0) NOT NULL
    CONSTRAINT FK_Jugador_Tipo FOREIGN KEY (idTipo)
        REFERENCES Tipo (idTipo),
    CONSTRAINT FK_Jugador_Equipo FOREIGN KEY (idEquipo)
        REFERENCES Equipo (idEquipo),

);

CREATE Table Plantilla
(
    idPlantilla TINYINT AUTO_INCREMENT PRIMARY KEY,
    idUsuario SMALLINT AUTO_INCREMENT ,
    idJugador SMALLINT AUTO_INCREMENT ,
    presupuesto INT NOT NULL,
    cantidaJugadores TINYINT NOT NULL,

    CONSTRAINT FK_Plantilla_Usuario FOREIGN KEY (idUsuario)
        REFERENCES Usuario (idUsuario),
    CONSTRAINT FK_Plantilla_Jugador FOREIGN KEY (idJugador)
        REFERENCES Jugador (idJugador),


);