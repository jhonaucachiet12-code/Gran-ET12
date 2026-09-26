using System;
using System.Collections.Generic;
using GranDT.Core.Model.IRepos;
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using GranDT.Core.Model;

namespace GranDT.Core.Gran_DT.ConDapper;

public class RepoPosicion : RepoDapper , IRepoPosicion
{
    public RepoPosicion(IDbConnection conexion) 
    : base(conexion){}

    public IEnumerable<Posicion> ObtenerPosiciones()
    {
        var consulta = @"SELECT * FROM Posicion";

        var posiciones = _conexion.Query<Posicion>(consulta);

        return posiciones;
    }

    public Posicion? ObtenerPosicionPorId(byte IdPosicion)
    {
        var consulta = @"SELECT * FROM Posicion WHERE idPosicion = @IdPosicion";

        var posicion = _conexion.QuerySingleOrDefault<Posicion>(consulta, new { idPosicion = IdPosicion });

        return posicion;
    }

    public void AgregarPosicion(Posicion posicion)
    {
        var parametros = new DynamicParameters();
        parametros.Add("unIdPosicion", direction: ParameterDirection.Output);
        parametros.Add("unNombre", posicion.Nombre);

        _conexion.Execute("insertarPosicion", parametros, commandType: CommandType.StoredProcedure);
         posicion.IdPosicion = parametros.Get<byte>("unIdPosicion");


    }

    public void ActualizarPosicion(Posicion posicion)
    {
        var parametros = new DynamicParameters();
        parametros.Add("unIdPosicion", posicion.IdPosicion);
        parametros.Add("unNombre", posicion.Nombre);

        _conexion.Execute("actualizarPosicion", parametros, commandType: CommandType.StoredProcedure);
    }

    public void EliminarPosicion(byte IdPosicion)
    {
        var parametros = new DynamicParameters();
        parametros.Add("unIdPosicion", IdPosicion);

        _conexion.Execute("eliminarPosicion", parametros, commandType: CommandType.StoredProcedure);
    }
}