namespace GranDT.Core.Model.IRepos;

public interface IRepoJugador
{
    IEnumerable<Jugador> ObtenerJugadores();
    Jugador? ObtenerJugadorPorId(short IdJugador);
    void AgregarJugador(Jugador jugador);
    void ActualizarJugador(Jugador jugador);
    void EliminarJugador(short IdJugador);
}