namespace MinimalAPI.Services;

using GranDT.Core.Model;
using GranDT.Core.Model.IRepos;

public sealed class EquipoService
{
	private readonly IRepoEquipo _repoEquipo;

	public EquipoService(IRepoEquipo repoEquipo)
	{
		_repoEquipo = repoEquipo ?? throw new ArgumentNullException(nameof(repoEquipo));
	}

	public IEnumerable<Equipo> ObtenerEquipos()
	{
		return _repoEquipo.ObtenerEquipos();
	}

	public Equipo? ObtenerEquipoPorId(byte idEquipo)
	{
		ValidarId(idEquipo);
		return _repoEquipo.ObtenerEquipoPorId(idEquipo);
	}

	public void AgregarEquipo(Equipo equipo)
	{
		ValidarEquipo(equipo);
		_repoEquipo.AgregarEquipo(equipo);
	}

	public void ActualizarEquipo(Equipo equipo)
	{
		ValidarEquipo(equipo);
		ValidarId(equipo.IdEquipo);
		_repoEquipo.ActualizarEquipo(equipo);
	}

	public void EliminarEquipo(byte idEquipo)
	{
		ValidarId(idEquipo);
		_repoEquipo.EliminarEquipo(idEquipo);
	}

	private static void ValidarEquipo(Equipo equipo)
	{
		ArgumentNullException.ThrowIfNull(equipo);

		if (string.IsNullOrWhiteSpace(equipo.Nombre))
		{
			throw new ArgumentException("El nombre del equipo es obligatorio.", nameof(equipo));
		}
	}

	private static void ValidarId(short idEquipo)
	{
		if (idEquipo <= 0)
		{
			throw new ArgumentOutOfRangeException(nameof(idEquipo), "El identificador debe ser mayor que cero.");
		}
	}
}
