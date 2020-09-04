using System;
using System.Collections.Generic;
using System.Text;
using DocMarkingSystemContracts.DTO.Documents;

namespace DocMarkingSystemContracts.DTO.Sharing
{
    public class GetSharedDocumentsResponseOK:GetSharedDocumentsResponse
    {
        public List<string> Documents { get;}

        public GetSharedDocumentsResponseOK(List<string> documents)
        {
            Documents = documents;
        }
    }
}
