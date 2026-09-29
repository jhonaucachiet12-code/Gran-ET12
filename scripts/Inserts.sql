INSERT into Rol (idRol, nombre)
VALUES (1, "Usuario"),
        (2,"Admin")

INSERT into Usuario (idUsuario,nombre,apellido,email,fechaNacimiento,PasswordHash,idRol)
VALUES(1,"juan", "Parez", "Triplesiete777@al.com","2000-2-1", "12345678" , 1)