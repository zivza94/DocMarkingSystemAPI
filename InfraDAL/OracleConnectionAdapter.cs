using DALContracts;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DIContract;

namespace ORDAL
{
    [Register(Policy.Transient, typeof(IDBConnection))]
    public class OracleConnectionAdapter : IDBConnection
    {
        public OracleConnection Connection { get; set; }
        public OracleConnectionAdapter(string strConnection)
        {
            Connection = new OracleConnection(strConnection);
        }

        public OracleConnectionAdapter()
        {
            
        }

        public void Connect(string strConnection)
        {
            Connection = new OracleConnection(strConnection);
        }

    }
}
