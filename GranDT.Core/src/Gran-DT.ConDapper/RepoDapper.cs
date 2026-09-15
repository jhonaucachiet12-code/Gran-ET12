using System.Data;
using Dapper;

namespace GranDT.Core.Gran_DT.ConDapper;

public class RepoDapper
{
    protected IDbConnection _conexion;

    protected RepoDapper(IDbConnection conexion) => _conexion = conexion;

    protected static DynamicParameters AsignarParametros(Action<DynamicParameters> configuracion)
    {
        var parametros = new DynamicParameters();
        configuracion(parametros);
        return parametros;
    }
}

