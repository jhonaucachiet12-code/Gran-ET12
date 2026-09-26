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
        var consulta = @"SELECT * FROM Plantillas";
        return _conexion.Query<Plantilla>(consulta);
    }

    public Plantilla? ObtenerPlantillaPorId(int IdPlantilla)
    {
        var consulta = @"SELECT * FROM Plantillas WHERE IdPlantilla = @IdPlantilla";
        return _conexion.QueryFirstOrDefault<Plantilla>(consulta, new { IdPlantilla });
    }

    public void AgregarPlantilla(Plantilla plantilla)
    {
        var parametros = new DynamicParameters();
        parametros.Add("unIdPlantilla", direction: ParameterDirection.Output);
        parametros.Add("unIdUsuario", plantilla.IdUsuario);
        parametros.Add("unNombre", plantilla.Nombre);
        parametros.Add("unPresupuesto", plantilla.Presupuesto);
        parametros.Add("unCantidadJugadores", plantilla.CantidadJugadores);

        _conexion.Execute("AgregarPlantilla", parametros, commandType: CommandType.StoredProcedure);

        plantilla.IdPlantilla = parametros.Get<int>("unIdPlantilla");
        
    }

    public void ActualizarPlantilla(Plantilla plantilla)
    {
        var parametros = new DynamicParameters();
        parametros.Add("unIdPlantilla", plantilla.IdPlantilla);
        parametros.Add("unIdUsuario", plantilla.IdUsuario);
        parametros.Add("unNombre", plantilla.Nombre);
        parametros.Add("unPresupuesto", plantilla.Presupuesto);
        parametros.Add("unCantidadJugadores", plantilla.CantidadJugadores);

        _conexion.Execute("ActualizarPlantilla", parametros, commandType: CommandType.StoredProcedure);
    }

    public void EliminarPlantilla(int IdPlantilla)
    {
        var parametros = new DynamicParameters();
        parametros.Add("unIdPlantilla", IdPlantilla);

        _conexion.Execute("EliminarPlantilla", parametros, commandType: CommandType.StoredProcedure);
    }

    public void AgregarJugadorAPlantilla(int idPlantilla, int idJugador, bool esTitular)
    {
        var parametros = new DynamicParameters();
        parametros.Add("unIdPlantilla", idPlantilla);
        parametros.Add("unIdJugador", idJugador);
        parametros.Add("esTitular", esTitular);

        _conexion.Execute("AgregarJugadorAPlantilla", parametros, commandType: CommandType.StoredProcedure);
    }

    public void EliminarJugadorDePlantilla(short idJugador, int idPlantilla)
    {
        var parametros = new DynamicParameters();
        parametros.Add("unIdPlantilla", idPlantilla);
        parametros.Add("unIdJugador", idJugador);

        _conexion.Execute("EliminarJugadorDePlantilla", parametros, commandType: CommandType.StoredProcedure);
    }
    

    public void ActualizarJugadorEnPlantilla(int idPlantilla, int idJugador, bool esTitular)
    {
        var parametros = new DynamicParameters();
        parametros.Add("unIdPlantilla", idPlantilla);
        parametros.Add("unIdJugador", idJugador);
        parametros.Add("esTitular", esTitular);

        _conexion.Execute("ActualizarJugadorEnPlantilla", parametros, commandType: CommandType.StoredProcedure);
    }

    public Plantilla ObtenerJugadoresDeLaPlantilla(int idPlantilla)
    {
        var consulta = @"SELECT * FROM Plantillas WHERE IdPlantilla = @IdPlantilla;
        
                        SELECT J.*, P.nombre AS PosicionNombre, E.nombre AS EquipoNombre
                        FROM Jugadores J
                        INNER JOIN Posiciones P ON J.IdPosicion = P.IdPosicion
                        INNER JOIN Equipo E ON J.IdEquipo = E.idEquipo
                        INNER JOIN PlantillaJugadores PJ ON J.idJugador = PJ.idJugador
                        WHERE PJ.idPlantilla = @IdPlantilla AND PJ.titulares = TRUE;

                        SELECT J.*, P.nombre AS PosicionNombre, E.nombre AS EquipoNombre
                        FROM Jugadores J
                        INNER JOIN Posiciones P ON J.IdPosicion = P.IdPosicion
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
  
}