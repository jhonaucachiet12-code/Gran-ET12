// controller para el manejo de los jugadores
using GranDT.Core.Gran_DT.ConDapper;
using GranDT.Core.Model;
using GranDT.Core.src.Services;
using Microsoft.AspNetCore.Mvc;
using MinimalAPI.Services;

namespace MinimalAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JugadorController : ControllerBase
{
    private readonly JugadorService _jugadorService;

    public JugadorController(JugadorService jugadorService)
    {
        _jugadorService = jugadorService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Jugador>> ObtenerJugadores()
    {
        var jugadores = _jugadorService.ObtenerJugadores();
        return Ok(jugadores);
    }

    [HttpGet("{id}")]
    public ActionResult<Jugador> ObtenerPorId(short id)
    {
        try
        {
            var jugador = _jugadorService.ObtenerJugadorPorId(id);
            return Ok(jugador);
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
    
    [HttpPost("{idEquipo}/{idPosicion}")]
    public ActionResult AgregarJugador(byte idEquipo ,byte idPosicion, RegistroJugadorRequest request)
    {
        try
        {
            var jugador = new Jugador
            {
                IdEquipo = idEquipo,
                IdPosicion = idPosicion,
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                Apodo = request.Apodo,
                Nacimiento = request.Nacimiento,
                Cotización = request.Cotización
            };
            
            _jugadorService.AgregarJugador(jugador);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = jugador.IdJugador }, jugador);
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

    [HttpPut("{id}/{idEquipo}/{idPosicion}")]
    
    public ActionResult ActualizarJugador(short id,byte idEquipo ,byte idPosicion, RegistroJugadorRequest request)
    {
        

        try
        {
            var jugador = new Jugador
            {
                IdJugador = id,
                IdEquipo = idEquipo,
                IdPosicion = idPosicion,
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                Apodo = request.Apodo,
                Nacimiento = request.Nacimiento,
                Cotización = request.Cotización
            };

            if(_jugadorService.ObtenerJugadorPorId(jugador.IdJugador) is null)
            {
                return NotFound();
            }
            
            _jugadorService.ActualizarJugador(jugador);
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
    public ActionResult EliminarJugador(short id)
    {
        try
        {
            _jugadorService.EliminarJugador(id);
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
}



