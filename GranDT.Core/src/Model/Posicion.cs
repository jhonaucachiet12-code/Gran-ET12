namespace GranDT.Core.Model;

public class Posicion
{
    public byte IdPosicion {get;set;}
    public required string Nombre {get;set;}
    
}


//DTO
public class RegistroPosicionRequest()
{
    public required string Nombre{get;set;}
}