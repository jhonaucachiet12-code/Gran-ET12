namespace GranDT.Core.Model.IRepos;

public interface IRepoEquipo
{
    IEnumerable<Equipo> ObtenerEquipos();
    Equipo? ObtenerEquipoPorId(byte IdEquipo);
    void AgregarEquipo(Equipo equipo);
    void ActualizarEquipo(Equipo equipo);
    void EliminarEquipo(byte IdEquipo);
}
