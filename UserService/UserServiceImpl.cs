using System.Data;
using System.Net.Http.Headers;
using System.Security.Authentication.ExtendedProtection;
using System.Threading.Tasks;
using DALContracts;
using DIContract;
using DocMarkingSystemContracts.DTO;
using DocMarkingSystemContracts.DTO.Users;
using DocMarkingSystemContracts.Interfaces;

namespace UserService
{
    [Register(Policy.Transient,typeof(IUserService))]
    public class UserServiceImpl : IUserService
    {
        IInfraDAL _dal;
        IDBConnection _conn;
        public UserServiceImpl(IInfraDAL dal)
        {
            _dal = dal;
        }
        public void Connect(string connStr)
        {
            _conn = _dal.Connect(connStr);
        }
        public async Task<Response> CreateUser(CreateUserRequest request)
        {
            Response retval = new CreateUserResponseUserIDExist(request);
            DataSet validateDs = _dal.ExecuteQuery(_conn, "SELECT * FROM USERS WHERE USERID = '" + request.User.UserID + "'");
            if(validateDs.Tables[0].Rows.Count == 0)
            {
                try
                {
                    IDBParameter userName = _dal.CreateParameter("P_USERNAME", request.User.UserName);
                    IDBParameter userID = _dal.CreateParameter("P_USERID", request.User.UserID);
                    _dal.ExecuteSPQuery(_conn, "CreateUser", userID, userName);
                    retval = new CreateUserResponseOK(request);
                }
                catch
                {
                    retval = new AppResponseError("Couldn't create user, due to error in dataBase");
                }
            }

            return retval;
        }

        public async Task<Response> Login(LoginRequest request)
        {
            try
            {
                IDBParameter userID = _dal.CreateParameter("UserID", request.UserID);
                IDBParameter outParam = _dal.GetOutParameter();
                DataSet ds = _dal.ExecuteSPQuery(_conn, "Login", userID, outParam);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    return new LoginResponseInvalidUserID(request);
                }

                return new LoginResponseOK(request);
            }
            catch
            {
                return new AppResponseError("Error in server in Login");
            }
        }

        public async Task<Response> RemoveUser(RemoveUserRequest request)
        {
            Response retval = new RemoveUserResponseUserIDNotExists(request);
            DataSet validateDs = _dal.ExecuteQuery(_conn, "SELECT * FROM USERS WHERE USERID = '" + request.User.UserID + "' AND REMOVED = 0");
            if (validateDs.Tables[0].Rows.Count != 0)
            {
                try
                {
                    IDBParameter userID = _dal.CreateParameter("UserID", request.User.UserID);
                    _dal.ExecuteSPQuery(_conn, "RemoveUser", userID);
                    retval = new RemoveUserResponseOK(request);
                }
                catch
                {
                    retval = new AppResponseError("Couldn't create user, due to error in dataBase");
                }
            }
            return retval;
        }

    }
}
