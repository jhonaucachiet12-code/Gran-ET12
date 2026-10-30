// service de posicion
using GranDT.Core.Model;
using GranDT.Core.Model.IRepos;

namespace GranDT.Core.src.Services;
public class PosicionService
{
    private readonly IRepoPosicion repoPosicion;

    public PosicionService(IRepoPosicion repoPosicion)
    {
        this.repoPosicion = repoPosicion;
    }

    public IEnumerable<Posicion> ObtenerPosiciones()
    {
        return repoPosicion.ObtenerPosiciones();
    }

    public Posicion? ObtenerPosicionPorId(byte IdPosicion)
    {
        ValidarId(IdPosicion);

        if(repoPosicion.ObtenerPosicionPorId(IdPosicion) is null)
        {
            throw new KeyNotFoundException("No existe la posicion con ese identificador.");
        }

        return repoPosicion.ObtenerPosicionPorId(IdPosicion);
    }

    public void AgregarPosicion(Posicion posicion)
    {

        ValidarPosicion(posicion);
        var posicionExistentes = repoPosicion.ObtenerPosiciones();
        if(posicionExistentes.Any(P => P.Nombre == posicion.Nombre))
		{
			throw new InvalidOperationException("la posicion ya exite");
		}
        repoPosicion.AgregarPosicion(posicion);
    }

    public void ActualizarPosicion(Posicion posicion)
    {
        ValidarPosicion(posicion);
        ValidarId(posicion.IdPosicion);
        var posicionExistentes = repoPosicion.ObtenerPosiciones();
        if(posicionExistentes.Any(P => P.Nombre == posicion.Nombre))
		{
			throw new InvalidOperationException("la posicion ya exite");
		}
        if(repoPosicion.ObtenerPosicionPorId(posicion.IdPosicion) is null)
        {
            throw new KeyNotFoundException("No existe la posicion con ese identificador.");
        }
        repoPosicion.ActualizarPosicion(posicion);
        
    }

    public void EliminarPosicion(byte id)
    {
        ValidarId(id);
        if(repoPosicion.ObtenerPosicionPorId(id) is null)
        {
            throw new KeyNotFoundException("No existe la posicion con ese identificador.");
        }
        repoPosicion.EliminarPosicion(id);
    }
    private static void ValidarPosicion(Posicion posicion)
    {
        ArgumentNullException.ThrowIfNull(posicion);

        if (string.IsNullOrWhiteSpace(posicion.Nombre))
        {
            throw new ArgumentException("El nombre de la posición es obligatorio.", nameof(posicion));
        }
        
    }

    private static void ValidarId(byte id)
    {
        if (id <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(id), "El identificador debe ser mayor que cero.");
        }
    }
}