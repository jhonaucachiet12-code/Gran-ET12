using System.Data;

namespace GranDT.Core.Gran_DT.ConDapper;

public class RepoDapper
{
    protected readonly IDbConnection Conexion;

    public RepoDapper(IDbConnection conexion) => Conexion = conexion;
}

