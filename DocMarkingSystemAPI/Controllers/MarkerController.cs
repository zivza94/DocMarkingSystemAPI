using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DIContract;
using DocMarkingSystemContracts.DTO.Markers;
using DocMarkingSystemContracts.DTO.Sharing;
using DocMarkingSystemContracts.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocMarkingSystemAPI.Controllers
{
    [Route("api/[controller]/{action}")]
    [ApiController]
    public class MarkerController : ControllerBase
    {
        private IMarkerService _markerService;

        public MarkerController(IMarkerService markerService)
        {
            var strConn =
                "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))" +
                "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=XE)));" +
                "User Id=ZIVPROJ;Password=1234;";
            _markerService = markerService;
            _markerService.Connect(strConn);
        }

        [HttpPost]
        public async Task<Response> CreateMarker(CreateMarkerRequest request)
        {
            return await _markerService.CreateMarker(request);
        }

        [HttpPost]
        public async Task<Response> GetMarkers(GetMarkersRequest request)
        {
            return await _markerService.GetMarkers(request);
        }

        [HttpPost]
        public async Task<Response> RemoveMarker(RemoveMarkerRequest request)
        {
            return await _markerService.RemoveMarker(request);
        }
    }
}