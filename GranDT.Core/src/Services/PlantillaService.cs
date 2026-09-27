// service de plantilla
using GranDT.Core.Model;
using GranDT.Core.Model.IRepos;

namespace GranDT.Core.src.Services;

public class PlantillaService
    {
        private readonly IRepoPlantilla repoPlantilla;
    
        public PlantillaService(IRepoPlantilla repoPlantilla)
        {
            this.repoPlantilla = repoPlantilla;
        }
    
        public IEnumerable<Plantilla> ObtenerPlantillas()
        {
            return repoPlantilla.ObtenerPlantillas();
        }
    
        public Plantilla? ObtenerPlantillaPorId(int IdPlantilla)
        {
            ValidarId(IdPlantilla);
    
            return repoPlantilla.ObtenerPlantillaPorId(IdPlantilla);
        }
    
        public void AgregarPlantilla(Plantilla plantilla)
        {
            ValidarPlantilla(plantilla);
            repoPlantilla.AgregarPlantilla(plantilla);
        }
    
        public void ActualizarPlantilla(Plantilla plantilla)
        {
            ValidarId(plantilla.IdPlantilla);
            ValidarPlantilla(plantilla);
            repoPlantilla.ActualizarPlantilla(plantilla);
        }
    
        private static void ValidarId(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "El identificador debe ser mayor que cero.");
            }
        }
    
        private static void ValidarPlantilla(Plantilla plantilla)
        {
            ArgumentNullException.ThrowIfNull(plantilla);
    
            if (string.IsNullOrWhiteSpace(plantilla.Nombre))
            {
                throw new ArgumentException("El nombre de la plantilla es obligatorio.", nameof(plantilla));
            }
            
            if (plantilla.Presupuesto < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(plantilla.Presupuesto), "El presupuesto no puede ser negativo.");
            }
            
            if (plantilla.CantidadJugadores <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(plantilla.CantidadJugadores), "La cantidad de jugadores debe ser mayor que cero.");
            }
            
            if (plantilla.IdUsuario <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(plantilla.IdUsuario), "El identificador del usuario debe ser mayor que cero.");
            }
        }
    }