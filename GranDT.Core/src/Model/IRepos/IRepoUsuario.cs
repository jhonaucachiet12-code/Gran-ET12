using System.Data.SqlTypes;

namespace GranDT.Core.Model.IRepos;

public interface IRepoUsuario
{
    IEnumerable<Usuario> ObtenerUsuarios();
    Usuario? ObtenerPorEmail(short IdUsuario);
    void RegistrarUsario(Usuario usuario, string PasswordHash);
    void EliminarUsuario(short IdUsuario);
    void ActualizarUsuario(Usuario usuario);

}