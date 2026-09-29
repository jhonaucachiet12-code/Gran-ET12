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
        //var cadena = "Server=localhost;Database=bd_GranET;Uid=root;Pwd=1001;";
        var cadena = "Server=localhost;Database=bd_GranET;Uid=5to_agbd;Pwd=Trigg3rs!;";
        var conexion = new MySqlConnection(cadena);
        _repoRol = new RepoRol(conexion);
    }

    [Fact]
    public void ObtenerRoles_DevuelveListaDeRoles()
    {
        // Act
        var usuarioTemoral = CrearRolTemporal();

        try
        {
            _repoRol.AgregarRol(usuarioTemoral);
            var roles = _repoRol.ObtenerRoles();
            Assert.NotNull(roles);
            Assert.IsAssignableFrom<IEnumerable<Rol>>(roles);
            Assert.Contains(roles, r => r.Nombre == "Usuario");

        }
        finally
        {
            BorrarRolTemporal(usuarioTemoral);
        }

        // Assert

    }

    [Fact]
    public void ObtenerRolPorId_DevuelveRolExistente()
    {
        var usuarioTemoral = CrearRolTemporal();

        try
        {
            _repoRol.AgregarRol(usuarioTemoral);
            var roles = _repoRol.ObtenerRolPorId(usuarioTemoral.IdRol);
            Assert.NotNull(roles);
            Assert.Equal("Usuario", roles.Nombre);
        }
        finally
        {
            BorrarRolTemporal(usuarioTemoral);
        }

        // Arrange
        
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

    [Fact]

    public void AcutualizarUsuario()
    {
        var usuarioTemoral = CrearRolTemporal();

        try
        {
            _repoRol.AgregarRol(usuarioTemoral);
            usuarioTemoral.Nombre = "Usuario General";
            _repoRol.ActualizarRol(usuarioTemoral);

            var roles = _repoRol.ObtenerRolPorId(usuarioTemoral.IdRol);
            

            Assert.NotNull(roles);
            Assert.Equal(usuarioTemoral.Nombre , roles.Nombre);
        }
        finally
        {
            BorrarRolTemporal(usuarioTemoral);
        }


    }

    private static Rol CrearRolTemporal()
    {
        return new Rol
        {
            IdRol=1,
            Nombre ="Usuario" 
        };
    }

    private void BorrarRolTemporal(Rol roles)
    {
        if (roles.IdRol > 0)
		{
			_repoRol.EliminarRol(roles.IdRol);
		}
    }

    

}