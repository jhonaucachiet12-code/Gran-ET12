using Dapper;
using GranDT.Core.Model.IRepos;
using System.Data;
using GranDT.Core.Model;
namespace GranDT.Core.Gran_DT.ConDapper;

public class RepoUsuario: RepoDapper, IRepoUsuario
{
    public RepoUsuario(IDbConnection conexion) 
    : base(conexion){}

    public IEnumerable<Usuario> ObtenerUsuarios()
    {
        var consulta = @"SELECT J.*, R.nombre 
                        FROM Usuario U 
                        INNER JOIN Rol R ON U.idRol = R.idRol";

        var usuarios = _conexion.Query<Usuario, Rol,Usuario>(consulta, (Usuario, Rol) =>
        {
            Usuario.Roles = Rol;
            return Usuario;
        }, splitOn: "idRol");

        return usuarios;
    }

    public Usuario? ObtenerPorEmail(short IdUsuario)
    {
        var consulta = @"SELECT J.*, R.nombre 
                        FROM Jugadores J
                        INNER JOIN Rol R ON U.idRol = R.idRol
                        WHERE J.idJugador = @idUsuario";
        var usuarios = _conexion.Query<Usuario, Rol, Usuario>(
        consulta,
        (Usuario , Rol) =>
        {
            Usuario.Roles = Rol;
            return Usuario;
        }, new { idUsuario = IdUsuario }, splitOn: "idUsuario");
        
        return usuarios.FirstOrDefault();
    }

    public void RegistrarUsario(Usuario usuario, string PasswordHash)
    {

    }

    public void EliminarUsuario(short IdUsuario)
    {

    }
    public void ActualizarUsuario(Usuario usuario)
    {
        
    }
}
