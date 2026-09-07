
DELIMITER $$
DROP TRIGGER if EXISTS BefInsert $$
CREATE Trigger BefInsert before insert  on Usuario 
FOR EACH ROW
BEGIN

    INSERT INTO Plantilla(idPlantilla, idUsuario, idJugador, presupuesto, cantidaJugadores)
    
END $$