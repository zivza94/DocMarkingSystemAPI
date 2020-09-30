using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DIContract;
using DocMarkingSystemContracts.DTO.Sharing;
using DocMarkingSystemContracts.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DocMarkingSystemAPI.Controllers
{
    [Route("api/[controller]/{action}")]
    [ApiController]
    public class SharingController : ControllerBase
    {
        private ISharingService _sharingService;

        public SharingController(ISharingService sharingService,IConfiguration conf)
        {
            var strConn = conf.GetValue<string>("Oracle:ConnStr");
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
