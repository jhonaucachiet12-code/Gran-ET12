// configuración de la aplicación minimal api con scalar

using Scalar.AspNetCore;
using GranDT.Core.Model;
using GranDT.Core.src.Services;
using GranDT.Core.Gran_DT.ConDapper;
using GranDT.Core.Model.IRepos;
using MySqlConnector;
using Dapper;
using System.Data;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddScoped<IDbConnection>(sp =>
    new MySqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IRepoUsuario, RepoUsuario>();
builder.Services.AddScoped<UsuarioService>();


builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

// Endpoints de Usuario
app.MapGet("/usuarios", (UsuarioService service) =>
{
    var usuarios = service.ObtenerUsuarios();
    return Results.Ok(usuarios);
});

app.MapGet("/usuarios/{id}", (short id, UsuarioService service) =>
{
    var usuario = service.ObtenerPorEmail(id); // ojo, revisar nombre/lógica del método
    return usuario is not null ? Results.Ok(usuario) : Results.NotFound();
});

app.MapPost("/usuarios", (Usuario usuario, string passwordHash, UsuarioService service) =>
{
    service.RegistrarUsario(usuario, passwordHash);
    return Results.Created($"/usuarios/{usuario.IdUsuario}", usuario);
});

app.MapPut("/usuarios", (Usuario usuario, UsuarioService service) =>
{
    service.ActualizarUsuario(usuario);
    return Results.NoContent();
});

app.MapDelete("/usuarios/{id}", (short id, UsuarioService service) =>
{
    service.EliminarUsuario(id);
    return Results.NoContent();
});

app.Run();
