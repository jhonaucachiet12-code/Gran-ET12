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
        var usuario = _usuarioService.ObtenerPorEmail(id);
        if (usuario == null)
        {
            return NotFound();
        }
        return Ok(usuario);
    }

    [HttpPost]
    public ActionResult RegistrarUsuario(RegistroUsuarioRequest request)
    {
        try
        {
            var usuario = new Usuario
            {
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                Email = request.Email,
                FechaNacimiento = request.FechaNacimiento,
                IdRol = request.IdRol,
                PasswordHash = "temp",
                Roles = new Rol { Nombre = "" }
            };

            _usuarioService.RegistrarUsario(usuario, request.PasswordHash);
            return CreatedAtAction(nameof(ObtenerPorEmail), new { id = usuario.IdUsuario }, usuario);
        }
        catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    public ActionResult ActualizarUsuario( Usuario usuario)
    {
       

        _usuarioService.ActualizarUsuario(usuario);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult EliminarUsuario(short id)
    {
        _usuarioService.EliminarUsuario(id);
        return NoContent();
    }
}


public class RegistroUsuarioRequest()
{
    public required string Nombre{get;set;}
    public required string Apellido{get;set;}
    public required string Email{get;set;}
    public DateTime FechaNacimiento{get;set;}
    public byte IdRol {get;set;}
    public required string PasswordHash{get;set;}
}