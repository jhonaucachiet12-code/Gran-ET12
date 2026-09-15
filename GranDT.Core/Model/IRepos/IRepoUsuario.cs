using System.Data.SqlTypes;

namespace GranDT.Core.Model.IRepos;

public interface IRepoUsuario
{
    IEnumerable<Rol> ObtenerUsuarios();
    Task<Usuario?> ObtenerPorEmail(string Email);
    Task<bool> RegistrarUsario(Usuario usuario, string PasswordHash);
    
}