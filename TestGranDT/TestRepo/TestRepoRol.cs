//test de repositorio de roles
using System;
using System.Collections.Generic;
using GranDT.Core.Model;
using GranDT.Core.Model.IRepos;
using GranDT.Core.Gran_DT.ConDapper;
using Xunit;
using MySqlConnector;

namespace TestGranDT.TestRepo;

public class TestRepoRol
{
    private readonly IRepoRol _repoRol;

    public TestRepoRol()
    {
        var cadena = "Server=localhost;Database=bd_GranET;Uid=root;Pwd=1001;";
        var conexion = new MySqlConnection(cadena);
        _repoRol = new RepoRol(conexion);
    }

    [Fact]
    public void ObtenerRoles_DevuelveListaDeRoles()
    {
        // Act
        var roles = _repoRol.ObtenerRoles();

        // Assert
        Assert.NotNull(roles);
        Assert.IsAssignableFrom<IEnumerable<Rol>>(roles);
        Assert.NotEmpty(roles);
        Assert.Contains(roles, r => r.IdRol > 0 && !string.IsNullOrWhiteSpace(r.Nombre));
        Assert.Contains(roles, r => r.Nombre == "Usuario");
    }

    [Fact]
    public void ObtenerRolPorId_DevuelveRolExistente()
    {
        // Arrange
        byte idRolExistente = 1;

        // Act
        var rol = _repoRol.ObtenerRolPorId(idRolExistente);

        // Assert
        Assert.NotNull(rol);
        Assert.Equal(idRolExistente, rol.IdRol);
        Assert.Equal("Usuario", rol.Nombre);
    }

    [Fact]
    public void ObtenerRolPorId_DevuelveNullParaRolInexistente()
    {
        // Arrange
        byte idRolInexistente = 90;

        // Act
        var rol = _repoRol.ObtenerRolPorId(idRolInexistente);

        // Assert
        Assert.Null(rol);
    }

    

}