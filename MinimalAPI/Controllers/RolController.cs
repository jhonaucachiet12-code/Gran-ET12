using GranDT.Core.Gran_DT.ConDapper;
using GranDT.Core.Model;
using GranDT.Core.src.Services;
using Microsoft.AspNetCore.Mvc;
using MinimalAPI.Services;

namespace MinimalAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RolController : ControllerBase
{
    private readonly RepoRol  _repoRol;

    public RolController(RepoRol repoRol)
    {
        _repoRol = repoRol;
    }

    [HttpGet]

    public ActionResult<IEnumerable<Rol>> ObtenerRoles()
    {
        var roles = _repoRol.ObtenerRoles();
        return Ok(roles);
    }

    [HttpGet("{id}")]
    public ActionResult<Rol> ObtenerRolPorId(byte id)
    {
        var rol = _repoRol.ObtenerRolPorId(id);
        if (rol == null)
        {
            return NotFound();
        }
        return rol is not null ? Ok(rol) : NotFound();
    }

    [HttpPost]
    public ActionResult AgregarRol(Rol rol)
    {
       
            _repoRol.AgregarRol(rol);
            return CreatedAtAction(nameof(ObtenerRolPorId), new { id = rol.IdRol }, rol);
        
    }

    [HttpPut("{id}")]
    public ActionResult ActualizarRol(byte id, Rol rol)
    {
        if (id != rol.IdRol)
        {
            return BadRequest();
        }

        try
        {
            _repoRol.ActualizarRol(rol);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public ActionResult EliminarRol(byte id)
    {
        try
        {
            _repoRol.EliminarRol(id);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }


    
}