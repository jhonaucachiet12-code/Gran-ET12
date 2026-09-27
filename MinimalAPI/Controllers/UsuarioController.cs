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
    public ActionResult RegistrarUsario(Usuario usuario, string PasswordHash)
    {
        _usuarioService.RegistrarUsario(usuario, PasswordHash);
        return CreatedAtAction(nameof(ObtenerPorEmail), new { id = usuario.IdUsuario }, usuario);
    }

    [HttpPut("{id}")]
    public ActionResult ActualizarUsuario(short id, Usuario usuario)
    {
        if (id != usuario.IdUsuario)
        {
            return BadRequest();
        }

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