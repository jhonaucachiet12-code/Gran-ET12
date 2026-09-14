namespace GranDT.Core.Model.IRepos;

public interface IRepoEquipo
{
    IEnumerable<Equipo> ObtenerQuipos();
    Equipo? ObtenerEquipoPorId(int id);
    void AgregarEquipo(Equipo equipo);
    void ActualizarEquipo(Equipo equipo);
    void EliminarEquipo(int id);
}
