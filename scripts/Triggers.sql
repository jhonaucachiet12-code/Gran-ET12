
DELIMITER $$
DROP TRIGGER if EXISTS BefInsert $$
CREATE Trigger BefInsert before insert  on Usuario 
FOR EACH ROW
BEGIN

    INSERT INTO Plantilla(idPlantilla, idUsuario, nombre, presupuesto, cantidaJugadores)
    VALUES (new.idPlantilla,new.idUsuario,new.nombre,new.presupuesto,new.cantidaJugadores );
    
END $$