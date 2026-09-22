// repositorio de la entidad Equipo con dapper, permite obtener todos los equipos de la base de datos.
using GranDT.Core.Model;
using GranDT.Core.Model.IRepos;
using Dapper;
using System.Data;

namespace GranDT.Core.Gran_DT.ConDapper;

public class RepoEquipo : RepoDapper, IRepoEquipo
{
    public RepoEquipo(IDbConnection conexion) 
    : base(conexion){}

    public IEnumerable<Equipo> ObtenerEquipos()
    {
        var consulta = @"SELECT * FROM Equipo";

        var equipos = _conexion.Query<Equipo>(consulta);

        return equipos;
    }

    public Equipo? ObtenerEquipoPorId(byte IdEquipo)
    {
        var consulta = @"SELECT * FROM Equipo WHERE idEquipo = @IdEquipo";

        var equipo = _conexion.QuerySingleOrDefault<Equipo>(consulta, new { IdEquipo = IdEquipo });

        return equipo;
    }

    public void AgregarEquipo(Equipo equipo)
    {
        var parametros = new DynamicParameters();
        parametros.Add("unIdEquipo", direction: ParameterDirection.Output);
        parametros.Add("unNombre", equipo.Nombre);

        _conexion.Execute("insertarEquipo", parametros, commandType: CommandType.StoredProcedure);
         equipo.IdEquipo = parametros.Get<byte>("unIdEquipo");
    }

    public void ActualizarEquipo(Equipo equipo)
    {
        var parametros = new DynamicParameters();
        parametros.Add("unIdEquipo", equipo.IdEquipo);
        parametros.Add("unNombre", equipo.Nombre);

        _conexion.Execute("actualizarEquipo", parametros, commandType: CommandType.StoredProcedure);
    }

    public void EliminarEquipo(byte IdEquipo)
    {
        var parametros = new DynamicParameters();
        parametros.Add("unIdEquipo", IdEquipo);

        _conexion.Execute("eliminarEquipo", parametros, commandType: CommandType.StoredProcedure);
    }
}