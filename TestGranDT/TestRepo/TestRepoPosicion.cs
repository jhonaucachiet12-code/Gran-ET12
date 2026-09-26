// test de repositorio de posicion
using System;
using System.Collections.Generic;
using GranDT.Core.Model;
using GranDT.Core.Model.IRepos;
using GranDT.Core.Gran_DT.ConDapper;
using Xunit;
using MySqlConnector;
namespace TestGranDT.TestRepo;

public class TestRepoPosicion
{
    private readonly IRepoPosicion _repoPosicion;

    public TestRepoPosicion()
    {
        var cadena = "Server=localhost;Database=bd_GranET;Uid=root;Pwd=1001;";
        var conexion = new MySqlConnection(cadena);
        _repoPosicion = new RepoPosicion(conexion);
    }

    [Fact]
    public void ObtenerPosiciones_DevuelveListaDePosiciones()
    {
        // Act
        var posiciones = _repoPosicion.ObtenerPosiciones();

        // Assert
        Assert.NotNull(posiciones);
        Assert.IsAssignableFrom<IEnumerable<Posicion>>(posiciones);
        Assert.NotEmpty(posiciones);
        Assert.Contains(posiciones, p => p.IdPosicion > 0 && !string.IsNullOrWhiteSpace(p.Nombre));
        Assert.Contains(posiciones, p => p.Nombre == "Delantero");
    }
    [Fact]
    public void ObtenerPosicionPorId_DevuelvePosicionExistente()
    {
        // Arrange
        byte idPosicionExistente = 1;

        // Act
        var posicion = _repoPosicion.ObtenerPosicionPorId(idPosicionExistente);

        // Assert
        Assert.NotNull(posicion);
        Assert.Equal(idPosicionExistente, posicion.IdPosicion);
        Assert.Equal("Delantero", posicion.Nombre);
    }

    [Fact]
    public void ObtenerPosicionPorId_DevuelveNullParaPosicionInexistente()
    {
        // Arrange
        byte idPosicionInexistente = 99;

        // Act
        var posicion = _repoPosicion.ObtenerPosicionPorId(idPosicionInexistente);

        // Assert
        Assert.Null(posicion);
    }

    [Fact]
    public void AgregarPosicion_AgregaNuevaPosicion()
    {
        // Arrange
        var nuevaPosicion = new Posicion { Nombre = "Arquero" };

        // Act
        _repoPosicion.AgregarPosicion(nuevaPosicion);

        // Assert
        Assert.True(nuevaPosicion.IdPosicion > 0);
        var posicionAgregada = _repoPosicion.ObtenerPosicionPorId(nuevaPosicion.IdPosicion);
        Assert.NotNull(posicionAgregada);
        Assert.Equal("Arquero", posicionAgregada.Nombre);
    }

    [Fact]
    public void ActualizarPosicion_ActualizaPosicionExistente()
    {
        // Arrange
        var posicionExistente = _repoPosicion.ObtenerPosicionPorId(1);
        Assert.NotNull(posicionExistente);
        posicionExistente.Nombre = "Delantero Actualizado";

        // Act
        _repoPosicion.ActualizarPosicion(posicionExistente);

        // Assert
        var posicionActualizada = _repoPosicion.ObtenerPosicionPorId(1);
        Assert.NotNull(posicionActualizada);
        Assert.Equal("Delantero Actualizado", posicionActualizada.Nombre);
    }

    [Fact]
    public void EliminarPosicion_EliminaPosicionExistente()
    {
        // Arrange
        var posicionExistente = _repoPosicion.ObtenerPosicionPorId(1);
        Assert.NotNull(posicionExistente);

        // Act
        _repoPosicion.EliminarPosicion(1);

        // Assert
        var posicionEliminada = _repoPosicion.ObtenerPosicionPorId(1);
        Assert.Null(posicionEliminada);
    }

}   
