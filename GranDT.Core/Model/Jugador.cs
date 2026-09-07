namespace GranDT.Core.Model;

public class Jugador
{
    public short idJugador {get;set;}
    public byte idTipo{get;set;}
    public byte idEquipo{get;set;}
    public required string nombre{get;set;}
    public required string apellido{get;set;}
    public required string apodo{get;set;}
    public DateTime nacimiento{get;set;}
    public decimal cotización{get;set;}
    public Tipo Tipo {get;set}
    public Equipo Equipo{get;set;}


}
