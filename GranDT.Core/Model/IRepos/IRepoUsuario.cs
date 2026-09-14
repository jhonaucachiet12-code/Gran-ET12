namespace GranDT.Core.Model.IRepos;

public interface IRepoUsuario
{
    Task<Puntuacion?> ObtenerPorEmail(string Email);

    Task<bool> RegistrarUsario(Usuario usuario, string PasswordHash);
}