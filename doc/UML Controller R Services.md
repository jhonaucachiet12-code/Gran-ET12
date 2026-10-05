# Diagramas de clase de la Relacion entre los Controllers y los Services

Digrama de clase del del sistema Gran_ET12, con solo las relaciones entre Las Controllers y los services


```mermaid
classDiagram
    direction LR
    class EquipoController {
        +ObtenerEquipos() IEnumerable~Equipo~
        +ObtenerEquipoPorId(byte id) Equipo
        +AgregarEquipo(RegistroEquipoRequest request)
        +ActualizarEquipo(Equipo equipo)
        +EliminarEquipo(byte id)
        
    }

    class JugadorController {
        +ObtenerJugadores() IEnumerable~Jugador~
        +ObtenerJugadorPorId(short id) Jugador
        +AgregarJugador(RegistroJugadorRequest request)
        +ActualizarJugador(Jugador jugador)
        +EliminarJugador(short id)
    }

    class PlantillaController {
        +ObtenerPlantillas() IEnumerable~Plantilla~
        +ObtenerPlantillaPorId(byte id) Plantilla
        +AgregarPlantilla(RegistroPlantillaRequest request)
        +ActualizarPlantilla(Plantilla plantilla)
        +EliminarPlantilla(byte id)
        +AgregarJugadorAPlantilla(AgregarJugadorPlantillaRequest request)
        +ActualizarJugadorEnPlantilla(AgregarJugadorPlantillaRequest request)
        +EliminarJugadorDePlantilla(short idJugador, int idPlantilla)
        +ObtenerJugadoresDeLaPlantilla(int idPlantilla) Plantilla
        +ObtenersumaDeLasPuntuacionesDeLosTitularesDeLaPlantilla(int idPlantilla, byte fecha) decimal
        +ObtenerElValorTotalDeLaPlantilla(int idPlantilla) decimal
    }

    class PosicionController {
        +ObtenerPosiciones() IEnumerable~Posicion~
        +ObtenerPosicionPorId(byte id) Posicion
        +AgregarPosicion(RegistroPosicionRequest request)
        +ActualizarPosicion(Posicion posicion)
        +EliminarPosicion(byte id)
    }

    class PuntuacionController {
        +ObtenerPuntuasiones() IEnumerable~Puntuacion~
        +ObtenerLaPuntucionDelJugador(short idJugador, byte fecha) Puntuacion
        +ObtenerTodasLasPuntuasionesDelJugador(short idJugador) IEnumerable~Puntuacion~
        +AgregarPuntuacion(RegistroPuntuacionesRequest request)
        +ActualizarPuntuacion(Puntuacion puntuacion)
        +EliminarPuntuacion(short idJugador)
    }

    class RolController {
        +ObtenerRoles() 
        +ObtenerRolPorId(byte id) 
        +AgregarRol(RegistroRolRequest request)
        +ActualizarRol(Rol rol)
        +EliminarRol(byte id)
    }

    class UsuarioController {
        +ObtenerUsuarios()
        +ObtenerPorEmail(short id)
        +RegistrarUsario(RegistroUsuarioRequest request)
        +ActualizarUsuario(short id, Usuario usuario)
        +EliminarUsuario(short id)
    }

    class EquipoService {
        +ObtenerEquipos() IEnumerable~Equipo~
        +ObtenerEquipoPorId(byte idEquipo) Equipo
        +AgregarEquipo(Equipo equipo)
        +ActualizarEquipo(Equipo equipo)
        +EliminarEquipo(byte idEquipo)
    }

    class JugadorService {
        +ObtenerJugadores() IEnumerable~Jugador~
        +ObtenerJugadorPorId(short id) Jugador
        +AgregarJugador(Jugador jugador)
        +ActualizarJugador(Jugador jugador)
    }

    class PlantillaService {
        +ObtenerPlantillas() IEnumerable~Plantilla~
        +ObtenerPlantillaPorId(int id) Plantilla
        +AgregarPlantilla(Plantilla plantilla)
        +ActualizarPlantilla(Plantilla plantilla)
        +EliminarPlantilla(int id)
        +AgregarJugadorAPlantilla(int idPlantilla, short idJugador, bool esTitular)
        +ActualizarJugadorEnPlantilla(int idPlantilla, short idJugador, bool esTitular)
        +EliminarJugadorDePlantilla(short idJugador, int idPlantilla)
        +ObtenerJugadoresDeLaPlantilla(int idPlantilla) Plantilla
        +ObtenersumaDeLasPuntuacionesDeLosTitularesDeLaPlantilla(int idPlantilla, byte fecha) decimal
        +ObtenerElValorTotalDeLaPlantilla(int idPlantilla) decimal
    }

    class PosicionService {
        +ObtenerPosiciones() IEnumerable~Posicion~
        +ObtenerPosicionPorId(byte id) Posicion
        +AgregarPosicion(Posicion posicion)
        +ActualizarPosicion(Posicion posicion)
    }

    class PuntuacionService {
        +ObtenerPuntuasiones() IEnumerable~Puntuacion~
        +ObtenerLaPuntucionDelJugador(short idJugador, byte fecha) Puntuacion
        +ObtenerTodasLasPuntuasionesDelJugador(short idJugador) IEnumerable~Puntuacion~
        +AgregarPuntuacion(Puntuacion puntuacion)
        +ActualizarPuntuacion(Puntuacion puntuacion)
        +EliminarPuntuacion(short idJugador)
    }

    class RolService {
        +ObtenerRoles() IEnumerable~Rol~
        +ObtenerRolPorId(byte id) Rol
        +AgregarRol(Rol rol)
        +ActualizarRol(Rol rol)
    }

    class UsuarioService {
        +ObtenerUsuarios() IEnumerable~Usuario~
        +ObtenerPorEmail(short id) Usuario
        +RegistrarUsario(Usuario usuario, string PasswordHash)
        +ActualizarUsuario(Usuario usuario)
        +EliminarUsuario(short id)
    }

    class RegistroPlantillaRequest {
        +int IdUsuario
        +string Nombre
    }

    class AgregarJugadorPlantillaRequest {
        +int IdPlantilla
        +short IdJugador
        +bool EsTitular
    }

    class RegistroEquipoRequest{
        <<Record>>
        + string Nombre
    }

    class RegistroRolRequest
    {
        <<Record>>
        + string Nombre
    }

    class RegistroPuntuacionesRequest
    {
        <<Record>>
        + string Nombre
    }

    class RegistroPuntuacionesRequest
    {
        <<Record>>
        + string Nombre
    }

    class RegistroPuntuacionesRequest
    {
        <<Record>>
        short IdJugador
        byte Fecha 
        decimal Puntuaciones
    }

    class RegistroJugadorRequest
    { 
        <<Record>>
        byte IdPosicion
        byte IdEquipo
        string Nombre
        string Apellido
        string Apodo
        DateTime Nacimiento
        decimal Cotización
    }


    


    EquipoController --> EquipoService
    JugadorController --> JugadorService
    PlantillaController --> PlantillaService
    PlantillaController ..> RegistroPlantillaRequest
    PlantillaController ..> AgregarJugadorPlantillaRequest
    PosicionController --> PosicionService
    PuntuacionController --> PuntuacionService
    RolController --> RolService
    UsuarioController --> UsuarioService

```