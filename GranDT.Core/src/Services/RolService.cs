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
        validarExistenciaDelRol(IdRol);


        return repoRol.ObtenerRolPorId(IdRol);
    }

    public void AgregarRol(Rol rol)
    {
        
        ValidarRol(rol);
        validarSuUnicidad(rol);

        repoRol.AgregarRol(rol);
    }

    public void ActualizarRol(Rol rol)
    {
        ValidarRol(rol);
        ValidarId(rol.IdRol);
        validarExistenciaDelRol(rol.IdRol);
        validarSuUnicidad(rol);

        repoRol.ActualizarRol(rol);

    }

    public void EliminarRol(byte id)
    {
        ValidarId(id);
        validarExistenciaDelRol(id);

        repoRol.EliminarRol(id);
        
    }
    public  void ValidarRol(Rol rol)
    {
        ArgumentNullException.ThrowIfNull(rol);

        if (string.IsNullOrWhiteSpace(rol.Nombre))
        {
            throw new ArgumentException("El nombre del rol es obligatorio.", nameof(rol));
        }
    }

    public static void ValidarId(byte id)
    {
        if (id <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(id), "El identificador debe ser mayor que cero.");
        }
    }

    private void validarExistenciaDelRol(Byte IdRol)
    {
        
        if(repoRol.ObtenerRolPorId(IdRol)is null)
        {
            throw new KeyNotFoundException("el rol no existe");
        }
    } 

    private void validarSuUnicidad(Rol rol)
    {
        var rolesExistentes = repoRol.ObtenerRoles();

        if(rolesExistentes.Any(r => r.Nombre == rol.Nombre))
		{
			throw new InvalidOperationException("El rol ya exite");
		}

    }
}