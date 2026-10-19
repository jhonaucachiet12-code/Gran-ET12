# Actividad 1: Reglas, capas y pruebas pequeñas

Separamos en las capas service modelo, servicios, acceso a datos y API el programa. porque es más fácil de mantener , probar y de escalar el programa. 
Para evitar repetir un futbolista en una plantilla, se tiene que ver si en la plantilla la existe el jugador , se podría hacer con una consulta  de mysql que traiga todos los jugadores de la tabla , comprar si ya existe. Lo que pasaría si un jugador no tuviera una puntuación para una fecha es nada , solo indicaría que el jugador no jugó esa fecha. La principal diferencia entre validar datos y verificar permisos, es que validar datos se enfoca en qué información entra al sistema, mientras que verificar permisos se enfoca en quién tiene autorización para hacer una acción.


