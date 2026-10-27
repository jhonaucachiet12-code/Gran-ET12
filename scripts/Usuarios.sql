-- archivo que crea los usuarios y permisos de su proyecto
-- usuario
create user "usuarios"@"%" identified by "12345678";

-- select
grant select(nombre,apellido,apodo,nacimiento,cotización) on bd_GranET.Jugador  to "usuarios"@"%";
grant select(nombre) on bd_GranET.Rol  to "usuarios"@"%";
grant select on bd_GranET.Usuario  to "usuarios"@"%";
grant select(nombre) on bd_GranET.Equipo  to "usuarios"@"%";
grant select(nombre) on bd_GranET.Posicion  to "usuarios"@"%";
grant select on bd_GranET.Plantilla  to "usuarios"@"%";
grant select(Fecha, puntuacion) on bd_GranET.Puntuacion  to "usuarios"@"%";
grant select on bd_GranET.PlantillaJugadores  to "usuarios"@"%";

--UPDATE
grant UPDATE,(nombre, apellido, email, fechaNacimiento, PasswordHash) on bd_GranET.Usuario  to "usuarios"@"%";
grant UPDATE(titulares) on bd_GranET.PlantillaJugadores  to "usuarios"@"%";
grant UPDATE(nombre) on bd_GranET.Plantilla  to "usuarios"@"%";

--INSERT
grant INSERT on bd_GranET.Usuario  to "usuarios"@"%";
grant INSERT on bd_GranET.PlantillaJugadores  to "usuarios"@"%";
grant INSERT on bd_GranET.Plantilla  to "usuarios"@"%";

--administrador
create user "admin"@"%" identified by "12345678";

-- select
grant select on bd_GranET.Jugador  to "admin"@"%";
grant select(nombre) on bd_GranET.Rol  to "admin"@"%";
grant select on bd_GranET.Usuario  to "admin"@"%";
grant select(nombre) on bd_GranET.Equipo  to "admin"@"%";
grant select(nombre) on bd_GranET.Posicion  to "admin"@"%";
grant select on bd_GranET.Plantilla  to "admin"@"%";
grant select on bd_GranET.Puntuacion  to "admin"@"%";
grant select on bd_GranET.PlantillaJugadores  to "admin"@"%";

--UPDATE
grant UPDATE(nombre, apellido, email, fechaNacimiento, PasswordHash) on bd_GranET.Usuario  to "admin"@"%";
grant UPDATE(titulares) on bd_GranET.PlantillaJugadores  to "admin"@"%";
grant UPDATE(nombre) on bd_GranET.Plantilla  to "admin"@"%";
grant UPDATE(nombre,apellido,apodo,nacimiento,cotización) on bd_GranET.Jugador  to "admin"@"%";
GRANT UPDATE(Fecha, puntuacion) on bd_GranET.Puntuacion  to "admin"@"%";

--INSERT
grant INSERT on bd_GranET.Usuario  to "admin"@"%";
grant INSERT on bd_GranET.PlantillaJugadores  to "admin"@"%";
grant INSERT on bd_GranET.Plantilla  to "admin"@"%";
grant INSERT on bd_GranET.Jugador  to "admin"@"%";
grant INSERT on bd_GranET.Puntuacion  to "admin"@"%";

-- preguntar si el 