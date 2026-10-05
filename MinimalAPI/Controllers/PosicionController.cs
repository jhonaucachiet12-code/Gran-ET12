// Controladores de la entidad Posicion 
using GranDT.Core.Model;
using GranDT.Core.src.Services;
using Microsoft.AspNetCore.Mvc;
using MinimalAPI.Services;

namespace MinimalAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PosicionController : ControllerBase
{
    
    private readonly PosicionService _posicionService;

    public PosicionController(PosicionService posicionService)
    {
        _posicionService = posicionService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Posicion>> ObtenerPosiciones()
    {
        var posiciones = _posicionService.ObtenerPosiciones();
        return Ok(posiciones);
    }

    [HttpGet("{id}")]
    public ActionResult<Posicion> ObtenerPorId(byte id)
    {
        var posicion = _posicionService.ObtenerPosicionPorId(id);
        return posicion is not null ? Ok(posicion) : NotFound();
    }

    [HttpPost]
    public ActionResult RegistrarPosicion(RegistroPosicionRequest request)
    {
        try
        {
            var posicion = new Posicion
            {
                Nombre = request.Nombre
            };
            _posicionService.AgregarPosicion(posicion);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = posicion.IdPosicion }, posicion);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    public ActionResult ActualizarPosicion( Posicion posicion)
    {


        try
        {
           
            _posicionService.ActualizarPosicion(posicion);
            return NoContent();
        }
        catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public ActionResult EliminarPosicion(byte id)
    {
        try
        {
            _posicionService.EliminarPosicion(id);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}



public class RegistroPosicionRequest()
{
    public required string Nombre{get;set;}
}
