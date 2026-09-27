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

app.Run();
