using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DIContract;
using DocMarkingSystemContracts.DTO.Documents;
using DocMarkingSystemContracts.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocMarkingSystemAPI.Controllers
{
    [Route("api/[controller]/{action}")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private IDocumentService _documentService;

        public DocumentController(IDocumentService documentService)
        {
            var strConn =
                "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))" +
                "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=XE)));" +
                "User Id=ZIVPROJ;Password=1234;";
            _documentService = documentService;
            _documentService.Connect(strConn);
        }

        [HttpPost]
        public async Task<Response> CreateDocument(CreateDocumentRequest request)
        {
            return await _documentService.CreateDocument(request);
        }

        [HttpPost]
        public async Task<Response> GetDocuments(GetDocumentsRequest request)
        {
            return await _documentService.GetDocuments(request);
        }

        [HttpPost]
        public async Task<Response> GetDocument(GetDocumentRequest request)
        {
            return await _documentService.GetDocument(request);
        }

        [HttpPost]
        public async Task<Response> RemoveDocument(RemoveDocumentRequest request)
        {
            return await _documentService.RemoveDocument(request);
        }

    }
}