//service de jugador
using GranDT.Core.Model;
using GranDT.Core.Model.IRepos;
namespace GranDT.Core.src.Services;

public class JugadorService
{
    private readonly IRepoJugador repoJugador;

    public JugadorService(IRepoJugador repoJugador)
    {
        this.repoJugador = repoJugador;
    }

    public IEnumerable<Jugador> ObtenerJugadores()
    {
        return repoJugador.ObtenerJugadores();
    }

    public Jugador? ObtenerJugadorPorId(short IdJugador)
    {
        ValidarId(IdJugador);

        return repoJugador.ObtenerJugadorPorId(IdJugador);
    }

    public void AgregarJugador(Jugador jugador)
    {
        
        ValidarJugador(jugador);
        repoJugador.AgregarJugador(jugador);
    }

    public void ActualizarJugador(Jugador jugador)
    {
        ValidarJugador(jugador);
        ValidarId(jugador.IdJugador);
        repoJugador.ActualizarJugador(jugador);
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

        if (jugador.Nacimiento > DateTime.Now)
        {
            throw new ArgumentException("La fecha de nacimiento del jugador no puede ser futura.", nameof(jugador));
        }

        if (jugador.IdPosicion <= 0)
        {
            throw new ArgumentException("El jugador debe tener una posición válida.", nameof(jugador));
        }
        if (jugador.IdEquipo <= 0)
        {
            throw new ArgumentException("El jugador debe pertenecer a un equipo válido.", nameof(jugador));
        }
        if(jugador.Cotización < 0)
        {
            throw new ArgumentException("La cotización del jugador no puede ser negativa.", nameof(jugador));
        }
        
    }

    private static void ValidarId(short id)
    {
        if (id <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(id), "El identificador debe ser mayor que cero.");
        }
    }
}