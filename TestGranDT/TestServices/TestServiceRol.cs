using GranDT.Core.Model;
using GranDT.Core.Model.IRepos;
using Xunit;
using MinimalAPI.Services;
using GranDT.Core.src.Services;

namespace TestGranDT.TestsServices;

public class MockRepoRol:IRepoRol
{
    public List<Rol> Roles { get; set; } = new();

    public bool SeLlamoObtenerRoles { get; private set; }
    public bool SeLlamoAgregarRol { get; private set; }
    public bool SeLlamoActualizarRol { get; private set; }
    public bool SeLlamoEliminarRol { get; private set; }

    public Rol? UltimoRolAgregado { get; private set; }
    public Rol? UltimoRolActualizado { get; private set; }
    public byte? UltimoIdEliminado { get; private set; }

    public IEnumerable<Rol> ObtenerRoles()
    {
        SeLlamoObtenerRoles = true;
        return Roles;
    }

    public Rol? ObtenerRolPorId(byte idRol)
    {
        return Roles.FirstOrDefault(r => r.IdRol == idRol);
    }

    public void AgregarRol(Rol rol)
    {
        SeLlamoAgregarRol = true;
        UltimoRolAgregado = rol;
        Roles.Add(rol);
    }

    public void ActualizarRol(Rol rol)
    {
        SeLlamoActualizarRol = true;
        UltimoRolActualizado = rol;

        var existente = Roles.FirstOrDefault(r => r.IdRol == rol.IdRol);
        if (existente != null)
        {
            existente.Nombre = rol.Nombre;
        }
    }

    public void EliminarRol(byte idRol)
    {
        SeLlamoEliminarRol = true;
        UltimoIdEliminado = idRol;

        var existente = Roles.FirstOrDefault(r => r.IdRol == idRol);
        if (existente != null)
        {
            Roles.Remove(existente);
        }
    }
}




// test 

public class TestServiceRol
{

    
    [Fact]
    public void ObtenerRoles_DevuelveListaDeRoles()
    {
        var mockRepo = new MockRepoRol();
        mockRepo.Roles.Add(new Rol {IdRol = 1 , Nombre ="Usuario"});
        mockRepo.Roles.Add(new Rol {IdRol = 2 , Nombre ="Admin"});

        var service = new RolService(mockRepo);

        var resultado = service.ObtenerRoles();

        Assert.Equal(2, resultado.Count());
        Assert.True(mockRepo.SeLlamoObtenerRoles);
    }

    // Test de obtenerRoles

    [Fact]
    public void ObtenerRolPorId_DevuelveElrol()
    {
         var mockRepo = new MockRepoRol();
        mockRepo.Roles.Add(new Rol {IdRol = 1 , Nombre ="Usuario"});
        var service = new RolService(mockRepo);

        var resultado = service.ObtenerRolPorId(1);

        Assert.NotNull(resultado);
        Assert.True(resultado.IdRol > 0);
        Assert.Equal("Usuario", resultado!.Nombre);
        
    }

    [Fact]
    public void ObtenerRolPorId_IdValido()
    {
        var mockRepo = new MockRepoRol();
        var service = new RolService(mockRepo);

        var ex =Assert.Throws<ArgumentOutOfRangeException>(() => service.ObtenerRolPorId(0));
        Assert.Contains("El identificador debe ser mayor que cero.", ex.Message);
    }

    // agregarRol
    [Fact]
    public void agregarRol_RolValido()
    {
        var mockRepo = new MockRepoRol();
        var service = new RolService(mockRepo);
        var nuevoEquipo = new Rol { IdRol = 1, Nombre = "Independiente" };

        var equiposExistentes = service.ObtenerRoles();
        Assert.DoesNotContain(nuevoEquipo, equiposExistentes);

        service.AgregarRol(nuevoEquipo);

        Assert.True(mockRepo.SeLlamoAgregarRol);
        Assert.Equal("Independiente", mockRepo.UltimoRolAgregado?.Nombre);

        // nombre vasillo
    }

    [Fact]
    public void agregarRol_RolInValido_faltaNombre()
    {
        var mockRepo = new MockRepoRol();
        var service = new RolService(mockRepo);

        var rolInvalido = new Rol { IdRol = 1, Nombre = "" };

        Assert.Throws<ArgumentException>(() => service.AgregarRol(rolInvalido));
        Assert.False(mockRepo.SeLlamoAgregarRol);

    }

    [Fact]
    public void AgregarEquipo_Null_LanzaArgumentNullException()
    {
        var mockRepo = new MockRepoRol();
        var service = new RolService(mockRepo);

        Assert.Throws<ArgumentNullException>(() => service.ActualizarRol(null!));
    }

    // AtualizarRol

    [Fact]
    public void ActualizarRol_RolValido_LlamaAlRepo()
    {
        var mockRepo = new MockRepoRol();
        var service = new RolService(mockRepo);
        var rolExistente = new Rol { IdRol = 1, Nombre = "Usuario" };
        mockRepo.Roles.Add(rolExistente);

        var rolActualizado = new Rol { IdRol = 1, Nombre = "Administrador" };

        service.ActualizarRol(rolActualizado);

        Assert.True(mockRepo.SeLlamoActualizarRol);
        Assert.Equal("Administrador", mockRepo.UltimoRolActualizado?.Nombre);
    }

    [Fact]
    public void ActualizarRol_RolInvalido_LanzaArgumentException()
    {
        var mockRepo = new MockRepoRol();
        var service = new RolService(mockRepo);
        var rolExistente = new Rol { IdRol = 1, Nombre = "Usuario" };
        mockRepo.Roles.Add(rolExistente);

        var rolInvalido = new Rol { IdRol = 1, Nombre = "" };

        Assert.Throws<ArgumentException>(() => service.ActualizarRol(rolInvalido));
        Assert.False(mockRepo.SeLlamoActualizarRol);
    }

    //EliminarRol
    [Fact]
    public void EliminarRol_RolValido_LlamaAlRepo()
    {
        var mockRepo = new MockRepoRol();
        var service = new RolService(mockRepo);
        var rolExistente = new Rol { IdRol = 1, Nombre = "Usuario" };
        mockRepo.Roles.Add(rolExistente);

        service.EliminarRol(rolExistente.IdRol);

        Assert.True(mockRepo.SeLlamoEliminarRol);
    }

    [Fact]
    public void EliminarRol_RolInvalido_LanzaArgumentException()
    {
        var mockRepo = new MockRepoRol();
        var service = new RolService(mockRepo);

        Assert.Throws<ArgumentOutOfRangeException>(() => service.EliminarRol(0));
        Assert.False(mockRepo.SeLlamoEliminarRol);
    }

}