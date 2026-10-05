

using Org.BouncyCastle.Asn1.Esf;
using System.Collections.Generic;

namespace GranDT.Core.Model;

public class Plantilla
{
    public int IdPlantilla {get; set;}
    public short IdUsuario{get; set;}
    public required string Nombre{get;set;}
    public decimal Presupuesto  {get;set;} = 9000000;
    public byte CantidadJugadores => (byte)(JugadoresTitulares.Count + JugadoresSuplentes.Count);

    public List<Jugador> JugadoresTitulares {get;set;} = new List<Jugador>();
    public List<Jugador> JugadoresSuplentes {get;set;} = new List<Jugador>();
    public Usuario? usuario {get;set;}

    
}
