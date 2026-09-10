namespace GranDT.Core.Model;

public class Plantilla
{
    public int IdPlantilla {get; set;}
    public short IdUsuario{get; set;}
    public required string Nombre{get;set;}
    public decimal Presupuesto{get;set;}
    public byte CantidadJugadores{get;set;}
    
    public Plantilla(decimal Presupuesto )
    {
        Presupuesto = 100000000;
    }

}
