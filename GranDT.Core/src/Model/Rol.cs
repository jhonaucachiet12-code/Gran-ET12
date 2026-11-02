namespace GranDT.Core.Model;

public class Rol
{
    public byte IdRol {get;set;}
    public required string Nombre{get;set;}
}


public class RegistroRolRequest()
{
    public required string Nombre {get;set;}
}