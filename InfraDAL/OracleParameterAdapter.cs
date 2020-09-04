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
    [Register(Policy.Transient, typeof(IDBParameter))]
    public class OracleParameterAdapter : IDBParameter
    {
        public OracleParameter Parameter { get; set; }

        public OracleParameterAdapter()
        {
            Parameter = new OracleParameter();
        }
        public string ParameterName { get => Parameter.ParameterName; set => Parameter.ParameterName = value; }
        public object Value { get => Parameter.Value; set => Parameter.Value = value; }
    }
}
