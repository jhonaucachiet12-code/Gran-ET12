//Jugador es un Jugador de futbol, tiene un nombre, apellido, apodo, fecha de nacimiento, cotización, posición y equipo al que pertenece.

using GranDT.Core.Model;
using GranDT.Core.Model.IRepos;
using Dapper;
using System.Data;
namespace GranDT.Core.Gran_DT.ConDapper;

public class RepoJugador :RepoDapper, IRepoJugador
{
    public RepoJugador(IDbConnection conexion) 
    : base(conexion){}

    public IEnumerable<Jugador> ObtenerJugadores()
    {
        var consulta = @"SELECT J.*, P.nombre , R.nombre 
                        FROM Jugadores J
                        INNER JOIN Posiciones P ON J.IdPosicion = P.IdPosicion
                        INNER JOIN Equipo E ON E.idEquipo = J.IdEquipo";

        var jugadores = _conexion.Query<Jugador, Posicion, Equipo, Jugador>(consulta, (jugador, posicion, equipo) =>
        {
            jugador.posicion = posicion;
            jugador.equipo = equipo;
            return jugador;
        }, splitOn: "IdPosicion,IdEquipo");

        return jugadores;
    }

    public Jugador? ObtenerJugadorPorId(short id)
    {
        var consulta = @"SELECT J.*, P.nombre , R.nombre 
                        FROM Jugadores J
                        INNER JOIN Posiciones P ON J.idPosicion = P.IdPosicion
                        INNER JOIN Equipo E ON E.idEquipo = J.IdEquipo
                        WHERE J.idJugador = @Id";
        var Jugadores = _conexion.Query<Jugador, Posicion, Equipo, Jugador>(
        consulta,
        (jugador, posicion, equipo) =>
        {
            jugador.posicion = posicion;
            jugador.equipo = equipo;
            return jugador;
        }, new { Id = id }, splitOn: "IdPosicion,IdEquipo");
        
        return Jugadores.FirstOrDefault();
    }

    public void AgregarJugador(Jugador jugador)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@unIdJugador", direction: ParameterDirection.Output);
        parametros.Add("@unIdPosicion", jugador.IdPosicion);
        parametros.Add("@unEquipo", jugador.IdEquipo);
        parametros.Add("@unNombre", jugador.Nombre);
        parametros.Add("@unApellido", jugador.Apellido);
        parametros.Add("@unApodo", jugador.Apodo);
        parametros.Add("@unNacimiento", jugador.Nacimiento);
        parametros.Add("@unCotizacion", jugador.Cotización);
        

        _conexion.Execute("insertarJugador", parametros, commandType: CommandType.StoredProcedure);
        jugador.IdJugador = parametros.Get<short>("@IdJugador");
    }

    public void ActualizarJugador(Jugador jugador)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@unIdJugador", jugador.IdJugador);
        parametros.Add("@unIdPosicion", jugador.IdPosicion);
        parametros.Add("@unIdEquipo", jugador.IdEquipo);
        parametros.Add("@unNombre", jugador.Nombre);
        parametros.Add("@unApellido", jugador.Apellido);
        parametros.Add("@unApodo", jugador.Apodo);
        parametros.Add("@unNacimiento", jugador.Nacimiento);
        parametros.Add("@unCotizacion", jugador.Cotización);
        
        _conexion.Execute("actualizarJugador", parametros, commandType: System.Data.CommandType.StoredProcedure);
    }

    public void EliminarJugador(short id)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@unIdJugador", id);

        _conexion.Execute("eliminarJugador", parametros, commandType: System.Data.CommandType.StoredProcedure);
    }
}