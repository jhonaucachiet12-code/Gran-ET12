/*using GranDT.Core.Gran_DT.ConDapper;
using GranDT.Core.Model;
using GranDT.Core.src.Services;
using Microsoft.AspNetCore.Mvc;
using MinimalAPI.Services;

namespace MinimalAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlantillaJugadorController : ControllerBase
{
     private readonly PlantillaService _plantillaService;

    public PlantillaJugadorController(PlantillaService plantillaService)
    {
        _plantillaService = plantillaService;
    }
        //---------------------PlantillaJugador--------------------

    [HttpPost]
    public ActionResult AgregarJugadorAPlantilla(Plantilla plantilla, Jugador jugador,int idPlantilla, short idJugador, bool esTitular)
    {
        try
        {
             _plantillaService.AgregarJugadorAPlantilla(plantilla, jugador, idPlantilla, idJugador, esTitular);
            return CreatedAtAction(nameof(_plantillaService.ObtenerPlantillaPorId), new { id = idPlantilla }, null);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    public ActionResult ActualizarJugadorEnPlantilla(Plantilla plantilla, Jugador jugador,int idPlantilla, short idJugador, bool esTitular)
    {
        try
        {
            _plantillaService.ActualizarJugadorEnPlantilla(plantilla, jugador, idPlantilla, idJugador, esTitular);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
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

    [HttpGet("{idPlantilla}")]
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

*/