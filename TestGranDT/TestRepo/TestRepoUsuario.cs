using System;
using System.Collections.Generic;
using GranDT.Core.Model;
using GranDT.Core.Model.IRepos;
using GranDT.Core.Gran_DT.ConDapper;
using Xunit;
using MySqlConnector;

namespace TestGranDT.TestRepo;

public class TestRepoUsuario
{
	private readonly IRepoUsuario _repoUsuario;

	public TestRepoUsuario()
	{
		var cadena = "Server=localhost;Database=bd_GranET;Uid=root;Pwd=1001;";
		var conexion = new MySqlConnection(cadena);
		_repoUsuario = new RepoUsuario(conexion);
	}

	[Fact]
	public void ObtenerUsuarios_DevuelveListaDeUsuarios()
	{
		var usuarioEsperado = CrearUsuarioTemporal();

		try
		{
			_repoUsuario.RegistrarUsario(usuarioEsperado, usuarioEsperado.PasswordHash);

			var usuarios = _repoUsuario.ObtenerUsuarios();

			Assert.NotNull(usuarios);
			Assert.IsAssignableFrom<IEnumerable<Usuario>>(usuarios);
			Assert.Contains(usuarios, usuario => usuario.Email == usuarioEsperado.Email);
		}
		finally
		{
			EliminarSiFueRegistrado(usuarioEsperado);
		}
	}

	[Fact]
	public void ObtenerPorEmail_DevuelveUsuarioExistente()
	{
		var usuarioEsperado = CrearUsuarioTemporal();

		try
		{
			_repoUsuario.RegistrarUsario(usuarioEsperado, usuarioEsperado.PasswordHash);

			var usuario = _repoUsuario.ObtenerPorEmail(usuarioEsperado.IdUsuario);

			Assert.NotNull(usuario);
			Assert.Equal(usuarioEsperado.IdUsuario, usuario.IdUsuario);
			Assert.Equal(usuarioEsperado.Nombre, usuario.Nombre);
			Assert.Equal(usuarioEsperado.Email, usuario.Email);
			Assert.Equal(usuarioEsperado.IdRol, usuario.Roles.IdRol);
			
		}
		finally
		{
			EliminarSiFueRegistrado(usuarioEsperado);
		}
	}

	[Fact]
	public void ObtenerPorEmail_DevuelveNullParaUsuarioInexistente()
	{
		var usuario = _repoUsuario.ObtenerPorEmail(0);

		Assert.Null(usuario);
	}

	[Fact]
	public void ActualizarUsuario_ActualizaUsuarioExistente()
	{
		var usuario = CrearUsuarioTemporal();

		try
		{
			_repoUsuario.RegistrarUsario(usuario, usuario.PasswordHash);
			usuario.Nombre = "Nombre actualizado";
			usuario.Apellido = "Apellido actualizado";
			usuario.Email = $"actualizado-{Guid.NewGuid():N}@test.local";
			usuario.FechaNacimiento = new DateTime(1995, 5, 12);
			usuario.PasswordHash = "hash-actualizado";

			_repoUsuario.ActualizarUsuario(usuario);

			var usuarioActualizado = _repoUsuario.ObtenerPorEmail(usuario.IdUsuario);

			Assert.NotNull(usuarioActualizado);
			Assert.Equal(usuario.Nombre, usuarioActualizado.Nombre);
			Assert.Equal(usuario.Apellido, usuarioActualizado.Apellido);
			Assert.Equal(usuario.Email, usuarioActualizado.Email);
			Assert.Equal(usuario.FechaNacimiento, usuarioActualizado.FechaNacimiento);
			Assert.Equal(usuario.PasswordHash, usuarioActualizado.PasswordHash);
		}
		finally
		{
			EliminarSiFueRegistrado(usuario);
		}
	}

	[Fact]
	public void EliminarUsuario_EliminaUsuarioExistente()
	{
		var usuario = CrearUsuarioTemporal();

		try
		{
			_repoUsuario.RegistrarUsario(usuario, usuario.PasswordHash);

			_repoUsuario.EliminarUsuario(usuario.IdUsuario);

			Assert.Null(_repoUsuario.ObtenerPorEmail(usuario.IdUsuario));
		}
		finally
		{
			EliminarSiFueRegistrado(usuario);
		}
	}

	private static Usuario CrearUsuarioTemporal()
	{
		return new Usuario
		{
			Nombre = "Usuario de prueba",
			Apellido = "Temporal",
			Email = $"test-{Guid.NewGuid():N}@test.local",
			FechaNacimiento = new DateTime(1990, 1, 1),
			PasswordHash = "hash-de-prueba",
			IdRol = 1,
			Roles = new Rol { IdRol = 1, Nombre = "Usuario" }
		};
	}

	private void EliminarSiFueRegistrado(Usuario usuario)
	{
		if (usuario.IdUsuario > 0)
		{
			_repoUsuario.EliminarUsuario(usuario.IdUsuario);
		}
	}
}
