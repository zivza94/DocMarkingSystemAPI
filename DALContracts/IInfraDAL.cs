using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALContracts
{
    public interface IInfraDAL
    {
        IDBConnection Connect(string strConnection);
        IDBParameter CreateParameter(string paramName, object value);
        IDBParameter GetOutParameter();
        DataSet ExecuteSPQuery(IDBConnection connection, string spName,
            params IDBParameter[] parameters);

        DataSet ExecuteQuery(IDBConnection connection, string query);

    }
}
