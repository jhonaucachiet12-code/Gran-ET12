DELIMITER $$

DROP TRIGGER IF EXISTS AftInsertUsuario $$

CREATE TRIGGER AftInsertUsuario
AFTER INSERT ON Usuario
FOR EACH ROW
BEGIN
    INSERT INTO Plantilla (idUsuario, nombre, presupuesto, cantidadJugadores)
    VALUES (NEW.idUsuario, CONCAT('Plantilla de ', NEW.nombre), 9000000, 0);
END $$
DELIMITER ;
-- Restar el costo del futbolitas del presupuesto de la plantilla

DELIMITER $$

DROP TRIGGER IF EXISTS AfInsrtPlantillaJugadores $$

CREATE TRIGGER AfInsrtPlantillaJugadores AFTER INSERT ON PlantillaJugadores
FOR EACH ROW
BEGIN
    UPDATE Plantilla
    SET presupuesto = presupuesto - (
            SELECT cotizacion
            FROM Jugador
            WHERE idJugador = NEW.idJugador
        ),
        cantidadJugadores = cantidadJugadores + 1
    WHERE idPlantilla = NEW.idPlantilla;
END $$

DELIMITER ;