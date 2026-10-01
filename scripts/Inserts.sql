
INSERT INTO Equipo (idEquipo, nombre) VALUES (3, 'Real Madrid'), (2, 'Barcelona'),(1,"Boca Juniors") ;


INSERT INTO Posicion (nombre) VALUES ('Arquero'), ('Defensor'), ('Mediocampista'), ('Delantero');

INSERT INTO Rol (idRol, nombre) VALUES (1, 'Administrador'), (2, 'Jugador');
INSERT INTO Usuario (idUsuario, nombre, apellido, email, fechaNacimiento, PasswordHash, idRol) 
VALUES (1, 'Carlos', 'Gómez', 'carlos@email.com', '1995-05-12', 'hash_ejemplo', 2);




-- 3. Jugadores
INSERT INTO Jugador (idJugador, idPosicion, idEquipo, nombre, apellido, apodo, nacimiento, cotización) VALUES 
(1, 4, 1, 'Karim', 'Benzema', 'KB9', '1987-12-19', 50.00),
(2, 4, 2, 'Robert', 'Lewandowski', 'Lewy', '1988-08-21', 60.00),
(3, 3, 1, 'Luka', 'Modric', 'Lukita', '1985-09-09', 40.00);

-- 4. Plantilla (Creamos la plantilla con idPlantilla = 5)
INSERT INTO Plantilla (idPlantilla, idUsuario, nombre, presupuesto, cantidadJugadores) 
VALUES (5, 1, 'Mi Equipo Ideal', 90000000.00, 3);

-- 5. Relación PlantillaJugadores (Asignamos los 3 jugadores a la plantilla 5)
INSERT INTO PlantillaJugadores (idPlantilla, idJugador, titulares) VALUES 
(5, 1, TRUE),
(5, 2, TRUE),
(5, 3, FALSE);

-- 6. Puntuaciones (Con múltiples fechas para probar el filtro de la fecha máxima)
INSERT INTO Puntuacion (idJugador, Fecha, puntuacion) VALUES 
(1, 1, 7.50),
(1, 2, 9.00), -- Última fecha de Jugador 1 (Fecha 2)
(2, 1, 6.00),
(2, 2, 8.00),
(2, 3, 8.50), -- Última fecha de Jugador 2 (Fecha 3)
(3, 1, 7.00); -- Última fecha de Jugador 3 (Fecha 1)