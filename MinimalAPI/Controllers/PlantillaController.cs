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
    public ActionResult<Plantilla> ObtenerPlantillaPorId(int id)
    {
        try
        {
            var plantilla = _plantillaService.ObtenerPlantillaPorId(id);
            return plantilla is not null ? Ok(plantilla) : NotFound();
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
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);      // 409: el nombre ya existe
        }
    }

    [HttpPut("{id}/{idusuario}")]
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
            return BadRequest(ex.Message); //400
        }

        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message); //404
        }

        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);      // 409: el nombre ya existe
        }
    }

    [HttpDelete("{id}")]
    public ActionResult EliminarPlantilla(int id)
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
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

        //---------------------PlantillaJugador--------------------

   [HttpPost("{idPlantilla}/agregar-jugador/{idjugador}")]
public ActionResult AgregarJugadorAPlantilla(int idPlantilla,short idjugador,[FromBody] AgregarJugadorPlantillaRequest request)
{
    try
    {
        // Llamas al servicio pasando únicamente el objeto request (o sus propiedades)
        _plantillaService.AgregarJugadorAPlantilla(idPlantilla, idjugador, request.EsTitular);
        
        return CreatedAtAction(nameof(ObtenerPlantillaPorId), new { id = idPlantilla }, null);
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

    [HttpPut("{IdPlantilla}/actualizar-jugador/{idfutbolista}" )]
    public ActionResult ActualizarJugadorEnPlantilla(int IdPlantilla, short idfutbolista ,[FromBody] ActualizarJugadorPlantillaRequest request)
    {
        try
        {
            _plantillaService.ActualizarJugadorEnPlantilla( IdPlantilla, idfutbolista, request.EsTitular);
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

    [HttpDelete("{idPlantilla}/jugadores/{idJugador}")]
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
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("plantilla/{idPlantilla}")]
    public ActionResult<Plantilla> ObtenerJugadoresDeLaPlantilla(int idPlantilla)
    {
        try
        {
            var plantilla = _plantillaService.ObtenerJugadoresDeLaPlantilla(idPlantilla);
            return plantilla is not null ? Ok(plantilla) : NotFound();
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

    [HttpGet("{idPlantilla}/{fecha}")]
    public ActionResult<decimal> ObtenersumaDeLasPuntuacionesDeLosTitularesDeLaPlantilla(int idPlantilla, byte fecha)
    {
        try
        {
            var suma = _plantillaService.ObtenersumaDeLasPuntuacionesDeLosTitularesDeLaPlantilla(idPlantilla, fecha);
            return Ok(suma);
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

    [HttpGet("valorTotal/{idPlantilla}")]
    public ActionResult<decimal> ObtenerElValorTotalDeLaPlantilla(int idPlantilla)
    {
        try
        {
            var valorTotal = _plantillaService.ObtenerElValorTotalDeLaPlantilla(idPlantilla);
            return Ok(valorTotal);
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

    
}

