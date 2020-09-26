using DALContracts;
using DIContract;
using DocMarkingSystemContracts.DTO.Sharing;
using DocMarkingSystemContracts.Interfaces;
using DocMarkingSystemDAL;
using NUnit.Framework;
using ORDAL;
using SharingService;
using SharingWebSocket;
using WebSocketInfra;

namespace ShareServiceTests
{
    public class Tests
    {
        private ISharingService _service;
        [SetUp]
        public void Setup()
        {
            IInfraDAL dal = new InfraDAL();
            var strConn = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))" +
                          "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=XE)));" +
                          "User Id=ZIVPROJ;Password=1234;";
            _service = new SharingServiceImpl(new DocMarkingSystemDALImpl(dal), new SharingWebSocketImpl(new SocketHandlerInfra(new ConnectionManagerInfra())));
            _service.Connect(strConn);

        }

        [Test]
        public void CreateShareOK()
        {
            CreateShareRequest request = new CreateShareRequest(){Share = new Share(){DocID = "1",UserID = "ziv1234@gmail.com" } };
            Response response = _service.CreateShare(request).Result;
            Assert.IsInstanceOf(typeof(CreateShareResponseOK),response);
        }

        [Test]
        public void CreateShareInvalid()
        {
            CreateShareRequest request = new CreateShareRequest() { Share = new Share() { DocID = "90", UserID = "ziv1234@gmail.com" } };
            Response response = _service.CreateShare(request).Result;
            Assert.IsInstanceOf(typeof(CreateShareResponseInvalidID), response);
        }

        [Test]
        public void GetSharedDocumentOK()
        {
            GetSharedDocumentsRequest request = new GetSharedDocumentsRequest(){UserID = "ziv1234@gmail.com"};
            Response response = _service.GetSharedDocuments(request).Result;
            Assert.IsInstanceOf(typeof(GetSharedDocumentsResponseOK), response);
        }

        [Test]
        public void GetSharedDocumentInvalid()
        {
            GetSharedDocumentsRequest request = new GetSharedDocumentsRequest() { UserID = "ziv11234@gmail.com" };
            Response response = _service.GetSharedDocuments(request).Result;
            Assert.IsInstanceOf(typeof(GetSharedDocumentsResponseInvalidUserID), response);
        }
        [Test]
        public void GetSharedUserOK()
        {
            GetSharedUsersRequest request = new GetSharedUsersRequest() { DocID= "1" };
            Response response = _service.GetSharedUsers(request).Result;
            Assert.IsInstanceOf(typeof(GetSharedUsersResponseOK), response);
        }

        [Test]
        public void GetSharedUserInvalid()
        {
            GetSharedUsersRequest request = new GetSharedUsersRequest() { DocID = "90" };
            Response response = _service.GetSharedUsers(request).Result;
            Assert.IsInstanceOf(typeof(GetSharedUsersResponseInvalidDocID), response);
        }

        [Test]
        public void RemoveShareOk()
        {
            RemoveShareRequest request = new RemoveShareRequest() { Share = new Share() { DocID = "1", UserID = "ziv1234@gmail.com" } };
            Response response = _service.RemoveShare(request).Result;
            Assert.IsInstanceOf(typeof(RemoveShareResponseOK), response);
        }
        [Test]
        public void RemoveShareInvalid()
        {
            RemoveShareRequest request = new RemoveShareRequest() { Share = new Share() { DocID = "90", UserID = "ziv1234@gmail.com" } };
            Response response = _service.RemoveShare(request).Result;
            Assert.IsInstanceOf(typeof(RemoveShareResponseInvalidID), response);
        }
    }
}