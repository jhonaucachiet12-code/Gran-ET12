namespace GranDT.Core.Model;

public class Jugador
{
    public short idJugador {get;set;}
    public required string nombre{get;set;}
    public required string apellido{get;set;}
    public required string apodo{get;set;}
    public DateTime nacimiento{get;set;}
    public decimal cotización{get;set;}
    public required  Tipo tipo {get;set;}
    public required Equipo equipo{get;set;}


}
