// repositorio para la entidad Plantilla
using GranDT.Core.Model;
using GranDT.Core.Model.IRepos;
using Dapper;
using System.Data;

namespace GranDT.Core.Gran_DT.ConDapper;

public class RepoPlantilla : RepoDapper, IRepoPlantilla
{
    public RepoPlantilla(IDbConnection conexion) 
    : base(conexion){}

    public IEnumerable<Plantilla> ObtenerPlantillas()
    {
        var consulta = @"SELECT P.*, U.*
                        FROM Plantilla P 
                        INNER JOIN Usuario U ON P.IdUsuario = U.IdUsuario";
        var plantillas = _conexion.Query<Plantilla, Usuario, Plantilla>(consulta, (plantilla, usuario) =>
        {
            plantilla.usuario = usuario;
            return plantilla;
        }, 
        splitOn: "IdUsuario");
        return plantillas;
    }

    public Plantilla? ObtenerPlantillaPorId(int IdPlantilla)
    {
        var consulta = @"SELECT P.*, U.*
                        FROM Plantilla P
                        INNER JOIN Usuario U ON P.IdUsuario = U.IdUsuario
                        WHERE P.IdPlantilla = @IdPlantilla";
        var plantillas = _conexion.Query<Plantilla, Usuario, Plantilla>(consulta, (plantilla, usuario) =>
        {
            plantilla.usuario = usuario;
            return plantilla;
        }, new { IdPlantilla = IdPlantilla }, 
        splitOn: "IdUsuario");
        return plantillas.FirstOrDefault();
    }

    public void AgregarPlantilla(Plantilla plantilla)
    {
        var parametros = new DynamicParameters();
        parametros.Add("unIdPlantilla", direction: ParameterDirection.Output);
        parametros.Add("unIdUsuario", plantilla.IdUsuario);
        parametros.Add("unNombre", plantilla.Nombre);
        parametros.Add("unPresupuesto", plantilla.Presupuesto);
        parametros.Add("unaCantidadJugadores", plantilla.CantidadJugadores);

        _conexion.Execute("insertarPlantilla", parametros, commandType: CommandType.StoredProcedure);

        plantilla.IdPlantilla = (int)parametros.Get<uint>("unIdPlantilla");
        
    }

    public void ActualizarPlantilla(Plantilla plantilla)
    {
        var parametros = new DynamicParameters();
        parametros.Add("unIdPlantilla", plantilla.IdPlantilla);
        parametros.Add("unIdUsuario", plantilla.IdUsuario);
        parametros.Add("unNombre", plantilla.Nombre);
        parametros.Add("unPresupuesto", plantilla.Presupuesto);
        parametros.Add("unaCantidadJugadores", plantilla.CantidadJugadores);

        _conexion.Execute("actualizarPlantilla", parametros, commandType: CommandType.StoredProcedure);
    }

    public void EliminarPlantilla(int IdPlantilla)
    {
        var parametros = new DynamicParameters();
        parametros.Add("unIdPlantilla", IdPlantilla);

        _conexion.Execute("eliminarPlantilla", parametros, commandType: CommandType.StoredProcedure);
    }

    public void AgregarJugadorAPlantilla(int idPlantilla, short idJugador, bool esTitular)
    {
        var parametros = new DynamicParameters();
        parametros.Add("unIdPlantilla", idPlantilla);
        parametros.Add("unIdJugador", idJugador);
        parametros.Add("unTitular", esTitular);

        _conexion.Execute("agregarJugadorAPlantilla", parametros, commandType: CommandType.StoredProcedure);
    }

    public void EliminarJugadorDePlantilla(short idJugador, int idPlantilla)
    {
        var parametros = new DynamicParameters();
        parametros.Add("unIdPlantilla", idPlantilla);
        parametros.Add("unIdJugador", idJugador);

        _conexion.Execute("eliminarJugadorDePlantilla", parametros, commandType: CommandType.StoredProcedure);
    }
    

    public void ActualizarJugadorEnPlantilla(int idPlantilla, short idJugador, bool esTitular)
    {
        var parametros = new DynamicParameters();
        parametros.Add("unIdPlantilla", idPlantilla);
        parametros.Add("unIdJugador", idJugador);
        parametros.Add("unTitular", esTitular);

        _conexion.Execute("actualizarTitularidadJugador", parametros, commandType: CommandType.StoredProcedure);
    }

    public Plantilla? ObtenerJugadoresDeLaPlantilla(int idPlantilla)
    {
        var consulta = @"SELECT * FROM Plantilla WHERE IdPlantilla = @IdPlantilla;
        
                        SELECT J.*, P.nombre AS PosicionNombre, E.nombre AS EquipoNombre
                        FROM Jugador J
                        INNER JOIN Posicion P ON J.IdPosicion = P.IdPosicion
                        INNER JOIN Equipo E ON J.IdEquipo = E.idEquipo
                        INNER JOIN PlantillaJugadores PJ ON J.idJugador = PJ.idJugador
                        WHERE PJ.idPlantilla = @IdPlantilla AND PJ.titulares = TRUE;

                        SELECT J.*, P.nombre AS PosicionNombre, E.nombre AS EquipoNombre
                        FROM Jugador J
                        INNER JOIN Posicion P ON J.IdPosicion = P.IdPosicion
                        INNER JOIN Equipo E ON J.IdEquipo = E.idEquipo
                        INNER JOIN PlantillaJugadores PJ ON J.idJugador = PJ.idJugador
                        WHERE PJ.idPlantilla = @IdPlantilla AND PJ.titulares = FALSE;";

        using (var multi = _conexion.QueryMultiple(consulta, new { IdPlantilla = idPlantilla }))
        {
            var plantilla = multi.Read<Plantilla>().FirstOrDefault();
            if (plantilla != null)
            {
                plantilla.JugadoresTitulares = multi.Read<Jugador>().ToList();
                plantilla.JugadoresSuplentes = multi.Read<Jugador>().ToList();
            }
            return plantilla;
        }
    }

    public decimal ObtenersumaDeLasPuntuacionesDeLosTitularesDeLaPlantilla(int idPlantilla, byte fecha)
    {
        var consulta = @"SELECT SUM(U.Puntuacion) AS PuntuacionPromedio
                        FROM PlantillaJugadores L
                        INNER JOIN Puntuacion U ON L.idJugador = U.idJugador
                        WHERE L.idPlantilla = @idPlantilla AND U.Fecha = @fecha AND L.titulares = TRUE;";

        return _conexion.ExecuteScalar<decimal>(consulta, new { idPlantilla, fecha });
    }

    public decimal ObtenerElValorTotalDeLaPlantilla(int idPlantilla)
    {
        var consulta =@"SELECT 
                        SUM(j.cotización) AS valorTotalPlantilla
                        FROM Plantilla p
                        INNER JOIN PlantillaJugadores pj ON pj.idPlantilla = p.idPlantilla
                        INNER JOIN Jugador j ON j.idJugador = pj.idJugador
                        WHERE p.idPlantilla = @idPlantilla  ;";
        return _conexion.ExecuteScalar<decimal>(consulta,new{idPlantilla});
    }
  
}
//dotnet build GranDT.Core/GranDT.Core.csproj