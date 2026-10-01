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
    email VARCHAR(100) UNIQUE NOT NULL,
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
-- INSERT INTO Posicion (nombre) VALUES ('Arquero'), ('Defensor'), ('Mediocampista'), ('Delantero');


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
        REFERENCES Usuario (idUsuario)

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

-- SOURCE 00 DDL.sql
-- SOURCE 01 SP.sql
-- SOURCE 02 INSERTS.sql

-- dotnet run Gran
/*mysql -u 5to_agbd -p
Enter password: 
Welcome to the MySQL monitor.  Commands end with ; or \g.
Your MySQL connection id is 37
Server version: 8.0.46-0ubuntu0.22.04.4 (Ubuntu)

Copyright (c) 2000, 2026, Oracle and/or its affiliates.

Oracle is a registered trademark of Oracle Corporation and/or its
affiliates. Other names may be trademarks of their respective
owners.

Type 'help;' or '\h' for help. Type '\c' to clear the current input statement.

mysql> source install.sql
ERROR: 
Failed to open file 'install.sql', error: 2
mysql> ^C
mysql> */
   
