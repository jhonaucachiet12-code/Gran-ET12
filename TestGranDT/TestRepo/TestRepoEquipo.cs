// Test del repositorio de la entidad Equipo con dapper con xunit, permite obtener todos los equipos de la base de datos.
using System;
using System.Collections.Generic;
using GranDT.Core.Model;
using GranDT.Core.Model.IRepos;
using GranDT.Core.Gran_DT.ConDapper;
using Xunit;
using MySqlConnector;


namespace TestGranDT.TestModel;

public class TestRepoEquipo
{
    private readonly IRepoEquipo _repoEquipo;


    public TestRepoEquipo()
    {
        //var cadena = "Server=localhost;Database=bd_Mundial26;Uid=root;Pwd=1001;";

        var cadena = "Server=localhost;Database=bd_GranET;Uid=5to_agbd;Pwd=Trigg3rs!;";
        var conexion = new MySqlConnection(cadena);
        _repoEquipo = new RepoEquipo(conexion);
    }

    [Fact]
    public void ObtenerEquipos_DevuelveListaDeEquipos()
    {
        // Act
        var equipos = _repoEquipo.ObtenerEquipos();

        // Assert
        Assert.NotNull(equipos);
        Assert.IsAssignableFrom<IEnumerable<Equipo>>(equipos);
        Assert.NotEmpty(equipos);
        Assert.Contains(equipos, e => e.IdEquipo > 0 && !string.IsNullOrWhiteSpace(e.Nombre));
        Assert.Contains(equipos, e => e.IdEquipo == 1 && e.Nombre == "Boca Juniors");
    }
    

    [Fact]
    public void ObtenerEquipoPorId_DevuelveEquipoExistente()
    {
        // Arrange
        byte idEquipoExistente = 1;

        // Act
        var equipo = _repoEquipo.ObtenerEquipoPorId(idEquipoExistente);

        // Assert
        Assert.NotNull(equipo);
        Assert.Equal(idEquipoExistente, equipo.IdEquipo);
        Assert.Equal("Boca Juniors", equipo.Nombre);
    }

    [Fact]

    public void ObtenerEquipoPorId_DevuelveNullParaEquipoInexistente()
    {
        // Arrange
        byte idEquipoInexistente = 90;

        // Act
        var equipo = _repoEquipo.ObtenerEquipoPorId(idEquipoInexistente);

        // Assert
        
        Assert.Null(equipo);
    }

    [Fact]
    public void AgregarEquipo_AgregaNuevoEquipo()
    {
        // Arrange
        var nuevoEquipo = new Equipo { Nombre = "Nuevo Equipo" };

        // Act
        _repoEquipo.AgregarEquipo(nuevoEquipo);

        // Assert
        Assert.True(nuevoEquipo.IdEquipo > 0);
        var equipoAgregado = _repoEquipo.ObtenerEquipoPorId(nuevoEquipo.IdEquipo);
        Assert.NotNull(equipoAgregado);
        Assert.Equal("Nuevo Equipo", equipoAgregado.Nombre);
    }

    [Fact]
    public void ActualizarEquipo_ActualizaEquipoExistente()
    {
        // Arrange
        var equipoExistente = _repoEquipo.ObtenerEquipoPorId(1);
        Assert.NotNull(equipoExistente);
        equipoExistente.Nombre = "Equipo Actualizado";

        // Act
        _repoEquipo.ActualizarEquipo(equipoExistente);

        // Assert
        var equipoActualizado = _repoEquipo.ObtenerEquipoPorId(1);
        Assert.NotNull(equipoActualizado);
        Assert.Equal("Equipo Actualizado", equipoActualizado.Nombre);
    }

    [Fact]
    public void EliminarEquipo_EliminaEquipoExistente()
    {
        // Arrange
        var equipoExistente = _repoEquipo.ObtenerEquipoPorId(1);
        Assert.NotNull(equipoExistente);

        // Act
        _repoEquipo.EliminarEquipo(1);

        // Assert
        var equipoEliminado = _repoEquipo.ObtenerEquipoPorId(1);
        Assert.Null(equipoEliminado);
    }



    
}
