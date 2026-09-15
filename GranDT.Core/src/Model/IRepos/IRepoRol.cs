namespace GranDT.Core.Model.IRepos;

public interface IRepoRol
{
    IEnumerable<Rol> ObtenerRoles();
    Rol? ObtenerRolPorId(byte IdRol);
    void AgregarRol(Rol rol);
    void ActualizarRol(Rol rol);
    void EliminarRol(byte IdRol);
}