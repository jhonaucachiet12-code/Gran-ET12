namespace GranDT.Core.Model;

public class Plantilla
{
    public int IdPlantilla {get; set;}
    public short IdUsuario{get; set;}
    public required string Nombre{get;set;}
    public decimal Presupuesto{get;set;}
    public byte CantidadJugadores{get;set;}
    public IDictionary<Jugador,short> PlantillaJugadores{get;set;} = new Dictionary<Jugador,short>();

    public Plantilla()
    {
        Presupuesto = 100000000;
    }

    public void AgregarJugador(Jugador jugador, short cantidad)
    {
        if (cantidad <= 0)
        {
            throw new ArgumentOutOfRangeException("Cantidad no puede ser cero");
        }

        if(Presupuesto < jugador.Cotización)
        {
            throw new ArgumentOutOfRangeException("No tines el dinero sufisiente");
        }


        if (PlantillaJugadores.ContainsKey(jugador))
        {
            PlantillaJugadores[jugador] += cantidad;
        }
        else
        {
            PlantillaJugadores.Add(jugador, cantidad);
        }
    }

    

}
