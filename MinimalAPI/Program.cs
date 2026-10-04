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

builder.Services.AddScoped<IRepoPosicion, RepoPosicion>();
builder.Services.AddScoped<PosicionService>();

builder.Services.AddScoped<IRepoJugador, RepoJugador>();
builder.Services.AddScoped<JugadorService>();

builder.Services.AddScoped<IRepoPuntuacion, RepoPuntuacion>();
builder.Services.AddScoped<PuntuacionService>();

builder.Services.AddScoped<IRepoPlantilla, RepoPlantilla>();
builder.Services.AddScoped<PlantillaService>();

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
app.MapControllers();



app.Run();



//ruta ami base de datos:
//  "DefaultConnection": "Server=localhost;Database=bd_GranET;Uid=root;Pwd=1001;"

// ruta alternativa a mi base de datos:
//  "DefaultConnection": "Server=localhost;Database=bd_GranET;Uid=5to_agbd;Pwd=Trigg3rs!;"