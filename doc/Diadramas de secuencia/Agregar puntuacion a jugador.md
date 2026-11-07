# Agregar una puntuación a un jugador

```mermaid
sequenceDiagram
    actor Administrador
    participant API as PuntuacionController
    participant Servicio as PuntuacionService
    participant RepoJugador
    participant RepoPuntuacion
    participant BD as Base de datos

    Administrador->>API: POST /api/Puntuacion/{idJugador}/{Fecha}<br/>Puntuaciones
    API->>API: Crear objeto Puntuacion
    API->>Servicio: AgregarPuntuacion(puntuacion)
    Servicio->>Servicio: Validar id, fecha y puntuación<br/>(0 a 10, máximo 2 decimales)

    alt Datos inválidos
        Servicio-->>API: ArgumentException
        API-->>Administrador: 400 Bad Request
    else Datos válidos
        Servicio->>RepoJugador: ObtenerJugadorPorId(idJugador)
        RepoJugador->>BD: Consultar jugador
        BD-->>RepoJugador: Jugador o no encontrado
        RepoJugador-->>Servicio: Resultado de consulta

        alt El jugador no existe
            Servicio-->>API: KeyNotFoundException
            API-->>Administrador: 404 Not Found
        else El jugador existe
            Servicio->>RepoPuntuacion: ObtenerLaPuntucionDelJugador(idJugador, Fecha)
            RepoPuntuacion->>BD: Buscar puntuación del jugador para la fecha
            BD-->>RepoPuntuacion: Puntuación existente o ninguna
            RepoPuntuacion-->>Servicio: Resultado de consulta

            alt Ya existe una puntuación para esa fecha
                Servicio-->>API: InvalidOperationException
                API-->>Administrador: 409 Conflict
            else Todavía no existe
                Servicio->>RepoPuntuacion: AgregarPuntuacion(puntuacion)
                RepoPuntuacion->>BD: Ejecutar insertarPuntuacion
                BD->>BD: INSERT en Puntuacion<br/>(idJugador, Fecha, puntuacion)
                BD-->>RepoPuntuacion: Inserción completada
                RepoPuntuacion-->>Servicio: Operación completada
                Servicio-->>API: Operación completada
                API-->>Administrador: 201 Created<br/>puntuación creada
            end
        end
    end
```
