using GranDT.Core.Model;
using GranDT.Core.Model.IRepos;
using Xunit;
using MinimalAPI.Services;
using GranDT.Core.src.Services;

namespace TestGranDT.TestsServices;

public class MockRepoPosicion:IRepoPosicion
{
    public List<Posicion> posiciones { get; set; } = new();

    // Flags para verificar si un método fue llamado 
    public bool SeLlamoObtenerPosiciones { get; private set; }
    public bool SeLlamoAgregarPosiciones { get; private set; }
    public bool SeLlamoActualizarPosiciones { get; private set; }
    public bool SeLlamoEliminarPosiciones { get; private set; }

    // Guarda el último Posiciones pasado a cada método
    public Posicion? UltimoPosicionesAgregado { get; private set; }
    public Posicion? UltimoPosicionesActualizado { get; private set; }
    public byte? UltimoIdEliminado { get; private set; }

    public IEnumerable<Posicion> ObtenerPosiciones()
    {
        SeLlamoObtenerPosiciones = true;
        return posiciones;
    }

    public Posicion ? ObtenerPosicionPorId(byte idPosicion)
    {
        return posiciones.FirstOrDefault(e => e.IdPosicion == idPosicion);
    }

    public void AgregarPosicion(Posicion posicion)
    {
        SeLlamoAgregarPosiciones = true;
        UltimoPosicionesAgregado = posicion;
        posiciones.Add(posicion);
    }

     public void ActualizarPosicion(Posicion posicion)
    {
        SeLlamoActualizarPosiciones = true;
        UltimoPosicionesActualizado = posicion;

        var existente = posiciones.FirstOrDefault(P => P.IdPosicion == posicion.IdPosicion);
        if (existente != null)
        {
            existente.Nombre = posicion.Nombre;
        }
    }

    public void EliminarPosicion(byte idPosicion)
    {
        SeLlamoEliminarPosiciones = true;
        UltimoIdEliminado = idPosicion;

        var existente = posiciones.FirstOrDefault(e => e.IdPosicion == idPosicion);
        if (existente != null)
        {
            posiciones.Remove(existente);
        }
    }


    
}


// Test

public class TestServicePosicion
{

    private static  Posicion CrearPosicionValida(byte id = 1, string nombre = "algo") => new()
    {
        IdPosicion = id,
        Nombre = nombre
    };


    //obtener Posiciones
    [Fact]
    public void ObtenerPoscisiones_Valido()
    {
        var mockRepo = new MockRepoPosicion();
        mockRepo.posiciones.Add(CrearPosicionValida(1 , "Arquero"));
        mockRepo.posiciones.Add(CrearPosicionValida(2 , "vodrio"));

        var service = new PosicionService(mockRepo);

        var resultado = service.ObtenerPosiciones();

        Assert.Contains(resultado,r => r.Nombre == "Arquero" );
        Assert.True(mockRepo.SeLlamoObtenerPosiciones);
    }
    [Fact]
    //Obtener por id
    public void obtenrPosicionPorId_valido()
    {
        var mockRepo = new MockRepoPosicion();
        mockRepo.posiciones.Add(CrearPosicionValida(1 , "Arquero"));

        var service = new PosicionService(mockRepo);

        var resultado  = service.ObtenerPosicionPorId(1);

        //Assert.Contains(resultado , r => r.Nombre == "Arquero");
        Assert.True ("Arquero" == resultado ?.Nombre);

    }

    [Fact]
    public void obtenrPosicionPorId_Invalido_idCero()
    {

        var mockRepo = new MockRepoPosicion();
        var service = new PosicionService(mockRepo);

        Assert.Throws<ArgumentOutOfRangeException>(() => service.ObtenerPosicionPorId(0));
    }

    // Agrgar Posicion

    [Fact]

    public void AgregarPosicion_Valido()
    {
        var mockRepo = new MockRepoPosicion();
        var service = new PosicionService(mockRepo);
        var posicion = CrearPosicionValida(1 , "alog");

        service.AgregarPosicion(posicion);

        var resultado = service.ObtenerPosiciones();

        Assert.Contains(resultado, r => r.Nombre == "alog");

        Assert.True(mockRepo.SeLlamoAgregarPosiciones);
    } 

    [Fact]

    public void AgregarPosicion_InValido_faltaNombre()
    {
        var mockRepo = new MockRepoPosicion();
        var service = new PosicionService(mockRepo);
        var posicion = CrearPosicionValida(1,"");
        

        
        Assert.Throws<ArgumentException>(() => service.AgregarPosicion(posicion));

        Assert.False(mockRepo.SeLlamoAgregarPosiciones);
    }

    [Fact]

     public void AgregarPosicion_Invalido_duplicado()
    {
        var mockRepo = new MockRepoPosicion();
        var service = new PosicionService(mockRepo);
        mockRepo.posiciones.Add(CrearPosicionValida(1 , "Arquero"));
        var posicion = CrearPosicionValida(2,"Arquero");

        Assert.Throws<ArgumentException>(() => service.AgregarPosicion(posicion));
        Assert.False(mockRepo.SeLlamoAgregarPosiciones);
    } 

    // Actualizar Posicion

    [Fact]
    public void ActualizarPosicion_valido()
    {
        var mockRepo = new MockRepoPosicion();
        var service = new PosicionService(mockRepo);
        mockRepo.posiciones.Add(CrearPosicionValida(1 , "Arquero"));

        var actualizado = CrearPosicionValida(1);

        actualizado.Nombre = "ti";

        service.ActualizarPosicion(actualizado);
        var resultado  = service.ObtenerPosicionPorId(1);

        Assert.True(mockRepo.SeLlamoActualizarPosiciones);
        Assert.Contains("ti", resultado?.Nombre);
    }


    //EliminarJugador

    [Fact]
    public void eliminarJugadorDePlantilla_Valido()
    {
        var mockRepo = new MockRepoPosicion();
        var service = new PosicionService(mockRepo);
        mockRepo.posiciones.Add(CrearPosicionValida(1 , "Arquero"));

        service.EliminarPosicion(1);

        Assert.True(mockRepo.SeLlamoEliminarPosiciones);
    }

    [Fact]

    public void EliminarJugadorDePlantilla_invalido_idCero()
    {
        var mockRepo = new MockRepoPosicion();
        var service = new PosicionService(mockRepo);
        
        Assert.Throws<ArgumentOutOfRangeException>(() => service.EliminarPosicion(0));
        Assert.False(mockRepo.SeLlamoEliminarPosiciones);
    }
}
