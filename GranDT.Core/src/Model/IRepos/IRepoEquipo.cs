namespace GranDT.Core.Model.IRepos;

public interface IRepoEquipo
{
    IEnumerable<Equipo> ObtenerEquipos();
    Equipo? ObtenerEquipoPorId(short IdEquipo);
    void AgregarEquipo(Equipo equipo);
    void ActualizarEquipo(Equipo equipo);
    void EliminarEquipo(short IdEquipo);
}
