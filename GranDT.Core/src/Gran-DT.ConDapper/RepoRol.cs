using Dapper;
using GranDT.Core.Model.IRepos;
using System.Data;
using GranDT.Core.Model;

namespace GranDT.Core.Gran_DT.ConDapper;


public class RepoRol : RepoDapper, IRepoRol
{
    public RepoRol(IDbConnection conexion) 
    : base(conexion){}

    public IEnumerable<Rol> ObtenerRoles()
    {
        var consulta = @"SELECT * FROM Roles";

        var roles = _conexion.Query<Rol>(consulta);

        return roles;
    }

    public Rol? ObtenerRolPorId(byte id)
    {
        var consulta = @"SELECT * FROM Roles WHERE IdRol = @Id";

        var rol = _conexion.QuerySingleOrDefault<Rol>(consulta, new { Id = id });

        return rol;
    }

    public void AgregarRol(Rol rol)
    {
        var parametros = new DynamicParameters();
        parametros.Add("unIdRol", direction: ParameterDirection.Output);
        parametros.Add("unNombre", rol.Nombre);

        _conexion.Execute("insertarRol", parametros, commandType: CommandType.StoredProcedure);
         rol.IdRol = parametros.Get<byte>("unIdRol");
    }

    public void ActualizarRol(Rol rol)
    {
        var parametros = new DynamicParameters();
        parametros.Add("unIdRol", rol.IdRol);
        parametros.Add("unNombre", rol.Nombre);

        _conexion.Execute("actualizarRol", parametros, commandType: CommandType.StoredProcedure);
    }

    public void EliminarRol(byte id)
    {
        var parametros = new DynamicParameters();
        parametros.Add("unIdRol", id);

        _conexion.Execute("eliminarRol", parametros, commandType: CommandType.StoredProcedure);
    }
}