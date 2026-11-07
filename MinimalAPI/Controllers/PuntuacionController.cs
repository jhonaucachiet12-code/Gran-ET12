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
        try
        {
            var puntuacion = _puntuacionService.ObtenerLaPuntucionDelJugador(id, Fecha);
            return Ok(puntuacion);
        }
        catch (ArgumentException ex  )
        {

            return BadRequest(ex.Message);
        }  
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        
    }

    [HttpGet("{id}")]
    public ActionResult<IEnumerable<Puntuacion>> ObtenerTodasLasPuntuasionesDelJugador(short id)
    {
        try
        {
            var puntuaciones = _puntuacionService.ObtenerTodasLasPuntuasionesDelJugador(id);
            return Ok(puntuaciones);
        }
        catch (ArgumentException ex  )
        {

            return BadRequest(ex.Message);
        }  
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        
    }

    [HttpPost("{id}/{Fecha}")]
    public ActionResult AgregarPuntuacion (short id, byte Fecha, RegistroPuntuacionesRequest request)
    {
        try
        {
            var puntuacion = new Puntuacion
            {
                IdJugador = id,
                Fecha = Fecha,
                Puntuaciones = request.Puntuaciones
            };
            _puntuacionService.AgregarPuntuacion(puntuacion);
            return CreatedAtAction(nameof(ObtenerTodasLasPuntuasionesDelJugador), new { id = puntuacion.IdJugador }, puntuacion);
        }
        catch (ArgumentException ex  )
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

    [HttpPut("{id}/{Fecha}")]
    public ActionResult ActualizarPuntuacion (short id, byte Fecha, RegistroPuntuacionesRequest request)
    {
        try
        {
            var puntuacion = new Puntuacion
            {
                IdJugador = id,
                Fecha = Fecha,
                Puntuaciones = request.Puntuaciones
            };
            _puntuacionService.ActualizarPuntuacion(puntuacion);
            return NoContent();
        }
         catch (ArgumentException ex  )
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

    [HttpDelete("{id}/{Fecha}")]
    public ActionResult EliminarPuntuacion(short id, byte Fecha)
    {
        try
        {
            _puntuacionService.EliminarPuntuacion(id, Fecha);
            return NoContent();
        }
         catch (ArgumentException ex  )
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

}



public class RegistroPuntuacionesRequest()
{
    
    
    public decimal Puntuaciones{get;set;}
}