using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DIContract;
using DocMarkingSystemContracts.DTO.Documents;
using DocMarkingSystemContracts.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DocMarkingSystemAPI.Controllers
{
    [Route("api/[controller]/{action}")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private IDocumentService _documentService;

        public DocumentController(IDocumentService documentService,IConfiguration conf)
        {
            var strConn = conf.GetValue<string>("Oracle:ConnStr");
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