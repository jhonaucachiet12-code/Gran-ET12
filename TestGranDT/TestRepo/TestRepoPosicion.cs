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
        var posicionEsperada = CrearPosicionTemporal();

        try
        {
            _repoPosicion.AgregarPosicion(posicionEsperada);

            var posiciones = _repoPosicion.ObtenerPosiciones();

            Assert.NotNull(posiciones);
            Assert.IsAssignableFrom<IEnumerable<Posicion>>(posiciones);
            Assert.Contains(posiciones,r => r.Nombre == "Arquero");
            Assert.Contains(posiciones, posicion => posicion.IdPosicion == posicionEsperada.IdPosicion);

        }
        finally
        {
            EliminarSiFueRegistrada(posicionEsperada);
        }
    }

    [Fact]
    public void ObtenerPosicionPorId_DevuelvePosicionExistente()
    {
        var posicionEsperada = CrearPosicionTemporal();

        try
        {
            _repoPosicion.AgregarPosicion(posicionEsperada);

            var posicion = _repoPosicion.ObtenerPosicionPorId(posicionEsperada.IdPosicion);

            Assert.NotNull(posicion);
            Assert.Equal(posicionEsperada.IdPosicion, posicion.IdPosicion);
            Assert.Equal(posicionEsperada.Nombre, posicion.Nombre);
            
        }
        finally
        {
            EliminarSiFueRegistrada(posicionEsperada);
        }
    }

    [Fact]
    public void ObtenerPosicionPorId_DevuelveNullParaPosicionInexistente()
    {
        var posicion = _repoPosicion.ObtenerPosicionPorId(0);

        Assert.Null(posicion);
    }

    [Fact]
    public void AgregarPosicion_AgregaNuevaPosicion()
    {
        var nuevaPosicion = CrearPosicionTemporal();

        try
        {
            _repoPosicion.AgregarPosicion(nuevaPosicion);

            Assert.True(nuevaPosicion.IdPosicion > 0);
            var posicionAgregada = _repoPosicion.ObtenerPosicionPorId(nuevaPosicion.IdPosicion);
            Assert.NotNull(posicionAgregada);
            Assert.Equal(nuevaPosicion.Nombre, posicionAgregada.Nombre);
        }
        finally
        {
            EliminarSiFueRegistrada(nuevaPosicion);
        }
    }

    [Fact]
    public void ActualizarPosicion_ActualizaPosicionExistente()
    {
        var posicion = CrearPosicionTemporal();

        try
        {
            _repoPosicion.AgregarPosicion(posicion);
            posicion.Nombre = $"Actualizada-{Guid.NewGuid():N}";

            _repoPosicion.ActualizarPosicion(posicion);

            var posicionActualizada = _repoPosicion.ObtenerPosicionPorId(posicion.IdPosicion);
            Assert.NotNull(posicionActualizada);
            Assert.Equal(posicion.Nombre, posicionActualizada.Nombre);
        }
        finally
        {
            EliminarSiFueRegistrada(posicion);
        }
    }

    [Fact]
    public void EliminarPosicion_EliminaPosicionExistente()
    {
        var posicion = CrearPosicionTemporal();

        try
        {
            _repoPosicion.AgregarPosicion(posicion);

            _repoPosicion.EliminarPosicion(posicion.IdPosicion);

            Assert.Null(_repoPosicion.ObtenerPosicionPorId(posicion.IdPosicion));
        }
        finally
        {
            EliminarSiFueRegistrada(posicion);
        }
    }

    private static Posicion CrearPosicionTemporal()
    {
        return new Posicion
        {
            Nombre = $"Arquero"
        };
    }

    private void EliminarSiFueRegistrada(Posicion posicion)
    {
        if (posicion.IdPosicion > 0)
        {
            _repoPosicion.EliminarPosicion(posicion.IdPosicion);
        }
    }
}
