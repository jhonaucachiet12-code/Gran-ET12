// controller para el manejo de los jugadores
using GranDT.Core.Gran_DT.ConDapper;
using GranDT.Core.Model;
using GranDT.Core.src.Services;
using Microsoft.AspNetCore.Mvc;
using MinimalAPI.Services;

namespace MinimalAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlantillaController : ControllerBase
{
    private readonly PlantillaService _plantillaService;

    public PlantillaController(PlantillaService plantillaService)
    {
        _plantillaService = plantillaService;
    }

    //---------------------Plantilla--------------------

    [HttpGet]
    public ActionResult<IEnumerable<Plantilla>> ObtenerPlantillas()
    {
        var plantillas = _plantillaService.ObtenerPlantillas();
        return Ok(plantillas);
    }

    [HttpGet("{id}")]
    public ActionResult<Plantilla> ObtenerPlantillaPorId(byte id)
    {
        var plantilla = _plantillaService.ObtenerPlantillaPorId(id);
        return plantilla is not null ? Ok(plantilla) : NotFound();
    }

    [HttpGet("{id}/usuario")]
    public ActionResult<IEnumerable<Plantilla>> ObtenerPlantillasDelUsuario(short id)
    {
        try
        {
            var plantillas = _plantillaService.ObtenerPlantillasDelUsuario(id);
            return Ok(plantillas);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost("{idUsuario}")]
    public ActionResult AgregarPlantilla(short idUsuario, RegistroPlantillaRequest request)
    {
        try
        {
            var plantilla = new Plantilla
            {
                IdUsuario = idUsuario,
                Nombre = request.Nombre,
                
                
            };
            
            _plantillaService.AgregarPlantilla(plantilla);
            return CreatedAtAction(nameof(ObtenerPlantillaPorId), new { id = plantilla.IdPlantilla }, plantilla);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPut("{id},{idusuario}")]
    public ActionResult ActualizarPlantilla(int id,short idusuario, RegistroPlantillaRequest request)
    {
        try
        {
            var plantilla = new Plantilla
            {

                IdPlantilla = id,
                IdUsuario =idusuario,
                Nombre = request.Nombre,
                
                
            };
            
            _plantillaService.ActualizarPlantilla(plantilla);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }

        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }

        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);      // 409: el nombre ya existe
        }
    }

    [HttpDelete("{id}")]
    public ActionResult EliminarPlantilla(byte id)
    {
        try
        {
            _plantillaService.EliminarPlantilla(id);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

        //---------------------PlantillaJugador--------------------

   [HttpPost("agregar-jugador")]
public ActionResult AgregarJugadorAPlantilla([FromBody] AgregarJugadorPlantillaRequest request)
{
    try
    {
        // Llamas al servicio pasando únicamente el objeto request (o sus propiedades)
        _plantillaService.AgregarJugadorAPlantilla(request.IdPlantilla, request.IdJugador, request.EsTitular);
        
        return CreatedAtAction(nameof(ObtenerPlantillaPorId), new { id = request.IdPlantilla }, null);
    }
    catch (ArgumentException ex)
    {
        return BadRequest(ex.Message);
    }
    catch (InvalidOperationException ex)
    {
        return BadRequest(ex.Message);
    }
}

    [HttpPut("actualizar-jugador")]
    public ActionResult ActualizarJugadorEnPlantilla([FromBody] AgregarJugadorPlantillaRequest request)
    {
        try
        {
            _plantillaService.ActualizarJugadorEnPlantilla(request.IdPlantilla, request.IdJugador, request.EsTitular);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
        
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{idJugador}/{idPlantilla}")]
    public ActionResult EliminarJugadorDePlantilla(short idJugador, int idPlantilla)
    {
        try
        {
            _plantillaService.EliminarJugadorDePlantilla(idJugador, idPlantilla);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("plantilla/{idPlantilla}")]
    public ActionResult<Plantilla> ObtenerJugadoresDeLaPlantilla(int idPlantilla)
    {
        var plantilla = _plantillaService.ObtenerJugadoresDeLaPlantilla(idPlantilla);
        return plantilla is not null ? Ok(plantilla) : NotFound();
    }

    [HttpGet("{idPlantilla}/{fecha}")]
    public ActionResult<decimal> ObtenersumaDeLasPuntuacionesDeLosTitularesDeLaPlantilla(int idPlantilla, byte fecha)
    {
        var suma = _plantillaService.ObtenersumaDeLasPuntuacionesDeLosTitularesDeLaPlantilla(idPlantilla, fecha);
        return Ok(suma);
    }

    [HttpGet("valorTotal/{idPlantilla}")]
    public ActionResult<decimal> ObtenerElValorTotalDeLaPlantilla(int idPlantilla)
    {
        var valorTotal = _plantillaService.ObtenerElValorTotalDeLaPlantilla(idPlantilla);
        return Ok(valorTotal);
    }

    
}

public class RegistroPlantillaRequest
{
    public required string Nombre { get; set; }
    
}

public class AgregarJugadorPlantillaRequest
{
    public int IdPlantilla { get; set; }
    public short IdJugador { get; set; }
    public bool EsTitular { get; set; }
}