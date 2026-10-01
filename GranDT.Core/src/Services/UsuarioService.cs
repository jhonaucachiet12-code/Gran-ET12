
using GranDT.Core.Model;
using GranDT.Core.Model.IRepos;
using BCrypt.Net;
using GranDT.Core.Gran_DT.ConDapper;
namespace GranDT.Core.src.Services;

// la capa de servicio se encarga de los que esta en medio. es la que se encargaqeu las condiciones se cumplan antes de hacer algo
// la logica de negocio
public class UsuarioService
{
    private readonly IRepoUsuario repoUsuario;

    public UsuarioService(IRepoUsuario repoUsuario)
    {
        this.repoUsuario = repoUsuario;
    }

    public IEnumerable<Usuario> ObtenerUsuarios()
    {
        return repoUsuario.ObtenerUsuarios();
    }

    public Usuario? ObtenerPorEmail(short IdUsuario)
    {
        ValidarId(IdUsuario);

        return repoUsuario.ObtenerPorEmail( IdUsuario);
    }

    public void RegistrarUsario(Usuario usuario , string PasswordHash)
    {
        if(string.IsNullOrWhiteSpace(PasswordHash))
        {
            throw new ArgumentException("La contraseña es obligatoria.", nameof(PasswordHash));
        }

        if(PasswordHash.Length < 6)
        {
            throw new ArgumentException("La contraseña debe tener al menos 6 caracteres.", nameof(PasswordHash));
        }

        ValidarUsuario(usuario);

        var usuarios = repoUsuario.ObtenerUsuarios();
        if (usuarios.Any(u => u.Email == usuario.Email))
        {
            throw new ArgumentException("El email ya está en uso.", nameof(usuario));
        }
        

        usuario.PasswordHash = BCrypt.Net.BCrypt.HashPassword(PasswordHash);

        repoUsuario.RegistrarUsario(usuario,PasswordHash);
        
    }

    public void EliminarUsuario(short IdUsuario)
    {
        if(IdUsuario <0)
        {
            throw new Exception("No puedes eliminar un jugador que no existe");
        }
        repoUsuario.EliminarUsuario(IdUsuario);
    }

    public void ActualizarUsuario(Usuario nuevoUsuario)
    {
        ValidarUsuario(nuevoUsuario);

        var viejoUsuario = repoUsuario.ObtenerPorEmail(nuevoUsuario.IdUsuario);
        
        if (viejoUsuario == null)
        {
            //por que?
            throw new Exception("El usuario no exite.");
        }

        // nuevoUsuario.PasswordHash llega en texto plano desde el cliente.
    // Verify(textoPlano, hashGuardado) chequea si es la misma contraseña de antes.
        bool esLaMismaPassword = BCrypt.Net.BCrypt.Verify(nuevoUsuario.PasswordHash, viejoUsuario.PasswordHash);

        if (esLaMismaPassword)
        {
        // No cambió la contraseña: conservamos el hash existente, no lo tocamos.
            nuevoUsuario.PasswordHash = viejoUsuario.PasswordHash;
        }
        else
        {
        // Es una contraseña nueva: la hasheamos antes de guardar.
            nuevoUsuario.PasswordHash = BCrypt.Net.BCrypt.HashPassword(nuevoUsuario.PasswordHash);
        }
        
        repoUsuario.ActualizarUsuario(nuevoUsuario);

    }

    private static void ValidarUsuario(Usuario usuario )
	{
		ArgumentNullException.ThrowIfNull(usuario);

		if (string.IsNullOrWhiteSpace(usuario.Nombre))
		{
			throw new ArgumentException("El nombre del usuario es obligatorio.", nameof(usuario));
		}

        if(string.IsNullOrWhiteSpace(usuario.Email))
        {
            throw new ArgumentException("El email del usuario es obligatorio.", nameof(usuario));
        }

        if(string.IsNullOrWhiteSpace(usuario.Apellido))
        {
            throw new ArgumentException("El Apellido del usuario es obligatorio.", nameof(usuario));
        }

        if(usuario.FechaNacimiento > DateTime.Now)
        {
            throw new ArgumentException("Fecha de nacimiento imposible.", nameof(usuario));
        }
        //Time  que haber  algo ante y des pues de un "@" y un "."

        int posicionArroba = usuario.Email.IndexOf('@');
        int posicionPunto = usuario.Email.LastIndexOf('.');

        if (posicionArroba > 0 && posicionPunto > posicionArroba + 1 && posicionPunto < usuario.Email.Length - 1)
        {
            // Estructura mínima válida: texto@texto.texto

            throw new ArgumentException("la estructura del email es incorecta.", nameof(usuario));


        }

	}

    private static void ValidarId(short idEquipo)
	{
		if (idEquipo <= 0)
		{
			throw new ArgumentOutOfRangeException(nameof(idEquipo), "El identificador debe ser mayor que cero.");
		}
	}


    
}
