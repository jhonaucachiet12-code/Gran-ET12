using GranDT.Core.Model;
using GranDT.Core.Model.IRepos;
using Xunit;
using MinimalAPI.Services;

namespace TestGranDT.TestsServices;

public class MockRepoEquipo : IRepoEquipo
{
    // Datos en memoria, simulando la "base de datos"
    public List<Equipo> Equipos { get; set; } = new();

    // Flags para verificar si un método fue llamado (espías simples)
    public bool SeLlamoObtenerEquipos { get; private set; }
    public bool SeLlamoAgregarEquipo { get; private set; }
    public bool SeLlamoActualizarEquipo { get; private set; }
    public bool SeLlamoEliminarEquipo { get; private set; }

    // Guarda el último equipo pasado a cada método, útil para asserts
    public Equipo? UltimoEquipoAgregado { get; private set; }
    public Equipo? UltimoEquipoActualizado { get; private set; }
    public byte? UltimoIdEliminado { get; private set; }

    public IEnumerable<Equipo> ObtenerEquipos()
    {
        SeLlamoObtenerEquipos = true;
        return Equipos;
    }

    public Equipo? ObtenerEquipoPorId(byte idEquipo)
    {
        return Equipos.FirstOrDefault(e => e.IdEquipo == idEquipo);
    }

    public void AgregarEquipo(Equipo equipo)
    {
        SeLlamoAgregarEquipo = true;
        UltimoEquipoAgregado = equipo;
        Equipos.Add(equipo);
    }

    public void ActualizarEquipo(Equipo equipo)
    {
        SeLlamoActualizarEquipo = true;
        UltimoEquipoActualizado = equipo;

        var existente = Equipos.FirstOrDefault(e => e.IdEquipo == equipo.IdEquipo);
        if (existente != null)
        {
            existente.Nombre = equipo.Nombre;
        }
    }

    public void EliminarEquipo(byte idEquipo)
    {
        SeLlamoEliminarEquipo = true;
        UltimoIdEliminado = idEquipo;

        var existente = Equipos.FirstOrDefault(e => e.IdEquipo == idEquipo);
        if (existente != null)
        {
            Equipos.Remove(existente);
        }
    }
}




// test

public class EquipoServiceTests
{
    

    [Fact]
    public void ObtenerEquipos_DevuelveListaDelRepo()
    {
        
        var mockRepo = new MockRepoEquipo();
        mockRepo.Equipos.Add(new Equipo { IdEquipo = 1, Nombre = "River" });
        mockRepo.Equipos.Add(new Equipo { IdEquipo = 2, Nombre = "Boca" });

        var service = new EquipoService(mockRepo);

        
        var resultado = service.ObtenerEquipos();

        
        Assert.Equal(2, resultado.Count());
        Assert.True(mockRepo.SeLlamoObtenerEquipos);
    }

    // --- ObtenerEquipoPorId ---

    [Fact]
    public void ObtenerEquipoPorId_IdValido_DevuelveEquipo()
    {
        var mockRepo = new MockRepoEquipo();
        mockRepo.Equipos.Add(new Equipo { IdEquipo = 1, Nombre = "River" });
        var service = new EquipoService(mockRepo);

        var resultado = service.ObtenerEquipoPorId(1);

        Assert.NotNull(resultado);
        Assert.True(resultado.IdEquipo > 0);
        Assert.Equal("River", resultado!.Nombre);
    }

    [Fact]
    public void ObtenerEquipoPorId_IdCero()
    {
        var mockRepo = new MockRepoEquipo();
        var service = new EquipoService(mockRepo);

        Assert.Throws<ArgumentOutOfRangeException>(() => service.ObtenerEquipoPorId(0));
    }

    

    // --- AgregarEquipo ---

    [Fact]
    public void AgregarEquipo_EquipoValido_LlamaAlRepo()
    {
        var mockRepo = new MockRepoEquipo();
        var service = new EquipoService(mockRepo);
        var nuevoEquipo = new Equipo { IdEquipo = 1, Nombre = "Independiente" };

        var equiposExistentes = service.ObtenerEquipos();
        Assert.DoesNotContain(nuevoEquipo, equiposExistentes);

        service.AgregarEquipo(nuevoEquipo);

        Assert.True(mockRepo.SeLlamoAgregarEquipo);
        Assert.Equal("Independiente", mockRepo.UltimoEquipoAgregado?.Nombre);
    }

    [Fact]
    public void AgregarEquipo_NombreVacio_LanzaArgumentException()
    {
        var mockRepo = new MockRepoEquipo();
        var service = new EquipoService(mockRepo);
        var equipoInvalido = new Equipo { IdEquipo = 1, Nombre = "" };

        Assert.Throws<ArgumentException>(() => service.AgregarEquipo(equipoInvalido));
        Assert.False(mockRepo.SeLlamoAgregarEquipo); // no debería haber llegado a llamar al repo
    }

    [Fact]
    public void AgregarEquipo_NombreDuplicado_LanzaArgumentException()
    {
        var mockRepo = new MockRepoEquipo();
        mockRepo.Equipos.Add(new Equipo { IdEquipo = 1, Nombre = "River" });
        var service = new EquipoService(mockRepo);

        var equipoDuplicado = new Equipo { IdEquipo = 2, Nombre = "River" };

        var ex = Assert.Throws<ArgumentException>(() => service.AgregarEquipo(equipoDuplicado));
        Assert.Contains("ya exite", ex.Message); // ojo: el mensaje real tiene el typo "exite"
        Assert.False(mockRepo.SeLlamoAgregarEquipo);
    }

    [Fact]
    public void AgregarEquipo_Null_LanzaArgumentNullException()
    {
        var mockRepo = new MockRepoEquipo();
        var service = new EquipoService(mockRepo);

        Assert.Throws<ArgumentNullException>(() => service.AgregarEquipo(null!));
    }

    // --- ActualizarEquipo ---

    [Fact]
    public void ActualizarEquipo_DatosValidos_LlamaAlRepo()
    {
        var mockRepo = new MockRepoEquipo();
        mockRepo.Equipos.Add(new Equipo { IdEquipo = 1, Nombre = "River" });
        var service = new EquipoService(mockRepo);

        var equipoActualizado = new Equipo { IdEquipo = 1, Nombre = "River Plate" };
        service.ActualizarEquipo(equipoActualizado);

        Assert.True(mockRepo.SeLlamoActualizarEquipo);
        Assert.Equal("River Plate", mockRepo.UltimoEquipoActualizado?.Nombre);
    }

    [Fact]
    public void ActualizarEquipo_IdInvalido_LanzaExcepcion()
    {
        var mockRepo = new MockRepoEquipo();
        var service = new EquipoService(mockRepo);

        var equipoInvalido = new Equipo { IdEquipo = 0, Nombre = "Test" };

        Assert.Throws<ArgumentOutOfRangeException>(() => service.ActualizarEquipo(equipoInvalido));
        Assert.False(mockRepo.SeLlamoActualizarEquipo);
    }

    // --- EliminarEquipo ---

    [Fact]
    public void EliminarEquipo_IdValido_LlamaAlRepo()
    {
        var mockRepo = new MockRepoEquipo();
        mockRepo.Equipos.Add(new Equipo { IdEquipo = 1, Nombre = "River" });
        var service = new EquipoService(mockRepo);

        service.EliminarEquipo(1);

        Assert.True(mockRepo.SeLlamoEliminarEquipo);
        Assert.Equal((byte)1, mockRepo.UltimoIdEliminado);
        Assert.Empty(mockRepo.Equipos);
    }

    [Fact]
    public void EliminarEquipo_IdCero_LanzaExcepcion()
    {
        var mockRepo = new MockRepoEquipo();
        var service = new EquipoService(mockRepo);

        Assert.Throws<ArgumentOutOfRangeException>(() => service.EliminarEquipo(0));
        Assert.False(mockRepo.SeLlamoEliminarEquipo);
    }

    // --- Constructor ---

    [Fact]
    public void Constructor_RepoNull_LanzaArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new EquipoService(null!));
    }
}


