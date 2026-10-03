namespace GranDT.Core.Model;


public class Jugador
{
    public short IdJugador {get;set;}
    public byte IdPosicion {get;set;}
    public byte IdEquipo {get;set;}
    public required string Nombre{get;set;}
    public required string Apellido{get;set;}
    public required string Apodo{get;set;}
    public DateTime Nacimiento{get;set;}
    public decimal Cotización{get;set;}
    
    public Posicion? posicion {get;set;}
    public Equipo? equipo{get;set;}


}
