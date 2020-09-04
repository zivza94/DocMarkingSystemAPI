using Oracle.ManagedDataAccess.Client;
using System.Data;
using DALContracts;
using DIContract;

namespace ORDAL
{
    [Register(Policy.Transient,typeof(IInfraDAL))]
    public class InfraDAL : IInfraDAL
    {
        public IDBConnection Connect(string strConnection)
        {
            IDBConnection conn = new OracleConnectionAdapter();
            conn.Connect(strConnection);
            return conn;
        }

        public IDBParameter CreateParameter(string paramName, object value)
        {
            return new OracleParameterAdapter(){ParameterName = paramName, Value = value};
        }

        public IDBParameter GetOutParameter()
        {
            var outParam = new OracleParameter();
            outParam.ParameterName = "p_RETVAL";
            outParam.OracleDbType = OracleDbType.RefCursor;
            outParam.Direction = ParameterDirection.Output;
            return new OracleParameterAdapter(){Parameter = outParam};
        }
        private DataSet getDataSet(OracleCommand command)
        {
            DataSet ds = new DataSet();
            OracleDataAdapter da = new OracleDataAdapter(command);
            da.Fill(ds);
            return ds;



        }
        public DataSet ExecuteSPQuery(IDBConnection connection, string spName, params IDBParameter[] parameters)
        {
            var command = new OracleCommand();
            command.CommandText = spName;
            command.CommandType = CommandType.StoredProcedure;
            command.Connection = (connection as OracleConnectionAdapter).Connection;


            foreach (var parameter in parameters)
                command.Parameters.Add((parameter as OracleParameterAdapter).Parameter);
            
            //exec
            return getDataSet(command);
        }
        public DataSet ExecuteQuery(IDBConnection connection, string query)
        {
            var command = new OracleCommand();
            command.CommandText = query;
            command.Connection = (connection as OracleConnectionAdapter).Connection;
            return getDataSet(command);

        }






    };
}
