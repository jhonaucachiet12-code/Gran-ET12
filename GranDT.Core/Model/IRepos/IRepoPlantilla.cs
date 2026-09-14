namespace GranDT.Core.Model.IRepos;

public interface IRepoPlantilla
{
    IEnumerable<Plantilla> ObtenerPlantillas();
    Plantilla? ObtenerPlantillaPorId(int IdPlantilla);
    void AgregarPlantilla(Plantilla plantilla);
    void ActualizarPlantilla(Plantilla plantilla);
    void EliminarPlantilla(int IdPlantilla);
    IDictionary<Jugador, short> ObtenerJugadoresPorPlantilla(int idPlantilla);
}