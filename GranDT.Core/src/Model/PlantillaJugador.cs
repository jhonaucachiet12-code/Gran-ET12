namespace GranDT.Core.Model;

public class PlantillaJugador
{
    public int IdPlantilla {get;set;}
    public short IdJugador{get;set;}
    public bool Titulares{get;set;}
    public ICollection<Jugador>Jugadores{get;set;} = new List<Jugador>();
    public required Jugador jugador{get;set;}
    public required Plantilla plantilla {get;set;}

}
