//service de rol
using GranDT.Core.Model;
using GranDT.Core.Model.IRepos;
namespace GranDT.Core.src.Services;
public class RolService
{
    private readonly IRepoRol repoRol;

    public RolService(IRepoRol repoRol)
    {
        this.repoRol = repoRol;
    }

    public IEnumerable<Rol> ObtenerRoles()
    {
        return repoRol.ObtenerRoles();
    }

    public Rol? ObtenerRolPorId(byte IdRol)
    {
        ValidarId(IdRol);

        return repoRol.ObtenerRolPorId(IdRol);
    }

    public void AgregarRol(Rol rol)
    {
        
        ValidarRol(rol);
        repoRol.AgregarRol(rol);
    }

    public void ActualizarRol(Rol rol)
    {
        ValidarRol(rol);
        ValidarId(rol.IdRol);
        repoRol.ActualizarRol(rol);
    }
    private static void ValidarRol(Rol rol)
    {
        ArgumentNullException.ThrowIfNull(rol);

        if (string.IsNullOrWhiteSpace(rol.Nombre))
        {
            throw new ArgumentException("El nombre del rol es obligatorio.", nameof(rol));
        }
    }

    private static void ValidarId(byte id)
    {
        if (id <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(id), "El identificador debe ser mayor que cero.");
        }
    }
}