// Test del repositorio de la entidad Equipo con dapper con xunit, permite obtener todos los equipos de la base de datos.
using System;
using System.Collections.Generic;
using GranDT.Core.Model;
using GranDT.Core.Model.IRepos;
using GranDT.Core.Gran_DT.ConDapper;
using Xunit;
using MySqlConnector;


namespace TestGranDT.TestRepo;

public class TestRepoEquipo
{
    private readonly IRepoEquipo _repoEquipo;


    public TestRepoEquipo()
    {
        //var cadena = "Server=localhost;Database=bd_Mundial26;Uid=root;Pwd=1001;";

        var cadena = "Server=localhost;Database=bd_GranET;Uid=root;Pwd=1001;";
        //var cadena = "Server=localhost;Database=bd_GranET;Uid=5to_agbd;Pwd=Trigg3rs!;";
        var conexion = new MySqlConnection(cadena);
        _repoEquipo = new RepoEquipo(conexion);
    }

    [Fact]
    public void ObtenerEquipos_DevuelveListaDeEquipos()
    {
        // Act


        var equipoEsperada = CrearEquipoTemporal();

        try
        {
            _repoEquipo.AgregarEquipo(equipoEsperada);

            var equipos1 = _repoEquipo.ObtenerEquipos();

            Assert.NotNull(equipos1);
            Assert.IsAssignableFrom<IEnumerable<Equipo>>(equipos1);
            Assert.Contains(equipos1,r => r.Nombre == "Boca Juniors");
            Assert.Contains(equipos1, equipos => equipos.IdEquipo == equipoEsperada.IdEquipo);

        }
        finally
        {
            BorrarEquipoTemporal(equipoEsperada);
        }

    }
    

    [Fact]
    public void ObtenerEquipoPorId_DevuelveEquipoExistente()
    {

        var equipoEsperada = CrearEquipoTemporal();

        try
        {
            _repoEquipo.AgregarEquipo(equipoEsperada);

            var equipos1 = _repoEquipo.ObtenerEquipoPorId(equipoEsperada.IdEquipo);

            Assert.NotNull(equipos1);
            Assert.Equal(equipoEsperada.IdEquipo, equipos1.IdEquipo);
            Assert.Equal(equipoEsperada.Nombre, equipos1.Nombre);
            
        }
        finally
        {
            BorrarEquipoTemporal(equipoEsperada);
        }
    }

    [Fact]

    public void ObtenerEquipoPorId_DevuelveNullParaEquipoInexistente()
    {
        // Arrange
        byte idEquipoInexistente = 90;

        // Act
        var equipo = _repoEquipo.ObtenerEquipoPorId(idEquipoInexistente);

        // Assert
        
        Assert.Null(equipo);
    }

    [Fact]
    public void AgregarEquipo_AgregaNuevoEquipo()
    {
        
        // Arrange
        var nuevoEquipo = new Equipo { Nombre = "Nuevo Equipo23" };

        
        try
        {
            _repoEquipo.AgregarEquipo(nuevoEquipo);
            Assert.True(nuevoEquipo.IdEquipo > 0);
            var equipoAgregado = _repoEquipo.ObtenerEquipoPorId(nuevoEquipo.IdEquipo);
            Assert.NotNull(equipoAgregado);
            Assert.Equal("Nuevo Equipo23", equipoAgregado.Nombre);
        }
        finally
        {
            BorrarEquipoTemporal(nuevoEquipo);
        }
        // Act
        // Assert
        
    }

    [Fact]
    public void ActualizarEquipo_ActualizaEquipoExistente()
    {


        var equipoTemporal = CrearEquipoTemporal();

        try
        {
            _repoEquipo.AgregarEquipo(equipoTemporal);

            

            equipoTemporal.Nombre = $"Equipo Actualizado";

            _repoEquipo.ActualizarEquipo(equipoTemporal);

            var equipoActualizada = _repoEquipo.ObtenerEquipoPorId(equipoTemporal.IdEquipo);
            Assert.NotNull(equipoActualizada);
            Assert.Equal(equipoTemporal.Nombre, equipoActualizada.Nombre);
        }
        finally
        {
            BorrarEquipoTemporal(equipoTemporal);
        }
    }

    [Fact]
    public void EliminarEquipo_EliminaEquipoExistente()
    {
        var equipoTemporal = CrearEquipoTemporal();

        try
        {
            _repoEquipo.AgregarEquipo(equipoTemporal);

            _repoEquipo.EliminarEquipo(equipoTemporal.IdEquipo);

            Assert.Null(_repoEquipo.ObtenerEquipoPorId(equipoTemporal.IdEquipo));
        }
        finally
        {
            BorrarEquipoTemporal(equipoTemporal);
        }
    }

    private static Equipo CrearEquipoTemporal()
    {
        return new Equipo
        {
            IdEquipo= 1,
            Nombre ="Boca Juniors" 
        };
    }

    private void BorrarEquipoTemporal(Equipo equipo)
    {
        if (equipo.IdEquipo > 0)
		{
			_repoEquipo.EliminarEquipo(equipo.IdEquipo);
		}
    }



    
}
