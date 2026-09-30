using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using GranDT.Core.Gran_DT.ConDapper;
using GranDT.Core.Model;
using GranDT.Core.Model.IRepos;
using MySqlConnector;
using Xunit;

namespace TestGranDT.TestRepo;

public class TestRepoPuntuacion
{
    private readonly MySqlConnection _conexion;
    private readonly IRepoPuntuacion _repoPuntuacion;

    public TestRepoPuntuacion()
    {
        var cadena = "Server=localhost;Database=bd_GranET;Uid=root;Pwd=1001;";
        _conexion = new MySqlConnection(cadena);
        _repoPuntuacion = new RepoPuntuacion(_conexion);
    }

    [Fact]
    public void ObtenerPuntuasiones_DevuelveListaDePuntuaciones()
    {
        var puntuacionEsperada = CrearPuntuacionTemporal();

        try
        {
            _repoPuntuacion.AgregarPuntuacion(puntuacionEsperada);

            var puntuaciones = _repoPuntuacion.ObtenerPuntuasiones();

            Assert.NotNull(puntuaciones);
            Assert.IsAssignableFrom<IEnumerable<Puntuacion>>(puntuaciones);
            Assert.Contains(puntuaciones, puntuacion =>
                puntuacion.IdJugador == puntuacionEsperada.IdJugador &&
                puntuacion.Fecha == puntuacionEsperada.Fecha);
        }
        finally
        {
            EliminarPuntuacionTemporal(puntuacionEsperada.IdJugador, puntuacionEsperada.Fecha);
        }
    }

    [Fact]
    public void ObtenerLaPuntucionDelJugador_DevuelvePuntuacionExistente()
    {
        var puntuacionEsperada = CrearPuntuacionTemporal();

        try
        {
            InsertarPuntuacionTemporal(puntuacionEsperada);

            var puntuacion = _repoPuntuacion.ObtenerLaPuntucionDelJugador(
                puntuacionEsperada.IdJugador,
                puntuacionEsperada.Fecha);

            Assert.NotNull(puntuacion);
            Assert.Equal(puntuacionEsperada.IdJugador, puntuacion.IdJugador);
            Assert.Equal(puntuacionEsperada.Fecha, puntuacion.Fecha);
            Assert.Equal(puntuacionEsperada.Puntuaciones, puntuacion.Puntuaciones);
        }
        finally
        {
            EliminarPuntuacionTemporal(puntuacionEsperada.IdJugador, puntuacionEsperada.Fecha);
        }
    }

    [Fact]
    public void ObtenerLaPuntucionDelJugador_DevuelveNullParaPuntuacionInexistente()
    {
        var puntuacion = CrearPuntuacionTemporal();

        Assert.Null(_repoPuntuacion.ObtenerLaPuntucionDelJugador(
            puntuacion.IdJugador,
            puntuacion.Fecha));
    }

    [Fact]
    public void ObtenerTodasLasPuntuasionesDelJugador_DevuelveSusPuntuaciones()
    {
        var puntuacionEsperada = CrearPuntuacionTemporal();

        try
        {
            InsertarPuntuacionTemporal(puntuacionEsperada);

            var puntuaciones = _repoPuntuacion.ObtenerTodasLasPuntuasionesDelJugador(
                puntuacionEsperada.IdJugador);

            Assert.NotNull(puntuaciones);
            Assert.Contains(puntuaciones, puntuacion =>
                puntuacion.Fecha == puntuacionEsperada.Fecha &&
                puntuacion.Puntuaciones == puntuacionEsperada.Puntuaciones);
        }
        finally
        {
            EliminarPuntuacionTemporal(puntuacionEsperada.IdJugador, puntuacionEsperada.Fecha);
        }
    }

    [Fact]
    public void AgregarPuntuacion_AgregaNuevaPuntuacion()
    {
        var puntuacionEsperada = CrearPuntuacionTemporal();
        var idJugador = puntuacionEsperada.IdJugador;

        try
        {
            _repoPuntuacion.AgregarPuntuacion(puntuacionEsperada);

            var puntuacionAgregada = _repoPuntuacion.ObtenerLaPuntucionDelJugador(
                idJugador,
                puntuacionEsperada.Fecha);

            Assert.NotNull(puntuacionAgregada);
            Assert.Equal(puntuacionEsperada.Puntuaciones, puntuacionAgregada.Puntuaciones);
        }
        finally
        {
            EliminarPuntuacionTemporal(idJugador, puntuacionEsperada.Fecha);
        }
    }

    [Fact]
    public void ActualizarPuntuacion_ActualizaPuntuacionExistente()
    {
        var puntuacion = CrearPuntuacionTemporal();

        try
        {
            InsertarPuntuacionTemporal(puntuacion);
            puntuacion.Puntuaciones = 98.76m;

            _repoPuntuacion.ActualizarPuntuacion(puntuacion);

            var puntuacionActualizada = _repoPuntuacion.ObtenerLaPuntucionDelJugador(
                puntuacion.IdJugador,
                puntuacion.Fecha);

            Assert.NotNull(puntuacionActualizada);
            Assert.Equal(puntuacion.Puntuaciones, puntuacionActualizada.Puntuaciones);
        }
        finally
        {
            EliminarPuntuacionTemporal(puntuacion.IdJugador, puntuacion.Fecha);
        }
    }

    [Fact]
    public void EliminarPuntuacion_EliminaPuntuacionExistente()
    {
        var puntuacion = CrearPuntuacionTemporal();

        try
        {
            _repoPuntuacion.AgregarPuntuacion(puntuacion);

            _repoPuntuacion.EliminarPuntuacion(puntuacion.IdJugador);

            Assert.Null(_repoPuntuacion.ObtenerLaPuntucionDelJugador(
                puntuacion.IdJugador,
                puntuacion.Fecha));
        }
        finally
        {
            EliminarPuntuacionTemporal(puntuacion.IdJugador, puntuacion.Fecha);
        }
    }

    private Puntuacion CrearPuntuacionTemporal()
    {
        var idJugador = _conexion.QueryFirstOrDefault<short?>(
            "SELECT idJugador FROM Jugador LIMIT 1");
        Assert.True(idJugador.HasValue, "Se necesita al menos un jugador para probar puntuaciones.");

        var fechasUsadas = _conexion.Query<byte>(
            "SELECT Fecha FROM Puntuacion WHERE idJugador = @IdJugador",
            new { IdJugador = idJugador.Value }).ToHashSet();
        var fechaDisponible = Enumerable.Range(1, 127)
            .Select(fecha => (byte)fecha)
            .FirstOrDefault(fecha => !fechasUsadas.Contains(fecha));

        Assert.NotEqual((byte)0, fechaDisponible);

        return new Puntuacion
        {
            IdJugador = idJugador.Value,
            Fecha = fechaDisponible,
            Puntuaciones = 12.34m,
            jugador = new Jugador
            {
                IdJugador = idJugador.Value,
                IdPosicion = 0,
                IdEquipo = 0,
                Nombre = "Jugador temporal",
                Apellido = "Prueba",
                Apodo = "Temporal",
                Nacimiento = new DateTime(1990, 1, 1),
                Cotización = 0,
                posicion = new Posicion { Nombre = "Posición temporal" },
                equipo = new Equipo { Nombre = "Equipo temporal" }
            }
        };
    }

    private void InsertarPuntuacionTemporal(Puntuacion puntuacion)
    {
        _conexion.Execute(
            "INSERT INTO Puntuacion (idJugador, Fecha, puntuacion) VALUES (@IdJugador, @Fecha, @Puntuaciones)",
            puntuacion);
    }

    private void EliminarPuntuacionTemporal(short idJugador, byte fecha)
    {
        _conexion.Execute(
            "DELETE FROM Puntuacion WHERE idJugador = @IdJugador AND Fecha = @Fecha",
            new { IdJugador = idJugador, Fecha = fecha });
    }
}