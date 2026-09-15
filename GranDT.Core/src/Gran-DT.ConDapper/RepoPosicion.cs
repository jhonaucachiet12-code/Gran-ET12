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
        var consulta = @"SELECT * FROM Posiciones";

        var posiciones = _conexion.Query<Posicion>(consulta);

        return posiciones;
    }

    public Posicion? ObtenerPosicionPorId(byte id)
    {
        var consulta = @"SELECT * FROM Posiciones WHERE IdPosicion = @Id";

        var posicion = _conexion.QuerySingleOrDefault<Posicion>(consulta, new { Id = id });

        return posicion;
    }

    public void AgregarPosicion(Posicion posicion)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@IdPosicion", direction: ParameterDirection.Output);
        parametros.Add("@Nombre", posicion.Nombre);

        _conexion.Execute("insertarPosicion", parametros, commandType: CommandType.StoredProcedure);
         posicion.IdPosicion = parametros.Get<byte>("@IdPosicion");


    }

    public void ActualizarPosicion(Posicion posicion)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@IdPosicion", posicion.IdPosicion);
        parametros.Add("@Nombre", posicion.Nombre);

        _conexion.Execute("actualizarPosicion", parametros, commandType: CommandType.StoredProcedure);
    }

    public void EliminarPosicion(byte id)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@IdPosicion", id);

        _conexion.Execute("eliminarPosicion", parametros, commandType: CommandType.StoredProcedure);
    }
}