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
    public ActionResult<Jugador> ObtenerPorId(byte id)
    {
        var jugador = _jugadorService.ObtenerJugadorPorId(id);
        return jugador is not null ? Ok(jugador) : NotFound();
    }
    
    [HttpPost]
    public ActionResult AgregarJugador(RegistroJugadorRequest request)
    {
        try
        {
            var jugador = new Jugador
            {
                IdEquipo = request.IdEquipo,
                IdPosicion = request.IdPosicion,
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
    }

    [HttpPut]
    public ActionResult ActualizarJugador(Jugador jugador)
    {
        try
        {
            _jugadorService.ActualizarJugador(jugador);
            return NoContent();
        }
        catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
        {
            return BadRequest(ex.Message);
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
    }
}
public record RegistroJugadorRequest(
    byte IdPosicion,
    byte IdEquipo,
    string Nombre,
    string Apellido,
    string Apodo,
    DateTime Nacimiento,
    decimal Cotización
);


