
using GranDT.Core.Model;
using GranDT.Core.Model.IRepos;

namespace GranDT.Core.src.Services;

public class PuntuacionService
{
    private readonly IRepoPuntuacion repoPuntuacion;
    private const decimal PuntuacionMinima = 0.00m;
    private const decimal PuntuacionMaxima = 10.00m;

    public PuntuacionService(IRepoPuntuacion repoPuntuacion)
    {
        this.repoPuntuacion = repoPuntuacion ?? throw new ArgumentNullException(nameof(repoPuntuacion));
    }

    public IEnumerable<Puntuacion> ObtenerPuntuasiones()
    {
        return repoPuntuacion.ObtenerPuntuasiones();
    }

    public Puntuacion? ObtenerLaPuntucionDelJugador(short idJugador, byte fecha)
    {
        ValidarIdJugador(idJugador);
        ValidarFecha(fecha);
        return repoPuntuacion.ObtenerLaPuntucionDelJugador(idJugador, fecha);
    }

    public IEnumerable<Puntuacion> ObtenerTodasLasPuntuasionesDelJugador(short idJugador)
    {
        ValidarIdJugador(idJugador);
        return repoPuntuacion.ObtenerTodasLasPuntuasionesDelJugador(idJugador);
    }

    public void AgregarPuntuacion(Puntuacion puntuacion)
    {
        ValidarPuntuacion(puntuacion);

        if (repoPuntuacion.ObtenerLaPuntucionDelJugador(puntuacion.IdJugador, puntuacion.Fecha) is not null)
        {
            throw new InvalidOperationException("Ya existe una puntuación para ese jugador y esa fecha.");
        }

        repoPuntuacion.AgregarPuntuacion(puntuacion);
    }

    public void ActualizarPuntuacion(Puntuacion puntuacion)
    {
        ValidarPuntuacion(puntuacion);

        if (repoPuntuacion.ObtenerLaPuntucionDelJugador(puntuacion.IdJugador, puntuacion.Fecha) is null)
        {
            throw new KeyNotFoundException("No existe una puntuación para ese jugador y esa fecha.");
        }

        repoPuntuacion.ActualizarPuntuacion(puntuacion);
    }

    public void EliminarPuntuacion(short idJugador)
    {
        ValidarIdJugador(idJugador);
        repoPuntuacion.EliminarPuntuacion(idJugador);
    }

    private static void ValidarPuntuacion(Puntuacion puntuacion)
    {
        ArgumentNullException.ThrowIfNull(puntuacion);
        ValidarIdJugador(puntuacion.IdJugador);

        ValidarFecha(puntuacion.Fecha);

        if (puntuacion.Puntuaciones < PuntuacionMinima || puntuacion.Puntuaciones > PuntuacionMaxima)
        {
            throw new ArgumentOutOfRangeException(nameof(puntuacion.Puntuaciones),
            $"La puntuación debe estar entre {PuntuacionMinima} y {PuntuacionMaxima}.");
        }

        if (decimal.Round(puntuacion.Puntuaciones, 2) != puntuacion.Puntuaciones)
        {
            throw new ArgumentOutOfRangeException(nameof(puntuacion.Puntuaciones),
                "La puntuación no puede tener más de dos decimales.");
        }
    }

    private static void ValidarIdJugador(short idJugador)
    {
        if (idJugador <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(idJugador), "El identificador del jugador debe ser mayor que cero.");
        }
    }

    private static void ValidarFecha(byte fecha)
    {
        if (fecha > 50)
        {
            throw new ArgumentOutOfRangeException(nameof(fecha), "La fecha debe estar entre 0 y 50.");
        }
    }

    



}