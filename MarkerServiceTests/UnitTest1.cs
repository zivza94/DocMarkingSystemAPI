using DALContracts;
using DIContract;
using DocMarkingSystemContracts.DTO.Markers;
using DocMarkingSystemContracts.Interfaces;
using MarkerService;
using NUnit.Framework;
using ORDAL;

namespace MarkerServiceTests
{
    public class Tests
    {
        private IMarkerService _service;
        [SetUp]
        public void Setup()
        {
            /*IInfraDAL dal = new InfraDAL();
            var strConn = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))" +
                          "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=XE)));" +
                          "User Id=ZIVPROJ;Password=1234;";
            _service = new MarkerServiceImpl(dal);
            _service.Connect(strConn);*/

        }

        [Test]
        public void CreateMarkerTest()
        {
            CreateMarkerRequest request = new CreateMarkerRequest()
            {
                BackColor = "black",DocID = "1",ForeColor = "red",
                MarkerLocation = new Location() { PointX = 16,PointY = 16,RadiusX = 3,RadiusY = 1}, MarkerType = "Rectangle",
                UserID = "ziv1234@gmail.com"
            };
            Response response = _service.CreateMarker(request).Result;
            Assert.IsInstanceOf(typeof(CreateMarkerResponseOK),response);
        }
        [Test]
        public void GetMarkersTestOK()
        {
            GetMarkersRequest request = new GetMarkersRequest(){DocID = "1"};
            Response response = _service.GetMarkers(request).Result;
            Assert.IsInstanceOf(typeof(GetMarkersResponseOk), response);
        }
        [Test]
        public void GetMarkerTestInvalid()
        {
            GetMarkersRequest request = new GetMarkersRequest() { DocID = "2" };
            Response response = _service.GetMarkers(request).Result;
            Assert.IsInstanceOf(typeof(GetMarkersResponseInvalidDocID), response);
        }
        [Test]
        public void RemoveMarkerTestOK()
        {
            RemoveMarkerRequest request = new RemoveMarkerRequest(){MarkerID = "c0e16305-9eb3-4eb0-b5e2-e00bfe5a00fb" };
            Response response = _service.RemoveMarker(request).Result;
            Assert.IsInstanceOf(typeof(RemoveMarkerResponseOK),response);
        }
        [Test]
        public void RemoveMarkerTestInvalidMarkerID()
        {
            RemoveMarkerRequest request = new RemoveMarkerRequest() { MarkerID = "2" };
            Response response = _service.RemoveMarker(request).Result;
            Assert.IsInstanceOf(typeof(RemoveMarkerResponseInvalidID), response);
        }
    }
}