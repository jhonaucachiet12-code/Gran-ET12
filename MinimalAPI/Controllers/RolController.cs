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
        var rol = _RepoService.ObtenerRolPorId(id);
        if (rol == null)
        {
            return NotFound();
        }
        return rol is not null ? Ok(rol) : NotFound();
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
        
        
    }

    [HttpPut]
    public ActionResult ActualizarRol( Rol rol)
    {

        try
        {
            _RepoService.ActualizarRol(rol);
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
            _RepoService.EliminarRol(id);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

}

public class RegistroRolRequest()
{
    public required string Nombre {get;set;}
}
    
