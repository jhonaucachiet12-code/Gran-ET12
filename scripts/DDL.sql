DROP DATABASE if EXISTS bd_GranET;
CREATE DATABASE bd_GranET;
use bd_GranET;

CREATE TABLE Usuario
(
    idUsuario SMALLINT AUTO_INCREMENT PRIMARY KEY, 
    nombre VARCHAR(50) NOT NULL,
    apellido VARCHAR (50) NOT NULL,
    email VARCHAR(100) UNIQUE NOT NULL,
    fechaNacimiento DATE NOT NULL,
    contraseña VARCHAR NOT NULL
);


CREATE TABLE Equipo
(
    idEquipo TINYINT AUTO_INCREMENT  PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL UNIQUE
    
);
CREATE TABLE Tipo
(
    idTipo INT AUTO_INCREMENT PRIMARY KEY ,
    nombre VARCHAR(64) UNIQUE
);

CREATE TABLE Jugador
(
    idJugador int AUTO_INCREMENT PRIMARY KEY,
    idTipo INT AUTO_INCREMENT ,
    idEquipo INT AUTO_INCREMENT ,
    nombre VARCHAR(50) NOT NULL,
    apellido VARCHAR (50) NOT NULL,
    apodo VARCHAR(50)NOT NULL,
    nacimiento DATE NOT NULL,
    cotización DECIMAL(2,0) NOT NULL
    CONSTRAINT FK_Plantilla_Usuario FOREIGN KEY (idUsuario)
        REFERENCES Usuario (idUsuario),
    

);

CREATE Table Plantilla
(
    idPlantilla TINYINT AUTO_INCREMENT PRIMARY KEY,
    idUsuario SMALLINT AUTO_INCREMENT ,
    idJugador int AUTO_INCREMENT ,
    presupuesto INT NOT NULL,
    cantidaJugadores TINYINT NOT NULL,

    CONSTRAINT FK_Plantilla_Usuario FOREIGN KEY (idUsuario)
        REFERENCES Usuario (idUsuario),
    CONSTRAINT FK_Plantilla_Jugador FOREIGN KEY (idJugador)
        REFERENCES Jugador (idJugador),


);