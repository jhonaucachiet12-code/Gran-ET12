// service de plantilla
using GranDT.Core.Model;
using GranDT.Core.Model.IRepos;

namespace GranDT.Core.src.Services;

public class PlantillaService
{
        private readonly IRepoPlantilla repoPlantilla;
    
        public PlantillaService(IRepoPlantilla repoPlantilla)
        {
            this.repoPlantilla = repoPlantilla ?? throw new ArgumentNullException(nameof(repoPlantilla));
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
            ValidarPlantilla(plantilla);
            ValidarId(plantilla.IdPlantilla);
            repoPlantilla.ActualizarPlantilla(plantilla);
        }

        public void EliminarPlantilla(int IdPlantilla)
        {
            ValidarId(IdPlantilla);
            repoPlantilla.EliminarPlantilla(IdPlantilla);
        
        }



        public void AgregarJugadorAPlantilla(Plantilla plantilla, Jugador jugador,int idPlantilla, int idJugador, bool esTitular)
        {
            ArgumentNullException.ThrowIfNull(plantilla);
            ArgumentNullException.ThrowIfNull(jugador);
            ValidarId(idPlantilla);
            ValidarId(idJugador);

            if (plantilla.IdPlantilla != idPlantilla)
            {
                throw new ArgumentException("El identificador no corresponde a la plantilla indicada.", nameof(idPlantilla));
            }

            if (jugador.IdJugador != idJugador)
            {
                throw new ArgumentException("El identificador no corresponde al jugador indicado.", nameof(idJugador));
            }

            if (jugador.Cotización < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(jugador.Cotización), "La cotización del jugador no puede ser negativa.");
            }

            if (plantilla.JugadoresTitulares.Any(j => j.IdJugador == idJugador) ||
                plantilla.JugadoresSuplentes.Any(j => j.IdJugador == idJugador))
            {
                throw new InvalidOperationException("El jugador ya pertenece a esta plantilla.");
            }

            if (plantilla.Presupuesto < jugador.Cotización)
            {
                throw new InvalidOperationException("El presupuesto disponible no alcanza para agregar al jugador.");
            }

            if (plantilla.CantidadJugadores == 20)
            {
                throw new InvalidOperationException("La plantilla alcanzó la cantidad máxima de jugadores.");
            }

            if (esTitular)
            {
                ValidarCupoTitular(plantilla, jugador);
            }

            repoPlantilla.AgregarJugadorAPlantilla(idPlantilla, idJugador, esTitular);
            plantilla.Presupuesto -= jugador.Cotización;
            

            if (esTitular)
            {
                plantilla.JugadoresTitulares.Add(jugador);
            }
            else
            {
                plantilla.JugadoresSuplentes.Add(jugador);
            }
        }

        public decimal ObtenersumaDeLasPuntuacionesDeLosTitularesDeLaPlantilla(int idPlantilla, byte fecha)
    {
        ValidarId(idPlantilla);
        if(fecha <= 0 || fecha > 50)
        {
            throw new ArgumentOutOfRangeException(nameof(idPlantilla), "El identificador debe ser mayor que cero.");
        }
        return repoPlantilla.ObtenersumaDeLasPuntuacionesDeLosTitularesDeLaPlantilla(idPlantilla,fecha);
    }
    public decimal ObtenerElValorTotalDeLaPlantilla(int idPlantilla)
    {
        ValidarId(idPlantilla);
        return repoPlantilla.ObtenerElValorTotalDeLaPlantilla(idPlantilla);
    }

        public void EliminarJugadorDePlantilla(short idJugador, int idPlantilla)
        {
            ValidarId(idJugador );
            ValidarId(idPlantilla);
            repoPlantilla.EliminarJugadorDePlantilla(idJugador,idPlantilla);
        }

        public void ActualizarJugadorEnPlantilla(Plantilla plantilla, Jugador jugador,int idPlantilla, int idJugador, bool esTitular)
        {

            if(esTitular)
            {
                ValidarCupoTitular(plantilla,jugador);

            }

            repoPlantilla.ActualizarJugadorEnPlantilla(idPlantilla, idJugador,  esTitular);
        }


        public Plantilla? ObtenerJugadoresDeLaPlantilla(int idPlantilla)
        {
            return repoPlantilla.ObtenerJugadoresDeLaPlantilla(idPlantilla);
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
            
            if (plantilla.IdUsuario <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(plantilla.IdUsuario), "El identificador del usuario debe ser mayor que cero.");
            }
        }

        private static void ValidarCupoTitular(Plantilla plantilla, Jugador jugador)
        {
            var (posicion, maximo) = jugador.IdPosicion switch
            {
                1 => ("arquero", 1),
                2 => ("defensor", 4),
                3 => ("mediocampista", 4),
                4 => ("delantero", 2),
                _ => throw new ArgumentOutOfRangeException(nameof(jugador.IdPosicion), "El jugador debe tener una posición válida.")
            };

            if (plantilla.JugadoresTitulares.Count(j => j.IdPosicion == jugador.IdPosicion) >= maximo)
            {
                throw new InvalidOperationException($"La plantilla ya tiene el máximo de titulares para la posición {posicion}.");
            }
        }

        
}