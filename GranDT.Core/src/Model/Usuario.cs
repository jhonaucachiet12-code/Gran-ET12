namespace GranDT.Core.Model;

public class Usuario
{
    public short IdUsuario {get;set;}
    
    public required string Nombre{get;set;}
    public required string Apellido{get;set;}
    public required string Email{get;set;}
    public DateTime FechaNacimiento {get;set;}
    public required string PasswordHash{get;set;}
    public byte IdRol{get;set;}
    public required Rol Roles{get;set;}

}
