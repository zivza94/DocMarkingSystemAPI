using DALContracts;
using DocMarkingSystemContracts.DTO.Users;
using DocMarkingSystemContracts.Interfaces;
using DocMarkingSystemDAL;
using NUnit.Framework;
using ORDAL;
using UserService;

namespace UserServiceTests
{
    public class Tests
    {
        private IUserService _userService;
        [SetUp]
        public void Setup()
        {
            IInfraDAL dal = new InfraDAL();
            var strConn = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))" +
                          "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=XE)));" +
                          "User Id=ZIVPROJ;Password=1234;";
            _userService = new UserServiceImpl(new DocMarkingSystemDALImpl(dal));
            _userService.Connect(strConn);

        }

        [Test]
        public void CreateUserOK()
        {
            User user = new User(){UserID = "ziv2311@gmail.com", UserName = "zivza"};
            var request = new CreateUserRequest(){User =  user};
            var actual = _userService.CreateUser(request);
            Assert.IsInstanceOf(typeof(CreateUserResponseOK),actual);
        }

        [Test]
        public void CreateUserUserIDExists()
        {
            User user = new User() { UserID = "ziv2311@gmail.com", UserName = "zivza" };
            var request = new CreateUserRequest() { User = user };
            var actual = _userService.CreateUser(request);
            Assert.IsInstanceOf(typeof(CreateUserResponseUserIDExist), actual);
        }

        [Test]
        public void LoginOK()
        {
            var request = new LoginRequest() { UserID = "ziv2311@gmail.com" };
            var actual = _userService.Login(request);
            Assert.IsInstanceOf(typeof(LoginResponseOK), actual);
        }

        [Test]
        public void LoginInvalidUserID()
        {
            
            var request = new LoginRequest() { UserID = "ziv" };
            var actual = _userService.Login(request);
            Assert.IsInstanceOf(typeof(LoginResponseInvalidUserID), actual);
        }
        [Test]
        public void RemoveUserOK()
        {
            User user = new User() { UserID = "ziv2311@gmail.com", UserName = "zivza" };
            var request = new RemoveUserRequest() { User = user };
            var actual = _userService.RemoveUser(request);
            Assert.IsInstanceOf(typeof(RemoveUserResponseOK), actual);
        }

        [Test]
        public void RemoveUserUserIDNotExists()
        {
            User user = new User() { UserID = "ziv2311@gmail.com", UserName = "zivza" };
            var request = new RemoveUserRequest() { User = user };
            var actual = _userService.RemoveUser(request);
            Assert.IsInstanceOf(typeof(RemoveUserResponseUserIDNotExists), actual);
        }
    }
}