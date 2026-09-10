namespace GranDT.Core.Model;

public class Usuario
{
    public short IdUidUsuariosuario {get;set;}
    
    public required string Nombre{get;set;}
    public required string Apellido{get;set;}
    public required string Email{get;set;}
    public DateTime FechaNacimiento {get;set;}
    public required string Contraseña{get;set;}
    public byte IdRoles{get;set;}
    
    public required Roles roles{get;set;}

}
