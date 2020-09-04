using System;
using System.Collections.Generic;
using System.Text;

namespace DocMarkingSystemContracts.DTO.Documents
{
    public class CreateDocumentRequest
    {
        public string UserID { get; set; }
        public string DocumentName { get; set; }
        public string ImageURL { get; set; }
    }
}
