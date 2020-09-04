using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DIContract;
using DocMarkingSystemContracts.DTO.Sharing;
using DocMarkingSystemContracts.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocMarkingSystemAPI.Controllers
{
    [Route("api/[controller]/{action}")]
    [ApiController]
    public class SharingController : ControllerBase
    {
        private ISharingService _sharingService;

        public SharingController(ISharingService sharingService)
        {
            var strConn =
                "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))" +
                "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=XE)));" +
                "User Id=ZIVPROJ;Password=1234;";
            _sharingService = sharingService;
            _sharingService.Connect(strConn);
        }

        [HttpPost]
        public async Task<Response> CreateShare(CreateShareRequest request)
        {
            return await _sharingService.CreateShare(request);
        }

        [HttpPost]
        public async Task<Response> GetSharedDocuments(GetSharedDocumentsRequest request)
        {
            return await _sharingService.GetSharedDocuments(request);
        }

        [HttpPost]
        public async Task<Response> GetSharedUsers(GetSharedUsersRequest request)
        {
            return await _sharingService.GetSharedUsers(request);
        }

        [HttpPost]
        public async Task<Response> RemoveShare(RemoveShareRequest request)
        {
            return await _sharingService.RemoveShare(request);
        }
    }
}
