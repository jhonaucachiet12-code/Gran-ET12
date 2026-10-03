using GranDT.Core.Model;
using GranDT.Core.Model.IRepos;
using GranDT.Core.Gran_DT.ConDapper;
using MySqlConnector;

namespace TestGranDT.TestRepo;

public class TestRepoPlantilla
{
	private readonly IRepoPlantilla _repoPlantilla;

	public TestRepoPlantilla()
	{

		var cadena = "Server=localhost;Database=bd_GranET;Uid=root;Pwd=1001;";
		//var cadena = "Server=localhost;Database=bd_GranET;Uid=5to_agbd;Pwd=Trigg3rs!;";
		var conexion = new MySqlConnection(cadena);
		_repoPlantilla = new RepoPlantilla(conexion);
	}

	[Fact]
	public void ObtenerPlantillas_DevuelveListaDePlantillas()
	{
		var plantillaEsperada = CrearPlantillaTemporal();

		try
		{
			_repoPlantilla.AgregarPlantilla(plantillaEsperada);

			var plantillas = _repoPlantilla.ObtenerPlantillas();

			Assert.NotNull(plantillas);
			Assert.IsAssignableFrom<IEnumerable<Plantilla>>(plantillas);
			Assert.Contains(plantillas, plantilla => plantilla.IdPlantilla == plantillaEsperada.IdPlantilla);
		}
		finally
		{
			EliminarSiFueRegistrada(plantillaEsperada);
		}
	}

	[Fact]
	public void ObtenerPlantillaPorId_DevuelvePlantillaExistente()
	{
		var plantillaEsperada = CrearPlantillaTemporal();

		try
		{
			_repoPlantilla.AgregarPlantilla(plantillaEsperada);

			var plantilla = _repoPlantilla.ObtenerPlantillaPorId(plantillaEsperada.IdPlantilla);

			Assert.NotNull(plantilla);
			Assert.Equal(plantillaEsperada.IdPlantilla, plantilla.IdPlantilla);
			Assert.Equal(plantillaEsperada.Nombre, plantilla.Nombre);
			Assert.Equal(plantillaEsperada.IdUsuario, plantilla.IdUsuario);
		}
		finally
		{
			EliminarSiFueRegistrada(plantillaEsperada);
		}
	}

	[Fact]
	public void ObtenerPlantillaPorId_DevuelveNullParaPlantillaInexistente()
	{
		var plantilla = _repoPlantilla.ObtenerPlantillaPorId(0);

		Assert.Null(plantilla);
	}

	[Fact]
	public void AgregarPlantilla_AgregaNuevaPlantilla()
	{
		var plantillaNueva = CrearPlantillaTemporal();

		try
		{
			_repoPlantilla.AgregarPlantilla(plantillaNueva);

			Assert.True(plantillaNueva.IdPlantilla > 0);
		}
		finally
		{
			EliminarSiFueRegistrada(plantillaNueva);
		}
	}

	[Fact]
	public void ActualizarPlantilla_ActualizaPlantillaExistente()
	{
		var plantilla = CrearPlantillaTemporal();

		try
		{
			_repoPlantilla.AgregarPlantilla(plantilla);
			plantilla.Nombre = $"Actualizada";
			plantilla.Presupuesto = 7500000;
			

			_repoPlantilla.ActualizarPlantilla(plantilla);

			var plantillaActualizada = _repoPlantilla.ObtenerPlantillaPorId(plantilla.IdPlantilla);

			Assert.NotNull(plantillaActualizada);
			Assert.Equal(plantilla.Nombre, plantillaActualizada.Nombre);
			Assert.Equal(plantilla.Presupuesto, plantillaActualizada.Presupuesto);
			Assert.Equal(plantilla.CantidadJugadores, plantillaActualizada.CantidadJugadores);
		}
		finally
		{
			EliminarSiFueRegistrada(plantilla);
		}
	}

	[Fact]
	public void EliminarPlantilla_EliminaPlantillaExistente()
	{
		var plantilla = CrearPlantillaTemporal();

		try
		{
			_repoPlantilla.AgregarPlantilla(plantilla);

			_repoPlantilla.EliminarPlantilla(plantilla.IdPlantilla);

			Assert.Null(_repoPlantilla.ObtenerPlantillaPorId(plantilla.IdPlantilla));
		}
		finally
		{
			EliminarSiFueRegistrada(plantilla);
		}
	}

	private static Plantilla CrearPlantillaTemporal()
	{
		return new Plantilla
		{
			IdUsuario = 1,
			Nombre = $"Plantilla de prueba",
			Presupuesto = 9000000,
			
		};
	}

	private void EliminarSiFueRegistrada(Plantilla plantilla)
	{
		if (plantilla.IdPlantilla > 5)
		{
			_repoPlantilla.EliminarPlantilla(plantilla.IdPlantilla);
		}
	}
}

