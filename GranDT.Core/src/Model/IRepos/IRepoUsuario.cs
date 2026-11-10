using System.Data.SqlTypes;

namespace GranDT.Core.Model.IRepos;

public interface IRepoUsuario
{
    IEnumerable<Usuario> ObtenerUsuarios();
    Usuario? ObtenerPorid(short IdUsuario);
    // validar si la contraseña esta la del email y si es correcta
    bool ValidarCredenciales(string Email, string Password);
    void RegistrarUsario(Usuario usuario, string PasswordHash);
    void EliminarUsuario(short IdUsuario);
    void ActualizarUsuario(Usuario usuario);


}