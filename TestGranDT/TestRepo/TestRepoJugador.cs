using System.Collections.Generic;
using GranDT.Core.Gran_DT.ConDapper;
using GranDT.Core.Model;
using GranDT.Core.Model.IRepos;
using MySqlConnector;
using Xunit;

namespace TestGranDT.TestRepo;

public class TestRepoJugador
{
	private readonly IRepoJugador _repoJugador;
	private readonly IRepoEquipo _repoEquipo;
	private readonly IRepoPosicion _repoPosicion;

	public TestRepoJugador()
	{
		var cadena = "Server=localhost;Database=bd_GranET;Uid=root;Pwd=1001;";
		var conexion = new MySqlConnection(cadena);
		_repoJugador = new RepoJugador(conexion);
		_repoEquipo = new RepoEquipo(conexion);
		_repoPosicion = new RepoPosicion(conexion);
	}

	[Fact]
	public void ObtenerJugadores_DevuelveListaDeJugadores()
	{
		var jugadorEsperado = CrearJugadorTemporal();

		try
		{
			_repoJugador.AgregarJugador(jugadorEsperado);

			var jugadores = _repoJugador.ObtenerJugadores();

			Assert.NotNull(jugadores);
			Assert.IsAssignableFrom<IEnumerable<Jugador>>(jugadores);
			Assert.Contains(jugadores, jugador => jugador.IdJugador == jugadorEsperado.IdJugador);
		}
		finally
		{
			EliminarSiFueRegistrado(jugadorEsperado);
		}
	}

	[Fact]
	public void ObtenerJugadorPorId_DevuelveJugadorExistente()
	{
		var jugadorEsperado = CrearJugadorTemporal();

		try
		{
			_repoJugador.AgregarJugador(jugadorEsperado);

			var jugador = _repoJugador.ObtenerJugadorPorId(jugadorEsperado.IdJugador);

			Assert.NotNull(jugador);
			Assert.Equal(jugadorEsperado.IdJugador, jugador.IdJugador);
			Assert.Equal(jugadorEsperado.Nombre, jugador.Nombre);
			Assert.Equal(jugadorEsperado.posicion.IdPosicion, jugador.posicion.IdPosicion);
			Assert.Equal(jugadorEsperado.equipo.IdEquipo, jugador.equipo.IdEquipo);
		}
		finally
		{
			EliminarSiFueRegistrado(jugadorEsperado);
		}
	}

	[Fact]
	public void ObtenerJugadorPorId_DevuelveNullParaJugadorInexistente()
	{
		var jugador = _repoJugador.ObtenerJugadorPorId(0);

		Assert.Null(jugador);
	}

	[Fact]
	public void AgregarJugador_AgregaNuevoJugador()
	{
		var nuevoJugador = CrearJugadorTemporal();

		try
		{
			_repoJugador.AgregarJugador(nuevoJugador);

			Assert.True(nuevoJugador.IdJugador > 0);
			var jugadorAgregado = _repoJugador.ObtenerJugadorPorId(nuevoJugador.IdJugador);
			Assert.NotNull(jugadorAgregado);
			Assert.Equal(nuevoJugador.Nombre, jugadorAgregado.Nombre);
			Assert.Equal(nuevoJugador.Apellido, jugadorAgregado.Apellido);
			Assert.Equal(nuevoJugador.Apodo, jugadorAgregado.Apodo);
		}
		finally
		{
			EliminarSiFueRegistrado(nuevoJugador);
		}
	}

	[Fact]
	public void ActualizarJugador_ActualizaJugadorExistente()
	{
		var jugador = CrearJugadorTemporal();

		try
		{
			_repoJugador.AgregarJugador(jugador);
			jugador.Nombre = $"Actualizado-{Guid.NewGuid():N}";
			jugador.Cotización = 125.50m;

			_repoJugador.ActualizarJugador(jugador);

			var jugadorActualizado = _repoJugador.ObtenerJugadorPorId(jugador.IdJugador);
			Assert.NotNull(jugadorActualizado);
			Assert.Equal(jugador.Nombre, jugadorActualizado.Nombre);
			Assert.Equal(jugador.Cotización, jugadorActualizado.Cotización);
		}
		finally
		{
			EliminarSiFueRegistrado(jugador);
		}
	}

	[Fact]
	public void EliminarJugador_EliminaJugadorExistente()
	{
		var jugador = CrearJugadorTemporal();

		try
		{
			_repoJugador.AgregarJugador(jugador);

			_repoJugador.EliminarJugador(jugador.IdJugador);

			Assert.Null(_repoJugador.ObtenerJugadorPorId(jugador.IdJugador));
		}
		finally
		{
			EliminarSiFueRegistrado(jugador);
		}
	}

	private Jugador CrearJugadorTemporal()
	{
		/*var identificador = Guid.NewGuid().ToString("N");
		var equipo = new Equipo { Nombre = $"equipo de MRD" };
		var posicion = new Posicion { Nombre = $"Esquina" };*/

		

		return new Jugador
		{
			IdEquipo = 4,
			IdPosicion = 5,
			Nombre = $"gruu",
			Apellido = $"smith",
			Apodo = $"gaga",
			Nacimiento = new DateTime(2000, 1, 1),
			Cotización = 100.00m,
			equipo = new Equipo { IdEquipo = 4 ,Nombre = $"equipo de MRD" },
			posicion = new Posicion { IdPosicion = 5,Nombre = $"Esquina" },
		};
	}

	private void EliminarSiFueRegistrado(Jugador jugador)
	{
		if (jugador.IdJugador > 0)
		{
			_repoJugador.EliminarJugador(jugador.IdJugador);
		}

		if (jugador.IdEquipo > 0)
		{
			_repoEquipo.EliminarEquipo((byte)jugador.IdEquipo);
		}

		if (jugador.IdPosicion > 0)
		{
			_repoPosicion.EliminarPosicion(jugador.IdPosicion);
		}
	}
}

