DROP TRIGGER IF EXISTS AftInsertUsuario $$

CREATE TRIGGER AftInsertUsuario
AFTER INSERT ON Usuario
FOR EACH ROW
BEGIN
    INSERT INTO Plantilla (idUsuario, nombre, presupuesto, cantidadJugadores)
    VALUES (NEW.idUsuario, CONCAT('Plantilla de ', NEW.nombre), 9000000, 0);
END $$