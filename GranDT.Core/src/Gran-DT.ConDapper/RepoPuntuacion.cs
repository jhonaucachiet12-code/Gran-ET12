// repositorio de puntuaciones 
using GranDT.Core.Model;
using Dapper;
using GranDT.Core.Model.IRepos;
using System.Data;
namespace GranDT.Core.Gran_DT.ConDapper;

public class RepoPuntuacion : RepoDapper, IRepoPuntuacion
{
    public RepoPuntuacion(IDbConnection conexion) 
    : base(conexion){}

    public IEnumerable<Puntuacion> ObtenerPuntuasiones()
    {
        var consulta = @"SELECT * FROM Puntuacion";

        var puntuaciones = _conexion.Query<Puntuacion>(consulta);

        return puntuaciones;
    }

    public Puntuacion? ObtenerLaPuntucionDelJugador(short IdJugador, byte Fecha)
    {
        var consulta = @"SELECT * FROM Puntuacion WHERE idJugador = @IdJugador AND fecha = @Fecha";

        var puntuacion = _conexion.QuerySingleOrDefault<Puntuacion>(consulta, new { IdJugador = IdJugador, Fecha = Fecha });

        return puntuacion;
    }

    public IEnumerable<Puntuacion> ObtenerTodasLasPuntuasionesDelJugador(short IdJugador)
    {
        var consulta = @"SELECT * FROM Puntuacion WHERE idJugador = @IdJugador";

        var puntuaciones = _conexion.Query<Puntuacion>(consulta, new { IdJugador = IdJugador });

        return puntuaciones;
    }

    public void AgregarPuntuacion(Puntuacion puntuacion)
    {
        var parametros = new DynamicParameters();
        parametros.Add("unIdJugador", direction: ParameterDirection.Output);
        parametros.Add("unaFecha", puntuacion.Fecha);
        parametros.Add("unaPuntuacion", puntuacion.Puntuaciones);

        _conexion.Execute("insertarPuntuacion", parametros, commandType: CommandType.StoredProcedure);
         puntuacion.IdJugador = parametros.Get<short>("unIdJugador");
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
        var parametros = new DynamicParameters();
        parametros.Add("unIdJugador", IdJugador);

        _conexion.Execute("eliminarPuntuacion", parametros, commandType: CommandType.StoredProcedure);
    }
}

