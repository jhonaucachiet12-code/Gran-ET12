namespace GranDT.Core.Model.IRepos;

public interface IRepoPuntuacion
{
    IEnumerable<Puntuacion> ObtenerPuntuasiones();
    //IEnumerable<Puntuacion> Obtenerpuntuacionesdeldia(byte Fecha);
    Puntuacion? ObtenerLaPuntucionDelJugador(short IdJugador, byte Fecha);
    IEnumerable<Puntuacion> ObtenerTodasLasPuntuasionesDelJugador(short IdJugador);
    
    void AgregarPuntuacion(Puntuacion puntuacion);
    void ActualizarPuntuacion(Puntuacion puntuacion);
    void EliminarPuntuacion(short IdJugador);

}
