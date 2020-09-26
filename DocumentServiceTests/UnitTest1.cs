using DALContracts;
using DocMarkingSystemContracts.DTO.Documents;
using DocMarkingSystemContracts.DTO.Users;
using DocMarkingSystemContracts.Interfaces;
using DocMarkingSystemDAL;
using DocumentService;
using DocumentWebSocket;
using NUnit.Framework;
using ORDAL;
using WebSocketInfra;
using WebSocketInfraContracts;

namespace DocumentServiceTests
{
    public class Tests
    {
        private IDocumentService _documentService;
        [SetUp]
        public void Setup()
        {
            IInfraDAL dal = new InfraDAL();
            var strConn = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))" +
                          "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=XE)));" +
                          "User Id=ZIVPROJ;Password=1234;";
            _documentService = new DocumentServiceImpl(new DocMarkingSystemDALImpl(dal), new DocumentSocketImpl(new SocketHandlerInfra(new ConnectionManagerInfra())));
            _documentService.Connect(strConn);

        }
        [Test]
        public void CreateDocOK()
        {
            var request = new CreateDocumentRequest(){DocumentName = "apple",ImageURL = "dfdf",UserID = "ziv1234@gmail.com"};
            var response = _documentService.CreateDocument(request).Result;
            Assert.IsInstanceOf(typeof(CreateDocumentResponseOK),response);
        }
        [Test]
        public void CreateDocInvalidData()
        {
            var request = new CreateDocumentRequest() { DocumentName = "apple", ImageURL = "dfdf", UserID = "z@gmail.com" };
            var response = _documentService.CreateDocument(request).Result;
            Assert.IsInstanceOf(typeof(CreateDocumentResponseInvalidData), response);
        }
        [Test]
        public void GetDocumentsOKFoundDocs()
        {
            var request = new GetDocumentsRequest(){UserID = "ziv1234@gmail.com"};
            var response =  _documentService.GetDocuments(request).Result;
            Assert.AreNotEqual(0,(response as GetDocumentsResponseOK).Documents.Count);
        }
        [Test]
        public void GetDocumentsOKNotFoundDocs()
        {
            var request = new GetDocumentsRequest() { UserID = "ziv111@gmail.com" };
            var response = _documentService.GetDocuments(request).Result;
            Assert.AreEqual(0, (response as GetDocumentsResponseOK).Documents.Count);
        }
        [Test]
        public void RemoveDocOK()
        {
            var request = new RemoveDocumentRequest(){ DocID = "93dd7e10-3ffa-4f8c-9c01-c58fee0a6cd4" };
            var response = _documentService.RemoveDocument(request).Result;
            Assert.IsInstanceOf(typeof(RemoveDocumentResponseOK),response);
        }
        [Test]
        public void RemoveDocInvalid()
        {
            var request = new RemoveDocumentRequest() { DocID = "2" };
            var response = _documentService.RemoveDocument(request).Result;
            Assert.IsInstanceOf(typeof(RemoveDocumentResponseInvalidDocID), response);
        }
    }
}