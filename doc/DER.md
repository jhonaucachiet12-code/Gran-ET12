erDiagram
    Rol ||--o{ Usuario : "1 asigna a N"
    Usuario ||--o{ Plantilla : "1 posee N"
    Posicion ||--o{ Jugador : "1 clasifica a N"
    Equipo ||--o{ Jugador : "1 pertenece N"
    Jugador ||--o{ Puntuacion : "1 tiene N"
    Plantilla ||--o{ PlantillaJugadores : "1 contiene N"
    Jugador ||--o{ PlantillaJugadores : "1 pertenece N"

    Rol {
        TINYINT idRol PK
        VARCHAR nombre
    }

    Usuario {
        SMALLINT idUsuario PK
        VARCHAR nombre
        VARCHAR apellido
        VARCHAR email
        DATE fechaNacimiento
        VARCHAR contraseña
        TINYINT idRol FK
    }

    Equipo {
        TINYINT idEquipo PK
        VARCHAR nombre
    }

    Posicion {
        TINYINT idPosicion PK
        VARCHAR nombre
    }

    Jugador {
        SMALLINT idJugador PK
        TINYINT idPosicion FK
        TINYINT idEquipo FK
        VARCHAR nombre
        VARCHAR apellido
        VARCHAR apodo
        DATE nacimiento
        DECIMAL cotización
    }

    Plantilla {
        INT idPlantilla PK
        SMALLINT idUsuario FK
        VARCHAR nombre
        DECIMAL presupuesto
        TINYINT cantidadJugadores
    }

    Puntuacion {
        SMALLINT idJugador PK, FK
        TINYINT Fecha PK
        DECIMAL puntuacion
    }

    PlantillaJugadores {
        INT idPlantilla PK, FK
        SMALLINT idJugador PK, FK
        BOOLEAN titulares
    }