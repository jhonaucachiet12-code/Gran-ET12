namespace GranDT.Core.Model.IRepos;

public interface IRepoPosicion
{
    IEnumerable<Posicion> ObtenerPosiciones();
    Posicion? ObtenerPosicionPorId(byte IdPosicion);
    void AgregarPosicion(Posicion posicion);
    void ActualizarPosicion(Posicion posicion);
    void EliminarPosicion(byte IdPosicion);
    
}