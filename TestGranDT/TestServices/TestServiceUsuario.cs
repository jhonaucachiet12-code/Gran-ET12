using GranDT.Core.Model;
using GranDT.Core.Model.IRepos;
using Xunit;
using MinimalAPI.Services;
using GranDT.Core.src.Services;

namespace TestGranDT.TestsServices;

public class MockRepoUsuario : IRepoUsuario
{
    public List<Usuario> Usuarios { get; set; } = new();

    public bool SeLlamoObtenerUsuarios { get; private set; }
    public bool SeLlamoRegistrarUsario { get; private set; }
    public bool SeLlamoActualizarUsuario { get; private set; }
    public bool SeLlamoEliminarUsuario { get; private set; }

    public Usuario? UltimoUsuarioRegistrado { get; private set; }
    public Usuario? UltimoUsuarioActualizado { get; private set; }
    public short? UltimoIdEliminado { get; private set; }

    public IEnumerable<Usuario> ObtenerUsuarios()
    {
        SeLlamoObtenerUsuarios = true;
        return Usuarios;
    }

    public Usuario? ObtenerPorid(short idUsuario)
    {
        return Usuarios.FirstOrDefault(u => u.IdUsuario == idUsuario);
    }

    public void RegistrarUsario(Usuario usuario, string passwordHash)
    {
        SeLlamoRegistrarUsario = true;
        UltimoUsuarioRegistrado = usuario;
        Usuarios.Add(usuario);
    }

    public void ActualizarUsuario(Usuario usuario)
    {
        SeLlamoActualizarUsuario = true;
        UltimoUsuarioActualizado = usuario;

        var existente = Usuarios.FirstOrDefault(u => u.IdUsuario == usuario.IdUsuario);
        if (existente != null)
        {
            existente.Nombre = usuario.Nombre;
            existente.Apellido = usuario.Apellido;
            existente.Email = usuario.Email;
            existente.PasswordHash = usuario.PasswordHash;
        }
    }

    public void EliminarUsuario(short idUsuario)
    {
        SeLlamoEliminarUsuario = true;
        UltimoIdEliminado = idUsuario;

        var existente = Usuarios.FirstOrDefault(u => u.IdUsuario == idUsuario);
        if (existente != null)
        {
            Usuarios.Remove(existente);
        }
    }
}





///  test del servicio de usuario





public class UsuarioServiceTests
{
    private static Usuario CrearUsuarioValido(short id = 1, string email = "test@correo.com") => new()
    {
        IdUsuario = id,
        Nombre = "Juan",
        Apellido = "Pérez",
        Email = email,
        FechaNacimiento = new DateTime(1990, 1, 1),
        PasswordHash = "hashPlaceholder",
        IdRol = 1,
        Roles = new Rol { IdRol = 1, Nombre = "Admin" }
    };

    // ObtenerUsuarios 

    [Fact]
    public void ObtenerUsuarios_DevuelveListaDelRepo()
    {
        var mockRepo = new MockRepoUsuario();
        mockRepo.Usuarios.Add(CrearUsuarioValido(1, "a@correo.com"));
        mockRepo.Usuarios.Add(CrearUsuarioValido(2, "b@correo.com"));

        var service = new UsuarioService(mockRepo);

        var resultado = service.ObtenerUsuarios();

        Assert.Equal(2, resultado.Count());
        Assert.True(mockRepo.SeLlamoObtenerUsuarios);
    }

    // ObtenerPorid
    [Fact]
    public void ObtenerPorid_IdValido_DevuelveUsuario()
    {
        var mockRepo = new MockRepoUsuario();
        mockRepo.Usuarios.Add(CrearUsuarioValido(1));
        var service = new UsuarioService(mockRepo);

        var resultado = service.ObtenerPorEmail(1);

        Assert.NotNull(resultado);
        Assert.Equal("Juan", resultado!.Nombre);
    }

    [Fact]
    public void ObtenerPorEmail_IdCero_LanzaExcepcion()
    {
        var mockRepo = new MockRepoUsuario();
        var service = new UsuarioService(mockRepo);

        Assert.Throws<ArgumentOutOfRangeException>(() => service.ObtenerPorEmail(0));
    }

    

    //  RegistrarUsario 

    [Fact]
    public void RegistrarUsario_DatosValidos_LlamaAlRepoYHasheaPassword()
    {
        var mockRepo = new MockRepoUsuario();
        var service = new UsuarioService(mockRepo);
        var usuario = CrearUsuarioValido();

        service.RegistrarUsario(usuario, "miClave123");

        Assert.True(mockRepo.SeLlamoRegistrarUsario);
        
        Assert.NotEqual("miClave123", usuario.PasswordHash);
        Assert.True(BCrypt.Net.BCrypt.Verify("miClave123", usuario.PasswordHash));
    }

    [Fact]
    public void RegistrarUsario_PasswordVacia_LanzaArgumentException()
    {
        var mockRepo = new MockRepoUsuario();
        var service = new UsuarioService(mockRepo);
        var usuario = CrearUsuarioValido();

        Assert.Throws<ArgumentException>(() => service.RegistrarUsario(usuario, ""));
        Assert.False(mockRepo.SeLlamoRegistrarUsario);
    }

    [Fact]
    public void RegistrarUsario_PasswordCorta_LanzaArgumentException()
    {
        var mockRepo = new MockRepoUsuario();
        var service = new UsuarioService(mockRepo);
        var usuario = CrearUsuarioValido();

        Assert.Throws<ArgumentException>(() => service.RegistrarUsario(usuario, "123"));
        Assert.False(mockRepo.SeLlamoRegistrarUsario);
    }

    [Fact]
    public void RegistrarUsario_EmailDuplicado_LanzaArgumentException()
    {
        var mockRepo = new MockRepoUsuario();
        mockRepo.Usuarios.Add(CrearUsuarioValido(1, "repetido@correo.com"));
        var service = new UsuarioService(mockRepo);

        var nuevoUsuario = CrearUsuarioValido(2, "repetido@correo.com");

        Assert.Throws<ArgumentException>(() => service.RegistrarUsario(nuevoUsuario, "miClave123"));
        Assert.False(mockRepo.SeLlamoRegistrarUsario);
    }

    [Fact]
    public void RegistrarUsario_NombreVacio_LanzaArgumentException()
    {
        var mockRepo = new MockRepoUsuario();
        var service = new UsuarioService(mockRepo);
        var usuario = CrearUsuarioValido();
        usuario.Nombre = "";

        Assert.Throws<ArgumentException>(() => service.RegistrarUsario(usuario, "miClave123"));
        Assert.False(mockRepo.SeLlamoRegistrarUsario);
    }

    [Fact]
    public void RegistrarUsario_FechaNacimientoFutura_LanzaArgumentException()
    {
        var mockRepo = new MockRepoUsuario();
        var service = new UsuarioService(mockRepo);
        var usuario = CrearUsuarioValido();
        usuario.FechaNacimiento = DateTime.Now.AddDays(1);

        Assert.Throws<ArgumentException>(() => service.RegistrarUsario(usuario,"miClave123"));
        Assert.False(mockRepo.SeLlamoRegistrarUsario);
    }

    [Fact]
    public void RegistrarUsario_EmailMalFormado_LanzaArgumentException()
    {
        var mockRepo = new MockRepoUsuario();
        var service = new UsuarioService(mockRepo);
        var usuario = CrearUsuarioValido(1, "aaa@.com");
        

        Assert.Throws<ArgumentException>(() => service.RegistrarUsario(usuario, "miClave123"));
        Assert.False(mockRepo.SeLlamoRegistrarUsario);
    }

    [Fact]
    public void RegistrarUsario_Null_LanzaArgumentNullException()
    {
        var mockRepo = new MockRepoUsuario();
        var service = new UsuarioService(mockRepo);

        Assert.Throws<ArgumentNullException>(() => service.RegistrarUsario(null!,"miClave123"));
    }

    

    [Fact]
    public void ActualizarUsuario_MismaPassword_ConservaHashExistente()
    {
        var mockRepo = new MockRepoUsuario();
        var usuarioExistente = CrearUsuarioValido(1);
        usuarioExistente.PasswordHash = BCrypt.Net.BCrypt.HashPassword("miClave123");
        mockRepo.Usuarios.Add(usuarioExistente);

        var service = new UsuarioService(mockRepo);

        var actualizado = CrearUsuarioValido(1);
        actualizado.Apellido = "Nuevo Apellido";
        actualizado.PasswordHash = "miClave123"; 

        service.ActualizarUsuario(actualizado);

        Assert.True(mockRepo.SeLlamoActualizarUsuario);
        
        Assert.Equal(usuarioExistente.PasswordHash, mockRepo.UltimoUsuarioActualizado?.PasswordHash);
    }

    [Fact]
    public void ActualizarUsuario_PasswordNueva_GeneraNuevoHash()
    {
        var mockRepo = new MockRepoUsuario();
        var usuarioExistente = CrearUsuarioValido(1);
        usuarioExistente.PasswordHash = BCrypt.Net.BCrypt.HashPassword("claveVieja");
        mockRepo.Usuarios.Add(usuarioExistente);

        var service = new UsuarioService(mockRepo);

        var actualizado = CrearUsuarioValido(1);
        actualizado.PasswordHash = "claveNueva456";

        service.ActualizarUsuario(actualizado);

        Assert.True(mockRepo.SeLlamoActualizarUsuario);
        var hashFinal = mockRepo.UltimoUsuarioActualizado!.PasswordHash;
        Assert.True(BCrypt.Net.BCrypt.Verify("claveNueva456", hashFinal));
        Assert.False(BCrypt.Net.BCrypt.Verify("claveVieja", hashFinal));
    }

    [Fact]
    public void ActualizarUsuario_UsuarioNoExiste_LanzaExcepcion()
    {
        var mockRepo = new MockRepoUsuario();
        var service = new UsuarioService(mockRepo);
        var usuario = CrearUsuarioValido(99);
        usuario.PasswordHash = "cualquierClave";

        Assert.Throws<Exception>(() => service.ActualizarUsuario(usuario));
        Assert.False(mockRepo.SeLlamoActualizarUsuario);
    }

    [Fact]
    public void ActualizarUsuario_NombreVacio_LanzaArgumentException()
    {
        var mockRepo = new MockRepoUsuario();
        var service = new UsuarioService(mockRepo);
        var usuario = CrearUsuarioValido();
        usuario.Nombre = "";

        Assert.Throws<ArgumentException>(() => service.ActualizarUsuario(usuario));
        Assert.False(mockRepo.SeLlamoActualizarUsuario);
    }

    // EliminarUsuario 

    [Fact]
    public void EliminarUsuario_IdValido_LlamaAlRepo()
    {
        var mockRepo = new MockRepoUsuario();
        mockRepo.Usuarios.Add(CrearUsuarioValido(1));
        var service = new UsuarioService(mockRepo);

        service.EliminarUsuario(1);

        Assert.True(mockRepo.SeLlamoEliminarUsuario);
        Assert.Equal((short)1, mockRepo.UltimoIdEliminado);
        Assert.Empty(mockRepo.Usuarios);
    }

    

    [Fact]
    public void EliminarUsuario_IdCero_NoLanzaYLlamaAlRepo()
    {
        
        var mockRepo = new MockRepoUsuario();
        var service = new UsuarioService(mockRepo);


        Assert.Throws<ArgumentOutOfRangeException>(() => service.EliminarUsuario(0));

        Assert.False(mockRepo.SeLlamoEliminarUsuario);
    }
}