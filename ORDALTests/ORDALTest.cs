using System.Data;
using DALContracts;
using NUnit.Framework;
using Oracle.ManagedDataAccess.Client;
using ORDAL;

namespace ORDALTests
{
    public class Tests
    {
        private IInfraDAL _dal;
        private IDBConnection _conn;
        [SetUp]
        public void Setup()
        {
            _dal = new InfraDAL();
            var strConn = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))" +
                          "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=XE)));" +
                          "User Id=ZIVPROJ;Password=1234;";
            _conn = _dal.Connect(strConn);
        }

        [Test]
        public void SPTestFound()
        {
            var expected = 1;
            IDBParameter userId = _dal.CreateParameter("UserID", "ziv1234@gmail.com");
            IDBParameter userName = _dal.CreateParameter("UserName", "zivZaarur");

            DataSet ds = _dal.ExecuteSPQuery(_conn, "Login", userId, userName);
            var actual = ds.Tables[0].Rows.Count;
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void SPTestNotFound()
        {
            var expected = 0;

            IDBParameter userId = _dal.CreateParameter("UserID", "ziv1234@gmail.com");
            IDBParameter userName = _dal.CreateParameter("UserName", "ziv");
            
            DataSet ds = _dal.ExecuteSPQuery(_conn, "Login", userId, userName);
            var actual = ds.Tables[0].Rows.Count;
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void QueryTestFound()
        {
            var expected = 1;
            DataSet ds = _dal.ExecuteQuery(_conn, "select * from USERS where userID = 'ziv1234@gmail.com' AND userName = 'zivZaarur' AND removed = 0");
            var actual = ds.Tables[0].Rows.Count;
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void QueryTestNotFound()
        {
            var expected = 0;
            DataSet ds = _dal.ExecuteQuery(_conn, "select * from USERS where userID = 'ziv1234@gmail.com' AND userName = 'ziv' AND removed = 0");
            var actual = ds.Tables[0].Rows.Count;
            Assert.AreEqual(expected, actual);
        }
    }
}