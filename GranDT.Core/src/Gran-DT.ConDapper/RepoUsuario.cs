// repositorio de usuarios que al registrarse se en cripta con BCrypt la contraseña y se guarda en la base de datos
// , al iniciar sesión se compara la contraseña ingresada con la encriptada en la base de datos.
using GranDT.Core.Model;
using Dapper;
using GranDT.Core.Model.IRepos;
using System.Data;
using BCrypt.Net;


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
        usuario.PasswordHash = BCrypt.Net.BCrypt.HashPassword(PasswordHash);

        var parametros = new DynamicParameters();
        parametros.Add("unIdUsuario", direction: ParameterDirection.Output);
        parametros.Add("unNombre", usuario.Nombre);
        parametros.Add("unApellido", usuario.Apellido);
        parametros.Add("unEmail", usuario.Email);
        parametros.Add("unPasswordHash", usuario.PasswordHash);
        parametros.Add("unIdRol", usuario.IdRol);

        _conexion.Execute("insertarUsuario", parametros, commandType: CommandType.StoredProcedure);
         usuario.IdUsuario = parametros.Get<short>("unIdUsuario");

    }

    public void EliminarUsuario(short IdUsuario)
    {
        var parametros = new DynamicParameters();
        parametros.Add("unIdUsuario", IdUsuario);

        _conexion.Execute("eliminarUsuario", parametros, commandType: CommandType.StoredProcedure);

    }
    public void ActualizarUsuario(Usuario usuario)
    {
        var parametros = new DynamicParameters();
        parametros.Add("unIdUsuario", usuario.IdUsuario);
        parametros.Add("unNombre", usuario.Nombre);
        parametros.Add("unApellido", usuario.Apellido);
        parametros.Add("unEmail", usuario.Email);
        parametros.Add("unPasswordHash", usuario.PasswordHash);
        parametros.Add("unIdRol", usuario.IdRol);

        _conexion.Execute("actualizarUsuario", parametros, commandType: CommandType.StoredProcedure);
        
    }
}
