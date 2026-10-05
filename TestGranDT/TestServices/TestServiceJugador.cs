using GranDT.Core.Model;
using GranDT.Core.Model.IRepos;
using Xunit;
using MinimalAPI.Services;
using GranDT.Core.src.Services;

namespace TestGranDT.TestsServices;
public class MockRepoJugador : IRepoJugador
{
    public List<Jugador> Jugadores { get; set; } = new();

    public bool SeLlamoObtenerJugadores { get; private set; }
    public bool SeLlamoAgregarJugador { get; private set; }
    public bool SeLlamoActualizarJugador { get; private set; }
    public bool SeLlamoEliminarJugador { get; private set; }

    public Jugador? UltimoJugadorAgregado { get; private set; }
    public Jugador? UltimoJugadorActualizado { get; private set; }
    public short? UltimoIdEliminado { get; private set; }

    public IEnumerable<Jugador> ObtenerJugadores()
    {
        SeLlamoObtenerJugadores = true;
        return Jugadores;
    }

    public Jugador? ObtenerJugadorPorId(short idJugador)
    {
        return Jugadores.FirstOrDefault(j => j.IdJugador == idJugador);
    }

    public void AgregarJugador(Jugador jugador)
    {
        SeLlamoAgregarJugador = true;
        UltimoJugadorAgregado = jugador;
        Jugadores.Add(jugador);
    }

    public void ActualizarJugador(Jugador jugador)
    {
        SeLlamoActualizarJugador = true;
        UltimoJugadorActualizado = jugador;

        var existente = Jugadores.FirstOrDefault(j => j.IdJugador == jugador.IdJugador);
        if (existente != null)
        {
            existente.Nombre = jugador.Nombre;
            existente.Apellido = jugador.Apellido;
            existente.Cotización = jugador.Cotización;
        }
    }

    public void EliminarJugador(short idJugador)
    {
        SeLlamoEliminarJugador = true;
        UltimoIdEliminado = idJugador;

        var existente = Jugadores.FirstOrDefault(j => j.IdJugador == idJugador);
        if (existente != null)
        {
            Jugadores.Remove(existente);
        }
    }
}





//test




public class JugadorServiceTests
{
    private static Jugador CrearJugadorValido(short id = 1) => new()
    {
        IdJugador = id,
        IdPosicion = 1,
        IdEquipo = 1,
        Nombre = "Lionel",
        Apellido = "Messi",
        Apodo = "Leo",
        Nacimiento = new DateTime(1987, 6, 24),
        Cotización = 100000
    };

    // --- ObtenerJugadores ---

    [Fact]
    public void ObtenerJugadores_DevuelveListaDelRepo()
    {
        var mockRepo = new MockRepoJugador();
        mockRepo.Jugadores.Add(CrearJugadorValido(1));
        mockRepo.Jugadores.Add(CrearJugadorValido(2));

        var service = new JugadorService(mockRepo);

        var resultado = service.ObtenerJugadores();

        Assert.Equal(2, resultado.Count());
        Assert.True(mockRepo.SeLlamoObtenerJugadores);
    }

    // --- ObtenerJugadorPorId ---

    [Fact]
    public void ObtenerJugadorPorId_IdValido_DevuelveJugador()
    {
        var mockRepo = new MockRepoJugador();
        mockRepo.Jugadores.Add(CrearJugadorValido(1));
        var service = new JugadorService(mockRepo);

        var resultado = service.ObtenerJugadorPorId(1);

        Assert.NotNull(resultado);
        Assert.Equal("Messi", resultado!.Apellido);
    }

    [Fact]
    public void ObtenerJugadorPorId_IdCero_LanzaExcepcion()
    {
        var mockRepo = new MockRepoJugador();
        var service = new JugadorService(mockRepo);

        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => service.ObtenerJugadorPorId(0));
        Assert.Contains("El identificador debe ser mayor que cero.", ex.Message);
    }

    // --- AgregarJugador ---

    [Fact]
    public void AgregarJugador_JugadorValido_LlamaAlRepo()
    {
        var mockRepo = new MockRepoJugador();
        var service = new JugadorService(mockRepo);
        var jugador = CrearJugadorValido();

        service.AgregarJugador(jugador);

        Assert.True(mockRepo.SeLlamoAgregarJugador);
        Assert.Equal("Messi", mockRepo.UltimoJugadorAgregado?.Apellido);
    }


    [Fact]
    public void AgregarJugador_NombreVacio_LanzaArgumentException()
    {
        var mockRepo = new MockRepoJugador();
        var service = new JugadorService(mockRepo);
        var jugador = CrearJugadorValido();
        jugador.Nombre = "";

        var ex =Assert.Throws<ArgumentException>(() => service.AgregarJugador(jugador));
        Assert.Contains("El nombre del jugador es obligatorio.", ex.Message);
        Assert.False(mockRepo.SeLlamoAgregarJugador);
    }

    [Fact]
    public void AgregarJugador_ApellidoVacio_LanzaArgumentException()
    {
        var mockRepo = new MockRepoJugador();
        var service = new JugadorService(mockRepo);
        var jugador = CrearJugadorValido();
        jugador.Apellido = "";

        var ex = Assert.Throws<ArgumentException>(() => service.AgregarJugador(jugador));
        Assert.Contains("El apellido del jugador es obligatorio.", ex.Message);
        Assert.False(mockRepo.SeLlamoAgregarJugador);
    }

    [Fact]
    public void AgregarJugador_FechaNacimientoFutura_LanzaArgumentException()
    {
        var mockRepo = new MockRepoJugador();
        var service = new JugadorService(mockRepo);
        var jugador = CrearJugadorValido();
        jugador.Nacimiento = DateTime.Now.AddDays(1);

        var ex = Assert.Throws<ArgumentException>(() => service.AgregarJugador(jugador));
        Assert.Contains("La fecha de nacimiento del jugador no puede ser futura.", ex.Message);


        Assert.False(mockRepo.SeLlamoAgregarJugador);
    }

    [Fact]
    public void AgregarJugador_PosicionInvalida_LanzaArgumentException()
    {
        var mockRepo = new MockRepoJugador();
        var service = new JugadorService(mockRepo);
        var jugador = CrearJugadorValido();
        jugador.IdPosicion = 0;

        Assert.Throws<ArgumentException>(() => service.AgregarJugador(jugador));
        Assert.False(mockRepo.SeLlamoAgregarJugador);
    }

    [Fact]
    public void AgregarJugador_EquipoInvalido_LanzaArgumentException()
    {
        var mockRepo = new MockRepoJugador();
        var service = new JugadorService(mockRepo);
        var jugador = CrearJugadorValido();
        jugador.IdEquipo = 0;

        Assert.Throws<ArgumentException>(() => service.AgregarJugador(jugador));
        Assert.False(mockRepo.SeLlamoAgregarJugador);
    }

    [Fact]
    public void AgregarJugador_CotizacionNegativa_LanzaArgumentException()
    {
        var mockRepo = new MockRepoJugador();
        var service = new JugadorService(mockRepo);
        var jugador = CrearJugadorValido();
        jugador.Cotización = -500;

        Assert.Throws<ArgumentException>(() => service.AgregarJugador(jugador));
        Assert.False(mockRepo.SeLlamoAgregarJugador);
    }

    // --- ActualizarJugador ---

    [Fact]
    public void ActualizarJugador_DatosValidos_LlamaAlRepo()
    {
        var mockRepo = new MockRepoJugador();
        mockRepo.Jugadores.Add(CrearJugadorValido(1));
        var service = new JugadorService(mockRepo);

        var actualizado = CrearJugadorValido(1);
        actualizado.Cotización = 200000;

        service.ActualizarJugador(actualizado);

        Assert.True(mockRepo.SeLlamoActualizarJugador);
        Assert.Equal(200000, mockRepo.UltimoJugadorActualizado?.Cotización);
    }

    [Fact]
    public void ActualizarJugador_IdInvalido_LanzaExcepcion()
    {
        var mockRepo = new MockRepoJugador();
        var service = new JugadorService(mockRepo);
        var jugador = CrearJugadorValido(0);

        Assert.Throws<ArgumentOutOfRangeException>(() => service.ActualizarJugador(jugador));
        Assert.False(mockRepo.SeLlamoActualizarJugador);
    }

    // --- EliminarJugador ---

    [Fact]
    public void EliminarJugador_IdValido_LlamaAlRepo()
    {
        var mockRepo = new MockRepoJugador();
        mockRepo.Jugadores.Add(CrearJugadorValido(1));
        var service = new JugadorService(mockRepo);

        service.EliminarJugador(1);

        Assert.True(mockRepo.SeLlamoEliminarJugador);
        Assert.Equal((short)1, mockRepo.UltimoIdEliminado);
        Assert.Empty(mockRepo.Jugadores);
    }

    [Fact]
    public void EliminarJugador_IdCero_LanzaExcepcion()
    {
        var mockRepo = new MockRepoJugador();
        var service = new JugadorService(mockRepo);

        Assert.Throws<ArgumentOutOfRangeException>(() => service.EliminarJugador(0));
        Assert.False(mockRepo.SeLlamoEliminarJugador);
    }
}