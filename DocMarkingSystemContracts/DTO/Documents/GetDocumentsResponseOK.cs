using System;
using System.Collections.Generic;
using System.Text;

namespace DocMarkingSystemContracts.DTO.Documents
{
    public class GetDocumentsResponseOK: GetDocumentsResponse
    {
        public List<Document> Documents { get; }
        public GetDocumentsResponseOK(List<Document> documents)
        {
            Documents = documents;
        }
    }
}
