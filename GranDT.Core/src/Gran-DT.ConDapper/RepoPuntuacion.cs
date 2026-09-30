// repositorio de puntuaciones
using GranDT.Core.Model;
using Dapper;
using GranDT.Core.Model.IRepos;
using System.Data;

namespace GranDT.Core.Gran_DT.ConDapper;

public class RepoPuntuacion : RepoDapper, IRepoPuntuacion
{
    public RepoPuntuacion(IDbConnection conexion)
        : base(conexion) { }

    public IEnumerable<Puntuacion> ObtenerPuntuasiones()
    {
        var consulta = @"SELECT P.idJugador, P.Fecha, P.puntuacion AS Puntuaciones,
                               J.idJugador AS JugadorIdJugador, J.idPosicion, J.idEquipo,
                               J.nombre, J.apellido, J.apodo, J.nacimiento, J.cotización
                        FROM Puntuacion P
                        INNER JOIN Jugador J ON P.idJugador = J.idJugador";

        return _conexion.Query<Puntuacion, Jugador, Puntuacion>(
            consulta,
            (puntuacion, jugador) =>
            {
                puntuacion.jugador = jugador;
                return puntuacion;
            },
            splitOn: "JugadorIdJugador");
    }

    public Puntuacion? ObtenerLaPuntucionDelJugador(short IdJugador, byte Fecha)
    {
        var consulta = @"SELECT P.idJugador, P.Fecha, P.puntuacion AS Puntuaciones,
                               J.idJugador AS JugadorIdJugador, J.idPosicion, J.idEquipo,
                               J.nombre, J.apellido, J.apodo, J.nacimiento, J.cotización
                        FROM Puntuacion P
                        INNER JOIN Jugador J ON P.idJugador = J.idJugador
                        WHERE P.idJugador = @IdJugador AND P.Fecha = @Fecha";

        return _conexion.Query<Puntuacion, Jugador, Puntuacion>(
            consulta,
            (puntuacion, jugador) =>
            {
                puntuacion.jugador = jugador;
                return puntuacion;
            },
            new { IdJugador, Fecha },
            splitOn: "JugadorIdJugador").FirstOrDefault();
    }

    public IEnumerable<Puntuacion> ObtenerTodasLasPuntuasionesDelJugador(short IdJugador)
    {
        var consulta = @"SELECT P.idJugador, P.Fecha, P.puntuacion AS Puntuaciones,
                               J.idJugador AS JugadorIdJugador, J.idPosicion, J.idEquipo,
                               J.nombre, J.apellido, J.apodo, J.nacimiento, J.cotización
                        FROM Puntuacion P
                        INNER JOIN Jugador J ON P.idJugador = J.idJugador
                        WHERE P.idJugador = @IdJugador";

        return _conexion.Query<Puntuacion, Jugador, Puntuacion>(
            consulta,
            (puntuacion, jugador) =>
            {
                puntuacion.jugador = jugador;
                return puntuacion;
            },
            new { IdJugador },
            splitOn: "JugadorIdJugador");
    }

    public void AgregarPuntuacion(Puntuacion puntuacion)
    {
        var parametros = new DynamicParameters();
        parametros.Add("unIdJugador", puntuacion.IdJugador);
        parametros.Add("unaFecha", puntuacion.Fecha);
        parametros.Add("unaPuntuacion", puntuacion.Puntuaciones);

        _conexion.Execute("insertarPuntuacion", parametros, commandType: CommandType.StoredProcedure);
    }

    public void ActualizarPuntuacion(Puntuacion puntuacion)
    {
        var parametros = new DynamicParameters();
        parametros.Add("unIdJugador", puntuacion.IdJugador);
        parametros.Add("unaFecha", puntuacion.Fecha);
        parametros.Add("unaPuntuacion", puntuacion.Puntuaciones);

        _conexion.Execute("actualizarPuntuacion", parametros, commandType: CommandType.StoredProcedure);
    }

    public void EliminarPuntuacion(short IdJugador)
    {
        _conexion.Execute(
            "DELETE FROM Puntuacion WHERE idJugador = @IdJugador",
            new { IdJugador });
    }
}

