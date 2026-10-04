using GranDT.Core.Gran_DT.ConDapper;
using GranDT.Core.Model;
using GranDT.Core.src.Services;
using Microsoft.AspNetCore.Mvc;
using MinimalAPI.Services;

namespace MinimalAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PuntuacionController : ControllerBase
{
    private readonly PuntuacionService _puntuacionService;

    public PuntuacionController(PuntuacionService puntuacionService)
    {
        _puntuacionService = puntuacionService;
    }
    
    [HttpGet]
    [HttpGet]
    public ActionResult<IEnumerable<Puntuacion>> ObtenerPuntuasiones()
    {
        var puntuaciones = _puntuacionService.ObtenerPuntuasiones();
        return Ok(puntuaciones);
    }

    [HttpGet("{id}/{Fecha}")]
    public ActionResult<Puntuacion> ObtenerLaPuntucionDelJugador(short id, byte Fecha)
    {
        var puntuacion = _puntuacionService.ObtenerLaPuntucionDelJugador(id, Fecha);
        if (puntuacion == null)
        {
            return NotFound();
        }
        return Ok(puntuacion);
    }

    [HttpGet("{id}")]
    public ActionResult<IEnumerable<Puntuacion>> ObtenerTodasLasPuntuasionesDelJugador(short id)
    {
        var puntuaciones = _puntuacionService.ObtenerTodasLasPuntuasionesDelJugador(id);
        if(puntuaciones == null)
        {
            return NotFound();
        }
        return Ok(puntuaciones);
    }

    [HttpPost]
    public ActionResult AgregarPuntuacion (RegistroPuntuacionesRequest request)
    {
        try
        {
            var puntuacion = new Puntuacion
            {
                IdJugador = request.IdJugador,
                Fecha = request.Fecha,
                Puntuaciones = request.Puntuaciones
            };
            _puntuacionService.AgregarPuntuacion(puntuacion);
            return CreatedAtAction(nameof(ObtenerTodasLasPuntuasionesDelJugador), new { id = puntuacion.IdJugador }, puntuacion);
        }
        catch (ArgumentException ex)
        {

            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    public ActionResult ActualizarPuntuacion (Puntuacion puntuacion)
    {
        try
        {
            _puntuacionService.ActualizarPuntuacion(puntuacion);
            return NoContent();
        }
        catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public ActionResult EliminarPuntuacion(short id)
    {
        try
        {
            _puntuacionService.EliminarPuntuacion(id);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

}

public record RegistroPuntuacionesRequest
(
    short IdJugador,
    byte Fecha ,
    decimal Puntuaciones
);
