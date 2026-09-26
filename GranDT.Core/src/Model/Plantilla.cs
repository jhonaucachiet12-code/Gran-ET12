

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

    public List<Jugador> JugadoresTitulares {get;set;} = new List<Jugador>();
    public List<Jugador> JugadoresSuplentes {get;set;} = new List<Jugador>();

    public decimal costoTotalDeLaPlantilla()
    {
        decimal costoTotal = 0;

        foreach (var jugador in JugadoresTitulares)
        {
            costoTotal += jugador.Cotización;
        }

        foreach (var jugador in JugadoresSuplentes)
        {
            costoTotal += jugador.Cotización;
        }

        return costoTotal;
    }

    public bool validarposiciones()
    {
        int cantidadArquero = 0;
        int cantidadDefensor = 0;
        int cantidadMediocampista = 0;
        int cantidadDelantero = 0;

        foreach (var jugador in JugadoresTitulares)
        {
            switch (jugador.IdPosicion)
            {
                case 1:
                    cantidadArquero++;
                    break;
                case 2:
                    cantidadDefensor++;
                    break;
                case 3:
                    cantidadMediocampista++;
                    break;
                case 4:
                    cantidadDelantero++;
                    break;
            }
        }

        return cantidadArquero == 1 && cantidadDefensor == 4 && cantidadMediocampista == 4 && cantidadDelantero == 2;
    }
    //public Plantilla()
    /*{
        Presupuesto = 100000000;
    }

    public void AgregarJugador(Jugador jugador)
    {
        

        if(Presupuesto < jugador.Cotización)
        {
            throw new ArgumentOutOfRangeException("No tines el dinero sufisiente");
        }


        if (PlantillaJugadores.Contains(jugador))
        {
            throw new ArgumentOutOfRangeException("se peude agregar mas de un mismo jugador");
        }
        else
        {
            
            PlantillaJugadores.Add(jugador);
            Presupuesto -= jugador.Cotización;
            CantidadJugadores++;
        }
    }*/

    

}
