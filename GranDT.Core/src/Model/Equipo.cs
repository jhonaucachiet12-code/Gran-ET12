namespace GranDT.Core.Model;

public class Equipo
{
    public byte IdEquipo {get;set;}
    public required string Nombre {get;set;}
     
}


// DTO
public class RegistroEquipoRequest()
{
    public required string Nombre {get;set;}
}