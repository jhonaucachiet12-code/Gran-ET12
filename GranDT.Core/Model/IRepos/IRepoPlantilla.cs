namespace GranDT.Core.Model.IRepos;

public interface IRepoPlantilla
{
    IEnumerable<Plantilla> ObtenerPlantillas();
    Plantilla ObtenerPlantillaPorId(int id);
    void AgregarPlantilla(Plantilla plantilla);
    void ActualizarPlantilla(Plantilla plantilla);
    void EliminarPlantilla(int id);
    IDictionary<Jugador, short> ObtenerJugadoresPorPlantilla(int idPlantilla);
}