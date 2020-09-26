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
        IDocMarkingSystemDAL _dal;
        IDBConnection _conn;
        public UserServiceImpl(IDocMarkingSystemDAL dal)
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
            if(!_dal.IsUserExists(_conn,request.User.UserID))
            {
                try
                {
                    _dal.CreateUser(_conn,request.User);
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
                DataSet ds = _dal.Login(_conn, request.UserID);
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
            if (_dal.IsUserExists(_conn,request.User.UserID))
            {
                try
                {
                    _dal.RemoveUser(_conn,request.User.UserID);
                    retval = new RemoveUserResponseOK(request);
                }
                catch
                {
                    retval = new AppResponseError("Couldn't remove user, due to error in dataBase");
                }
            }
            return retval;
        }

    }
}
