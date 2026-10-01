// configuración de la aplicación minimal api con scalar

using Scalar.AspNetCore;
using GranDT.Core.Model;
using GranDT.Core.src.Services;
using GranDT.Core.Gran_DT.ConDapper;
using GranDT.Core.Model.IRepos;
using MySqlConnector;
using Dapper;
using System.Data;
using MinimalAPI.Services;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers(); // ← agregar esto

builder.Services.AddScoped<IDbConnection>(sp =>
    new MySqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));
// enpont de Usuario
builder.Services.AddScoped<IRepoUsuario, RepoUsuario>();
builder.Services.AddScoped<UsuarioService>();

// enpont de equipo
builder.Services.AddScoped<IRepoEquipo, RepoEquipo>();
builder.Services.AddScoped<EquipoService>();

// enpont de rol
builder.Services.AddScoped<IRepoRol,RepoRol>();
builder.Services.AddScoped<RolService>();
builder.Services.AddScoped<RepoRol>();

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
app.MapControllers();

app.MapGet("/usuarios", (UsuarioService service) =>
{
    var usuarios = service.ObtenerUsuarios();
    return Results.Ok(usuarios);
});

app.MapGet("/usuarios/{id}", (short id, UsuarioService service) =>
{
    try
    {
        var usuario = service.ObtenerPorEmail(id);
        return usuario is not null ? Results.Ok(usuario) : Results.NotFound();
    }
    catch (ArgumentOutOfRangeException ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

app.MapPost("/usuarios", (RegistroUsuarioRequest request, UsuarioService service) =>
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
            PasswordHash = "temp",      // se sobreescribe dentro del service
            Roles = new Rol { Nombre = "" } // placeholder, no se usa para el insert
        };

        service.RegistrarUsario(usuario, request.PasswordHash);
        return Results.Created($"/usuarios/{usuario.IdUsuario}", usuario);
    }
    catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
    {
        return Results.BadRequest(ex.Message);
    }
});

app.MapPut("/usuarios", (Usuario usuario, UsuarioService service) =>
{
    try
    {
        service.ActualizarUsuario(usuario);
        return Results.NoContent();
    }
    catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
    {
        return Results.BadRequest(ex.Message);
    }
});

app.MapDelete("/usuarios/{id}", (short id, UsuarioService service) =>
{
    service.EliminarUsuario(id);
    return Results.NoContent();
});

app.Run();

public record RegistroUsuarioRequest(
    string Nombre,
    string Apellido,
    string Email,
    DateTime FechaNacimiento,
    byte IdRol,
    string PasswordHash
);

//ruta ami base de datos:
//  "DefaultConnection": "Server=localhost;Database=bd_GranET;Uid=root;Pwd=1001;"

// ruta alternativa a mi base de datos:
//  "DefaultConnection": "Server=localhost;Database=bd_GranET;Uid=5to_agbd;Pwd=Trigg3rs!;"