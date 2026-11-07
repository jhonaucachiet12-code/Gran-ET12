//service de jugador
using GranDT.Core.Model;
using GranDT.Core.Model.IRepos;
namespace GranDT.Core.src.Services;

public class JugadorService
{
    private readonly IRepoJugador repoJugador;
    private readonly IRepoEquipo repoEquipo;
    private readonly IRepoPosicion repoPosicion;

    public JugadorService(IRepoJugador repoJugador, IRepoEquipo repoEquipo, IRepoPosicion repoPosicion)
    {
        this.repoJugador = repoJugador;
        this.repoEquipo = repoEquipo;
        this.repoPosicion = repoPosicion;
    }

    public IEnumerable<Jugador> ObtenerJugadores()
    {
        return repoJugador.ObtenerJugadores();
    }

    public Jugador? ObtenerJugadorPorId(short IdJugador)
    {
        ValidarId(IdJugador);
        validarExistenciaDeJugador(IdJugador);

        return repoJugador.ObtenerJugadorPorId(IdJugador);
    }

    public void AgregarJugador(Jugador jugador)
    {
        
        ValidarJugador(jugador);
        validarExistenciaDeEquipoPosicion(jugador.IdEquipo, jugador.IdPosicion);
        repoJugador.AgregarJugador(jugador);
    }

    public void ActualizarJugador(Jugador jugador)
    {
        ValidarJugador(jugador);
        ValidarId(jugador.IdJugador);
        validarExistenciaDeJugador(jugador.IdJugador);
        validarExistenciaDeEquipoPosicion(jugador.IdEquipo, jugador.IdPosicion);
        repoJugador.ActualizarJugador(jugador);
    }

    public void EliminarJugador(short id)
    {
        ValidarId(id);
        validarExistenciaDeJugador(id);
        repoJugador.EliminarJugador(id);
    }
    
    private static void ValidarJugador(Jugador jugador)
    {
        ArgumentNullException.ThrowIfNull(jugador);

        if (string.IsNullOrWhiteSpace(jugador.Nombre))
        {
            throw new ArgumentException("El nombre del jugador es obligatorio.", nameof(jugador));
        }

        if (string.IsNullOrWhiteSpace(jugador.Apellido))
        {
            throw new ArgumentException("El apellido del jugador es obligatorio.", nameof(jugador));
        }

        if (jugador.Nacimiento == null || jugador.Nacimiento > DateTime.Now || jugador.Nacimiento == DateTime.MinValue)
        {
            throw new ArgumentException("La fecha de nacimiento del jugador no puede ser futura y es obligatoria.", nameof(jugador));
        }

        if (jugador.IdPosicion <= 0)
        {
            throw new ArgumentException("El jugador debe tener una posición válida.", nameof(jugador));
        }
        if (jugador.IdEquipo < 0)
        {
            throw new ArgumentException("El jugador debe pertenecer a un equipo válido.", nameof(jugador));
        }
        if(jugador.Cotización < 0 || jugador.Cotización > 99999999 )
        {
            throw new ArgumentException("La cotización del jugador no puede ser negativa o mayor a 99.999.999.", nameof(jugador));
        }
        
    }

    private static void ValidarId(short id)
    {
        if (id < 0)
        {
            throw new ArgumentException(nameof(id), "El identificador debe ser mayor que cero.");
        }
    }

    private void validarExistenciaDeEquipoPosicion(byte IdEquipo, byte IdPosicion)
    {
        if (IdPosicion < 0)
        {
            throw new ArgumentException("El jugador debe tener una posición válida.", nameof(IdPosicion));
        }
        if (IdEquipo < 0)
        {
            throw new ArgumentException("El jugador debe pertenecer a un equipo válido.", nameof(IdEquipo));
        }

        if(repoEquipo.ObtenerEquipoPorId(IdEquipo) is null)
        {
            throw new KeyNotFoundException("No existe un equipo con ese identificador.");
        }
        if(repoPosicion.ObtenerPosicionPorId(IdPosicion) is null)
        {
            throw new KeyNotFoundException("No existe un posicion con ese identificador.");
        }
    }

    private void validarExistenciaDeJugador(short idJugador)
    {
        if(repoJugador.ObtenerJugadorPorId(idJugador) is null)
        {
            throw new KeyNotFoundException("No existe un jugador con ese identificador.");
        }
    }
}