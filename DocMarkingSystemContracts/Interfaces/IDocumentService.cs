using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DIContract;
using DocMarkingSystemContracts.DTO.Documents;

namespace DocMarkingSystemContracts.Interfaces
{
    
    public interface IDocumentService
    {
        public void Connect(string connStr);
        public Task<Response> GetDocuments(GetDocumentsRequest request);
        public Task<Response> RemoveDocument(RemoveDocumentRequest request);
        public Task<Response> GetDocument(GetDocumentRequest request);
        public Task<Response> CreateDocument(CreateDocumentRequest request);
    }
}
