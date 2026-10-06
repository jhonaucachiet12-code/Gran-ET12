using GranDT.Core.Model;
using GranDT.Core.Model.IRepos;
using Xunit;
using MinimalAPI.Services;
using GranDT.Core.src.Services;

namespace TestGranDT.TestsServices;

public class MockRepoPlantilla : IRepoPlantilla
{
    public List<Plantilla> Plantillas { get; set; } = new();

    // Para simular distintas respuestas en ObtenerJugadoresDeLaPlantilla sin reconstruir todo
    public Func<int, Plantilla?>? ObtenerJugadoresDeLaPlantillaOverride { get; set; }

    public bool SeLlamoAgregarJugadorAPlantilla { get; private set; }
    public bool SeLlamoActualizarJugadorEnPlantilla { get; private set; }
    public bool SeLlamoEliminarJugadorDePlantilla { get; private set; }
    public bool SeLlamoAgregarPlantilla { get; private set; }
    public bool SeLlamoActualizarPlantilla { get; private set; }
    public bool SeLlamoEliminarPlantilla { get; private set; }

    public (int idPlantilla, short idJugador, bool esTitular)? UltimoAgregado { get; private set; }
    public (int idPlantilla, short idJugador, bool esTitular)? UltimoActualizado { get; private set; }

    public IEnumerable<Plantilla> ObtenerPlantillas() => Plantillas;

    public Plantilla? ObtenerPlantillaPorId(int idPlantilla) =>
        Plantillas.FirstOrDefault(p => p.IdPlantilla == idPlantilla);

    public void AgregarPlantilla(Plantilla plantilla)
    {
        SeLlamoAgregarPlantilla = true;
        Plantillas.Add(plantilla);
    }

    public void ActualizarPlantilla(Plantilla plantilla)
    {
        SeLlamoActualizarPlantilla = true;
    }

    public void EliminarPlantilla(int idPlantilla)
    {
        SeLlamoEliminarPlantilla = true;
        var existente = Plantillas.FirstOrDefault(p => p.IdPlantilla == idPlantilla);
        if (existente != null) Plantillas.Remove(existente);
    }

    public Plantilla? ObtenerJugadoresDeLaPlantilla(int idPlantilla)
    {
        if (ObtenerJugadoresDeLaPlantillaOverride != null)
            return ObtenerJugadoresDeLaPlantillaOverride(idPlantilla);

        return Plantillas.FirstOrDefault(p => p.IdPlantilla == idPlantilla);
    }

    public void AgregarJugadorAPlantilla(int idPlantilla, short idJugador, bool esTitular)
    {
        SeLlamoAgregarJugadorAPlantilla = true;
        UltimoAgregado = (idPlantilla, idJugador, esTitular);
        var plantilla = Plantillas.FirstOrDefault(p => p.IdPlantilla == idPlantilla);
    if (plantilla != null)
    {
        // Simulamos buscar el objeto Jugador en una lista global o crearlo al vuelo
        var jugadorSimulado = new Jugador { IdJugador = idJugador , Nombre = "Simulado", Apellido = "Simulado", Apodo = "Sim", IdPosicion = 1, IdEquipo = 1, Nacimiento = DateTime.Now, Cotización = 1000 }; 
        
        if (esTitular)
        {
            plantilla.JugadoresTitulares.Add(jugadorSimulado);
        }
        else
        {
            plantilla.JugadoresSuplentes.Add(jugadorSimulado);
        }
    }
    }

    public void ActualizarJugadorEnPlantilla(int idPlantilla, short idJugador, bool esTitular)
    {
        SeLlamoActualizarJugadorEnPlantilla = true;
        UltimoActualizado = (idPlantilla, idJugador, esTitular);
    }

    public void EliminarJugadorDePlantilla(short idJugador, int idPlantilla)
    {
        SeLlamoEliminarJugadorDePlantilla = true;
    }

    public decimal ObtenersumaDeLasPuntuacionesDeLosTitularesDeLaPlantilla(int idPlantilla, byte fecha) => 0;

    public decimal ObtenerElValorTotalDeLaPlantilla(int idPlantilla) => 0;
}




public class MockRepoJugador2 : IRepoJugador
{
    public List<Jugador> Jugadores { get; set; } = new();

    public IEnumerable<Jugador> ObtenerJugadores() => Jugadores;

    public Jugador? ObtenerJugadorPorId(short idJugador) =>
        Jugadores.FirstOrDefault(j => j.IdJugador == idJugador);

    public void AgregarJugador(Jugador jugador) => Jugadores.Add(jugador);
    public void ActualizarJugador(Jugador jugador) { }
    public void EliminarJugador(short idJugador) { }
}






public class PlantillaServiceTests
{
    private static Jugador CrearJugador(short id, byte idPosicion) => new()
    {
        IdJugador = id,
        IdPosicion = idPosicion,
        IdEquipo = 1,
        Nombre = "Test",
        Apellido = "Jugador",
        Apodo = "T",
        Nacimiento = new DateTime(1995, 1, 1),
        Cotización = 1000
    };

    private static Plantilla CrearPlantillaVacia(int id = 1) => new()
    {
        IdPlantilla = id,
        IdUsuario = 1,
        Nombre = "Mi Plantilla",
        Presupuesto = 9000000,
        JugadoresTitulares = new List<Jugador>(),
        JugadoresSuplentes = new List<Jugador>()
        
    };


        // --- AgregarJugadorAPlantilla: caso feliz ---

    [Fact]
    public void AgregarJugadorAPlantilla_JugadorValidoYCupoLibre_LlamaAlRepo()
    {
        var mockPlantilla = new MockRepoPlantilla();
        var mockJugador = new MockRepoJugador();

        var jugador = CrearJugador(1, idPosicion: 1); // arquero
        mockJugador.Jugadores.Add(jugador);
        mockPlantilla.Plantillas.Add(CrearPlantillaVacia(1));

        var service = new PlantillaService(mockPlantilla, mockJugador);

        service.AgregarJugadorAPlantilla(idPlantilla: 1, idJugador: 1, esTitular: true);

        Assert.True(mockPlantilla.SeLlamoAgregarJugadorAPlantilla);
        Assert.Equal((1, (short)1, true), mockPlantilla.UltimoAgregado);
    }

    // --- AgregarJugadorAPlantilla: plantilla inexistente ---

    [Fact]
    public void AgregarJugadorAPlantilla_PlantillaNoExiste_LanzaArgumentException()
    {
        var mockPlantilla = new MockRepoPlantilla(); // sin plantillas
        var mockJugador = new MockRepoJugador();
        var service = new PlantillaService(mockPlantilla, mockJugador);

        Assert.Throws<ArgumentException>(() =>
            service.AgregarJugadorAPlantilla(1, 1, true));

        Assert.False(mockPlantilla.SeLlamoAgregarJugadorAPlantilla);
    }

    // --- AgregarJugadorAPlantilla: jugador ya en la plantilla ---

    [Fact]
    public void AgregarJugadorAPlantilla_JugadorYaEnPlantilla_LanzaInvalidOperationException()
    {
        var mockPlantilla = new MockRepoPlantilla();
        var mockJugador = new MockRepoJugador();

        var plantilla = CrearPlantillaVacia(1);
        plantilla.JugadoresTitulares.Add(CrearJugador(1, 1));
        mockPlantilla.Plantillas.Add(plantilla);

        var service = new PlantillaService(mockPlantilla, mockJugador);

        Assert.Throws<InvalidOperationException>(() =>
            service.AgregarJugadorAPlantilla(1, 1, true));

        Assert.False(mockPlantilla.SeLlamoAgregarJugadorAPlantilla);
    }

    // --- AgregarJugadorAPlantilla: plantilla llena (20 jugadores) ---

    [Fact]
    public void AgregarJugadorAPlantilla_PlantillaLlena_LanzaInvalidOperationException()
    {
        var mockPlantilla = new MockRepoPlantilla();
        var mockJugador = new MockRepoJugador();

        var plantilla = CrearPlantillaVacia(1);
        mockPlantilla.Plantillas.Add(plantilla);

        mockJugador.Jugadores.Add(CrearJugador(1, idPosicion: 1));

        mockJugador.Jugadores.Add(CrearJugador(2, idPosicion: 2)); // defensor
        mockJugador.Jugadores.Add(CrearJugador(3, idPosicion: 2));
        mockJugador.Jugadores.Add(CrearJugador(4, idPosicion: 2));
        mockJugador.Jugadores.Add(CrearJugador(5, idPosicion: 2));

        mockJugador.Jugadores.Add(CrearJugador(6, idPosicion: 3));
        mockJugador.Jugadores.Add(CrearJugador(7, idPosicion: 3));
        mockJugador.Jugadores.Add(CrearJugador(8, idPosicion: 3));
        mockJugador.Jugadores.Add(CrearJugador(9, idPosicion: 3));

        mockJugador.Jugadores.Add(CrearJugador(10, idPosicion: 4));
        mockJugador.Jugadores.Add(CrearJugador(11, idPosicion: 4));

        mockJugador.Jugadores.Add(CrearJugador(12, idPosicion: 4));
        mockJugador.Jugadores.Add(CrearJugador(13, idPosicion: 4));
        mockJugador.Jugadores.Add(CrearJugador(14, idPosicion: 4));
        mockJugador.Jugadores.Add(CrearJugador(15, idPosicion: 4));
        mockJugador.Jugadores.Add(CrearJugador(16, idPosicion: 4));
        mockJugador.Jugadores.Add(CrearJugador(17, idPosicion: 4));
        mockJugador.Jugadores.Add(CrearJugador(18, idPosicion: 4));
        mockJugador.Jugadores.Add(CrearJugador(19, idPosicion: 4));
        mockJugador.Jugadores.Add(CrearJugador(20, idPosicion: 4));

        var service = new PlantillaService(mockPlantilla, mockJugador);

        // Llenamos con 20 jugadores (ids 100 en adelante para no chocar con el que intentamos agregar)
        for (short i = 1; i <= 11; i++)
        {
            service.AgregarJugadorAPlantilla(idPlantilla: 1, idJugador: i, esTitular: true); // defensor, variado
        }

        for(short i = 12 ; i <= 20;i++)
        {
            service.AgregarJugadorAPlantilla(idPlantilla: 1, idJugador: i, esTitular: false);
        }
        mockPlantilla.Plantillas.Add(plantilla);

        var jugadorNuevo = CrearJugador(21, 3);
        mockJugador.Jugadores.Add(jugadorNuevo);

        

        Assert.Throws<InvalidOperationException>(() =>
            service.AgregarJugadorAPlantilla(1, 21, false));

        
    }

    // --- ValidarCupoTitular: cupo de posición lleno ---

    [Fact]
    public void AgregarJugadorAPlantilla_CupoDeArquerosLleno_LanzaInvalidOperationException()
    {
        var mockPlantilla = new MockRepoPlantilla();
        var mockJugador = new MockRepoJugador();

        var plantilla = CrearPlantillaVacia(1);
        plantilla.JugadoresTitulares.Add(CrearJugador(50, idPosicion: 1)); // ya hay 1 arquero titular (máximo)
        mockPlantilla.Plantillas.Add(plantilla);

        var nuevoArquero = CrearJugador(1, idPosicion: 1);
        mockJugador.Jugadores.Add(nuevoArquero);

        var service = new PlantillaService(mockPlantilla, mockJugador);

        var ex = Assert.Throws<InvalidOperationException>(() =>
            service.AgregarJugadorAPlantilla(1, 1, esTitular: true));

        Assert.Contains("arquero", ex.Message);
        Assert.False(mockPlantilla.SeLlamoAgregarJugadorAPlantilla);
    }

    [Fact]
    public void AgregarJugadorAPlantilla_JugadorNoExiste_LanzaArgumentException()
    {
        var mockPlantilla = new MockRepoPlantilla();
        var mockJugador = new MockRepoJugador(); // sin jugadores
        mockPlantilla.Plantillas.Add(CrearPlantillaVacia(1));

        var service = new PlantillaService(mockPlantilla, mockJugador);

        Assert.Throws<ArgumentException>(() =>
            service.AgregarJugadorAPlantilla(1, idJugador: 99, esTitular: true));
    }

    [Fact]
    public void AgregarJugadorAPlantilla_IdPlantillaInvalido_LanzaArgumentOutOfRangeException()
    {
        var mockPlantilla = new MockRepoPlantilla();
        var mockJugador = new MockRepoJugador();
        var service = new PlantillaService(mockPlantilla, mockJugador);

        Assert.Throws<ArgumentOutOfRangeException>(() =>
            service.AgregarJugadorAPlantilla(0, 1, true));
    }

    [Fact]
    public void AgregarJugadorAPlantilla_titularesllenos_LanzaInvalidOperationException()
    {
        var mockPlantilla = new MockRepoPlantilla();
        var mockJugador = new MockRepoJugador();

        mockPlantilla.Plantillas.Add(CrearPlantillaVacia(1));

        mockJugador.Jugadores.Add(CrearJugador(1, idPosicion: 1)); // arquero

        mockJugador.Jugadores.Add(CrearJugador(2, idPosicion: 2)); // defensor
        mockJugador.Jugadores.Add(CrearJugador(3, idPosicion: 2));
        mockJugador.Jugadores.Add(CrearJugador(4, idPosicion: 2));
        mockJugador.Jugadores.Add(CrearJugador(5, idPosicion: 2));

        mockJugador.Jugadores.Add(CrearJugador(6, idPosicion: 3));
        mockJugador.Jugadores.Add(CrearJugador(7, idPosicion: 3));
        mockJugador.Jugadores.Add(CrearJugador(8, idPosicion: 3));
        mockJugador.Jugadores.Add(CrearJugador(9, idPosicion: 3));

        mockJugador.Jugadores.Add(CrearJugador(10, idPosicion: 4));
        mockJugador.Jugadores.Add(CrearJugador(11, idPosicion: 4));


        var nuevoJugador = CrearJugador(12, idPosicion: 3); // mediocampista
        mockJugador.Jugadores.Add(nuevoJugador);

        var service = new PlantillaService(mockPlantilla, mockJugador);


        for (short i = 1; i <= 11; i++)
        {
            service.AgregarJugadorAPlantilla(idPlantilla: 1, idJugador: i, esTitular: true);
        }

        
        Assert.Throws<ArgumentException>(() => service.AgregarJugadorAPlantilla(1, 12, esTitular: true));

        
    }


    //actualizar jugador de la platilla plantilla
    [Fact]
    public void ActualizarJugadorEnPlantilla_valido()
    {
        var mockPlantilla = new MockRepoPlantilla();
        var mockJugador = new MockRepoJugador();
        var jugador = CrearJugador(1, idPosicion: 1);
        var service = new PlantillaService(mockPlantilla, mockJugador);

        mockPlantilla.Plantillas.Add(CrearPlantillaVacia(1));

        mockJugador.Jugadores.Add(jugador); // arquero

        

        service.AgregarJugadorAPlantilla(idPlantilla: 1, idJugador: 1, esTitular: true);

        Assert.True(mockPlantilla.SeLlamoAgregarJugadorAPlantilla);

        

        service.ActualizarJugadorEnPlantilla(idJugador:1, idPlantilla: 1, esTitular :false);

        Assert.True(mockPlantilla.SeLlamoActualizarJugadorEnPlantilla);

        Assert.NotNull(mockPlantilla.UltimoActualizado);

        // 3. Ahora puedes evaluar el valor de forma segura
        Assert.False(mockPlantilla.UltimoActualizado.Value.esTitular);

    }

    // jugadores titulares llena
    [Fact]
    public void ActualizarJugadorEnPlantilla_invalido_titilareslleno()
    {
        

        var mockPlantilla = new MockRepoPlantilla();
        var mockJugador = new MockRepoJugador();
        var jugador = CrearJugador(20, idPosicion: 4);
        var service = new PlantillaService(mockPlantilla, mockJugador);

        mockPlantilla.Plantillas.Add(CrearPlantillaVacia(1));

        mockJugador.Jugadores.Add(CrearJugador(1, idPosicion: 1)); // arquero

        mockJugador.Jugadores.Add(CrearJugador(2, idPosicion: 2)); // defensor
        mockJugador.Jugadores.Add(CrearJugador(3, idPosicion: 2));
        mockJugador.Jugadores.Add(CrearJugador(4, idPosicion: 2));
        mockJugador.Jugadores.Add(CrearJugador(5, idPosicion: 2));

        mockJugador.Jugadores.Add(CrearJugador(6, idPosicion: 3));
        mockJugador.Jugadores.Add(CrearJugador(7, idPosicion: 3));
        mockJugador.Jugadores.Add(CrearJugador(8, idPosicion: 3));
        mockJugador.Jugadores.Add(CrearJugador(9, idPosicion: 3));

        mockJugador.Jugadores.Add(CrearJugador(10, idPosicion: 4));
        mockJugador.Jugadores.Add(CrearJugador(11, idPosicion: 4));

        for (short i = 1; i <= 11; i++)
        {
            service.AgregarJugadorAPlantilla(idPlantilla: 1, idJugador: i, esTitular: true);
        }
        

        mockJugador.Jugadores.Add(jugador); 

        service.AgregarJugadorAPlantilla(idPlantilla: 1, idJugador: 20, esTitular: false);

        

        Assert.True(mockPlantilla.SeLlamoAgregarJugadorAPlantilla);


        Assert.Throws<ArgumentException>(() => service.ActualizarJugadorEnPlantilla(1, 20, esTitular: true));

        Assert.False(mockPlantilla.SeLlamoActualizarJugadorEnPlantilla);

        Assert.False(mockPlantilla.UltimoAgregado.Value.esTitular);
    }

}
    