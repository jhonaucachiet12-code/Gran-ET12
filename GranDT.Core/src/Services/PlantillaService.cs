// service de plantilla
using GranDT.Core.Model;
using GranDT.Core.Model.IRepos;

namespace GranDT.Core.src.Services;

public class PlantillaService
{
        private readonly IRepoPlantilla repoPlantilla;
        private readonly IRepoJugador repoJugador;
    
        public PlantillaService(IRepoPlantilla repoPlantilla , IRepoJugador repoJugador)
        {
            this.repoPlantilla = repoPlantilla ?? throw new ArgumentNullException(nameof(repoPlantilla));
            this.repoJugador = repoJugador ?? throw new ArgumentNullException(nameof(repoJugador));
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



    public void AgregarJugadorAPlantilla(int idPlantilla, short idJugador, bool esTitular)
    {
    ValidarId(idPlantilla);
    ValidarId(idJugador);

    
    var plantilla = repoPlantilla.ObtenerJugadoresDeLaPlantilla(idPlantilla) 
                    ?? throw new ArgumentException("La plantilla no existe.");

    
    if (plantilla.JugadoresTitulares.Any(j => j.IdJugador == idJugador) ||
        plantilla.JugadoresSuplentes.Any(j => j.IdJugador == idJugador))
    {
        throw new InvalidOperationException("El jugador ya pertenece a esta plantilla.");
    }

    if (plantilla.CantidadJugadores == 20)
    {
        throw new InvalidOperationException("La plantilla alcanzó la cantidad máxima de jugadores.");
    }

    if(plantilla.JugadoresTitulares.Count >= 11 && esTitular == true)
    {
        throw new InvalidOperationException("La plantilla ya tiene el máximo de titulares.");
    }

    ValidarCupoTitular(idJugador, idPlantilla);

    
    repoPlantilla.AgregarJugadorAPlantilla(idPlantilla, idJugador, esTitular);
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



        public void ActualizarJugadorEnPlantilla(int idPlantilla, short idJugador, bool esTitular)
    {
        ValidarId(idPlantilla);
        ValidarId(idJugador);

        
        if (esTitular)
        {
            var plantilla = repoPlantilla.ObtenerJugadoresDeLaPlantilla(idPlantilla) 
                            ?? throw new ArgumentException("La plantilla no existe.");
            ValidarCupoTitular(idJugador, idPlantilla);
            
            // Si el jugador ya está en la plantilla pero quieres cambiarlo a titular, 
            // asegúrate de tener la lógica de validación de cupos aquí si la requieres.
        }

        

        repoPlantilla.ActualizarJugadorEnPlantilla(idPlantilla, idJugador, esTitular);
    }




        public Plantilla? ObtenerJugadoresDeLaPlantilla(int idPlantilla)
        {
            ValidarId(idPlantilla);
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

        private  void ValidarCupoTitular(short idJugador, int idPlantilla)
        {
            var jugador = repoJugador.ObtenerJugadorPorId(idJugador) 
                          ?? throw new ArgumentException("El jugador no existe.");
            var plantilla = repoPlantilla.ObtenerJugadoresDeLaPlantilla(idPlantilla) 
                          ?? throw new ArgumentException("La plantilla no existe.");
            

            if (jugador.IdPosicion <= 0)
            {
                throw new ArgumentException("El jugador debe tener una posición válida.");
            }

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