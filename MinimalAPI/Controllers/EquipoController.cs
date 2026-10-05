using GranDT.Core.Model;
using GranDT.Core.src.Services;
using Microsoft.AspNetCore.Mvc;
using MinimalAPI.Services;

namespace MinimalAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EquipoController : ControllerBase
{
    private readonly EquipoService _equipoService;

    public EquipoController(EquipoService equipoService)
    {
        _equipoService = equipoService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Equipo>> ObtenerEquipos()
    {
        var equipos = _equipoService.ObtenerEquipos();
        return Ok(equipos);
    }

    [HttpGet("{id}")]
    public ActionResult<Equipo> ObtenerPorId(byte id)
    {
        var equipo = _equipoService.ObtenerEquipoPorId(id);
        if(equipo == null)
        {
            return NotFound();
        }
        return  Ok(equipo);
    }

    [HttpPost]
    public ActionResult RegistrarEquipo(RegistroEquipoRequest request)
    {
        try
        {
            var equipo = new Equipo
            {
                Nombre = request.Nombre
            };
            _equipoService.AgregarEquipo(equipo);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = equipo.IdEquipo }, equipo);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    public ActionResult ActualizarEquipo( Equipo equipo)
    {
        try
        {
            _equipoService.ActualizarEquipo(equipo);
            return NoContent();
        }
        catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public ActionResult EliminarEquipo(byte id)
    {
        try
        {
            _equipoService.EliminarEquipo(id);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}



public class RegistroEquipoRequest()
{
    public required string Nombre {get;set;}
}
