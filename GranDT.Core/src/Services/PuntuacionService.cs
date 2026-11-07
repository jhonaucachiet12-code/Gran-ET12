
using GranDT.Core.Gran_DT.ConDapper;
using GranDT.Core.Model;
using GranDT.Core.Model.IRepos;

namespace GranDT.Core.src.Services;

public class PuntuacionService
{
    private readonly IRepoJugador repoJugador;
    private readonly IRepoPuntuacion repoPuntuacion;
    private const decimal PuntuacionMinima = 0.00m;
    private const decimal PuntuacionMaxima = 10.00m;

    public PuntuacionService(IRepoPuntuacion repoPuntuacion , IRepoJugador repoJugador)
    {
        this.repoPuntuacion = repoPuntuacion ?? throw new ArgumentNullException(nameof(repoPuntuacion));
        this.repoJugador = repoJugador ?? throw new ArgumentNullException(nameof(repoJugador));
    }
    

    public IEnumerable<Puntuacion> ObtenerPuntuasiones()
    {
        return repoPuntuacion.ObtenerPuntuasiones();
    }

    public Puntuacion? ObtenerLaPuntucionDelJugador(short idJugador, byte fecha)
    {
        ValidarIdJugador(idJugador);
        ValidarExistenciaDelJugador(idJugador);
        ValidarFecha(fecha);
        ValidarPuntuacionExistente(idJugador, fecha);

        return repoPuntuacion.ObtenerLaPuntucionDelJugador(idJugador, fecha);
    }

    public IEnumerable<Puntuacion> ObtenerTodasLasPuntuasionesDelJugador(short idJugador)
    {
        ValidarIdJugador(idJugador);
        ValidarExistenciaDelJugador(idJugador);

        if (repoPuntuacion.ObtenerTodasLasPuntuasionesDelJugador(idJugador) is null)
        {
            throw new KeyNotFoundException("No existe una puntuación para ese jugador y esa fecha.");
        }

        return repoPuntuacion.ObtenerTodasLasPuntuasionesDelJugador(idJugador);
    }

    public void AgregarPuntuacion(Puntuacion puntuacion)
    {
        ValidarPuntuacion(puntuacion);
        ValidarIdJugador(puntuacion.IdJugador);
        ValidarExistenciaDelJugador(puntuacion.IdJugador);
        ValidarFecha(puntuacion.Fecha);

        if (repoPuntuacion.ObtenerLaPuntucionDelJugador(puntuacion.IdJugador, puntuacion.Fecha) is not null)
        {
            throw new InvalidOperationException("Ya existe una puntuación para ese jugador y esa fecha.");
        }

        


        repoPuntuacion.AgregarPuntuacion(puntuacion);
    }

    public void ActualizarPuntuacion(Puntuacion puntuacion)
    {
        ValidarPuntuacion(puntuacion);
        ValidarExistenciaDelJugador(puntuacion.IdJugador);
        ValidarPuntuacionExistente(puntuacion.IdJugador, puntuacion.Fecha);
        ValidarPuntuacionExistente(puntuacion.IdJugador, puntuacion.Fecha);

        repoPuntuacion.ActualizarPuntuacion(puntuacion);
    }

    public void EliminarPuntuacion(short idJugador, byte Fecha)
    {
        ValidarIdJugador(idJugador);
        ValidarExistenciaDelJugador(idJugador);
        ValidarFecha(Fecha);
        ValidarPuntuacionExistente(idJugador, Fecha);
        repoPuntuacion.EliminarPuntuacion(idJugador, Fecha);
    }

    private  void ValidarPuntuacion(Puntuacion puntuacion)
    {
        ArgumentNullException.ThrowIfNull(puntuacion);
        ValidarIdJugador(puntuacion.IdJugador);

        ValidarFecha(puntuacion.Fecha);

        if (puntuacion.Puntuaciones < PuntuacionMinima || puntuacion.Puntuaciones > PuntuacionMaxima)
        {
            throw new ArgumentException(nameof(puntuacion.Puntuaciones),
            $"La puntuación debe estar entre {PuntuacionMinima} y {PuntuacionMaxima}.");
        }

        if (decimal.Round(puntuacion.Puntuaciones, 2) != puntuacion.Puntuaciones)
        {
            throw new ArgumentException(nameof(puntuacion.Puntuaciones),
                "La puntuación no puede tener más de dos decimales.");
        }
    }

    private void ValidarIdJugador(short idJugador)
    {
        if (idJugador <= 0)
        {
            throw new ArgumentException(nameof(idJugador), "El identificador del jugador debe ser mayor que cero.");
        }
    }

    private static void ValidarFecha(byte fecha)
    {
        if (fecha < 0 || fecha > 50) 
        { 
            throw new ArgumentOutOfRangeException(nameof(fecha), "La fecha debe estar entre 0 y 50."); 
        }
    }

    private void ValidarPuntuacionExistente(short idJugador, byte fecha)
    {
        if (repoPuntuacion.ObtenerLaPuntucionDelJugador(idJugador, fecha) is null)
        {
            throw new KeyNotFoundException("No existe una puntuación para ese jugador y esa fecha.");
        }

        
        
    }

    private void ValidarExistenciaDelJugador(short idJugador)
    {
        if (repoJugador.ObtenerJugadorPorId(idJugador) is null)
        {
            throw new KeyNotFoundException("no existe un jugador con el identificador proporcionado.");
        }
    }



}