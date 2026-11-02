using System.Security.Cryptography.X509Certificates;
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
    private readonly RolService  _RepoService;

    public RolController(RolService repoService)
    {
        _RepoService = repoService;
    }
    

    [HttpGet]

    public ActionResult<IEnumerable<Rol>> ObtenerRoles()
    {
        var roles = _RepoService.ObtenerRoles();
        return Ok(roles);
    }

    [HttpGet("{id}")]
    public ActionResult<Rol> ObtenerRolPorId(byte id)
    {
        try
        {
            var equipo = _RepoService.ObtenerRolPorId(id);
             return  Ok(equipo);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch(KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public ActionResult AgregarRol(RegistroRolRequest request)
    {
       try
        {
            var rol = new Rol
            {
                Nombre = request.Nombre
            };
            _RepoService.AgregarRol(rol);
            return CreatedAtAction(nameof(ObtenerRolPorId), new { id = rol.IdRol }, rol);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch(KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
        
    }

    [HttpPut("{id}")]
    public ActionResult ActualizarRol(byte id,  RegistroRolRequest request)
    {

        try
        {
            var rol = new Rol
            {
                IdRol = id,
                Nombre = request.Nombre
            };
            _RepoService.ActualizarRol(rol);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch(KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public ActionResult EliminarRol(byte id)
    {
        try
        {
            _RepoService.EliminarRol(id);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch(KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

}


    
