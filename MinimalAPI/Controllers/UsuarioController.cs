//controller para usuarios
using GranDT.Core.Model;
using GranDT.Core.src.Services;
using Microsoft.AspNetCore.Mvc;

namespace MinimalAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly UsuarioService _usuarioService;

    public UsuarioController(UsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Usuario>> ObtenerUsuarios()
    {
        var usuarios = _usuarioService.ObtenerUsuarios();
        return Ok(usuarios);
    }

    [HttpGet("{id}")]
    public ActionResult<Usuario> ObtenerPorEmail(short id)
    {
        try
        {
            var usuario = _usuarioService.ObtenerPorEmail(id);
            return Ok(usuario);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        
        
    }

    [HttpPost("{idrol}")]
    public ActionResult RegistrarUsuario(byte idrol,RegistroUsuarioRequest request)
    {
        try
        {
            
            var usuario = new Usuario
            {
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                Email = request.Email,
                FechaNacimiento = request.FechaNacimiento,
                IdRol = idrol,
                PasswordHash = "temp",
                
            };

            _usuarioService.RegistrarUsario(usuario, request.PasswordHash);
            return CreatedAtAction(nameof(ObtenerPorEmail), new { id = usuario.IdUsuario }, usuario);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);      // 409: el nombre ya existe
        }
    }

    [HttpPut("{id}/ {idrol}")]
    public ActionResult ActualizarUsuario(short id,byte idrol, actualizarUsuarioRequest request)
    {
        try
        {
            var usuario1 =_usuarioService.ObtenerPorEmail(id);
            if (usuario1 is null)
            {
                return NotFound("No se encuentra el usuario");
            }
            var usuario = new Usuario
            {
                IdUsuario = id,
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                Email = usuario1.Email,
                IdRol = idrol,
                FechaNacimiento = request.FechaNacimiento,
                PasswordHash =request.PasswordHash

            };
            _usuarioService.ActualizarUsuario(usuario);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);      // 409: el nombre ya existe
        }
       

        
    }

    [HttpDelete("{id}")]
    public ActionResult EliminarUsuario(short id)
    {
        try
        {
            _usuarioService.EliminarUsuario(id);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        
    }
}


