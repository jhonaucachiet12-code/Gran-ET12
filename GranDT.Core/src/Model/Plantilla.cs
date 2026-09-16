using Org.BouncyCastle.Asn1.Esf;
using System.Collections.Generic;

namespace GranDT.Core.Model;

public class Plantilla
{
    public int IdPlantilla {get; set;}
    public short IdUsuario{get; set;}
    public required string Nombre{get;set;}
    public decimal Presupuesto{get;set;}
    public byte CantidadJugadores{get;set;}

    HashSet<Jugador> PlantillaJugadoresDeSuplentes {get;set;} = new HashSet<Jugador>();
    HashSet<Jugador> PlantillaJugadoresDeTitulares{get;set;}= new HashSet<Jugador>();

    public Plantilla()
    {
        Presupuesto = 100000000;
    }

    public void AgregarJugador(Jugador jugador)
    {
        

        if(Presupuesto < jugador.Cotización)
        {
            throw new ArgumentOutOfRangeException("No tines el dinero sufisiente");
        }


        if (PlantillaJugadoresDeSuplentes.Contains(jugador) || PlantillaJugadoresDeTitulares.Contains(jugador))
        {
            throw new ArgumentOutOfRangeException("se peude agregar mas de un mismo jugador");
        }
        else
        {
            if(jugador.Titulares)
            {
                PlantillaJugadoresDeTitulares.Add(jugador);
            }
            else
            {
                PlantillaJugadoresDeSuplentes.Add(jugador);
            }
            
        }
    }

    

}
