namespace GranDT.Core.Model.IRepos;

public interface IRepoPlantilla
{
    IEnumerable<Plantilla> ObtenerPlantillas();
    Plantilla? ObtenerPlantillaPorId(int IdPlantilla);
    void AgregarPlantilla(Plantilla plantilla);
    void ActualizarPlantilla(Plantilla plantilla);
    void EliminarPlantilla(int IdPlantilla);

// crud de jugardorplantilla
    void AgregarJugadorAPlantilla(int idPlantilla, short idJugador, bool esTitular);
    void ActualizarJugadorEnPlantilla(int idPlantilla, short idJugador, bool esTitular);
    void EliminarJugadorDePlantilla(short idJugador, int idPlantilla);
    Plantilla? ObtenerJugadoresDeLaPlantilla(int idPlantilla);

    decimal ObtenersumaDeLasPuntuacionesDeLosTitularesDeLaPlantilla(int idPlantilla, byte fecha);

    decimal ObtenerElValorTotalDeLaPlantilla(int idPlantilla);


    /*decimal ObtenerPuntuacionPromedioDeLaPlantilla(int idPlantilla);
    IEnumerable<Jugador> ObtenerJugadoresTitularesDeLaPlantilla(int idPlantilla);
    IEnumerable<Jugador> ObtenerJugadoresSuplentesDeLaPlantilla(int idPlantilla);
    decimal ObtenerCostoTotalDeLaPlantilla(int idPlantilla);*/


    
}