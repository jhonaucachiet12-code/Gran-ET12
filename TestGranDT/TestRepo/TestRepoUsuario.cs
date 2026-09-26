using System;
using System.Collections.Generic;
using GranDT.Core.Model;
using GranDT.Core.Model.IRepos;
using GranDT.Core.Gran_DT.ConDapper;
using Xunit;
using MySqlConnector;
/*namespace TestGranDT.TestRepo
{
    public class TestRepoUsuario
    {
        private readonly IRepoUsuario _repoUsuario;

        public TestRepoUsuario()
        {
            var cadena = "Server=localhost;Database=bd_GranET;Uid=5to_agbd;Pwd=Trigg3rs!;";
            var conexion = new MySqlConnection(cadena);
            _repoUsuario = new RepoUsuario(conexion);
        }

        [Fact]
        public void ObtenerUsuarios_DevuelveListaDeUsuarios()
        {
            // Act
            var usuarios = _repoUsuario.ObtenerUsuarios();

            // Assert
            Assert.NotNull(usuarios);
            Assert.IsAssignableFrom<IEnumerable<Usuario>>(usuarios);
            Assert.NotEmpty(usuarios);
            Assert.Contains(usuarios, u => u.IdUsuario > 0 && !string.IsNullOrWhiteSpace(u.Nombre));
            Assert.Contains(usuarios, u => u.IdUsuario == 1 && u.Nombre == "Juan Perez");
        }
    }
}*/