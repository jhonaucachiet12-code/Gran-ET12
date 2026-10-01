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
        return equipo is not null ? Ok(equipo) : NotFound();
    }

    [HttpPost]
    public ActionResult RegistrarEquipo(Equipo equipo)
    {
        try
        {
            _equipoService.AgregarEquipo(equipo);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = equipo.IdEquipo }, equipo);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public ActionResult ActualizarEquipo(byte id, Equipo equipo)
    {
        if (id != equipo.IdEquipo)
        {
            return BadRequest();
        }

        try
        {
            _equipoService.ActualizarEquipo(equipo);
            return NoContent();
        }
        catch (ArgumentException ex)
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
