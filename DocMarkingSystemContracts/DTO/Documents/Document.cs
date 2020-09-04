using System;
using System.Collections.Generic;
using System.Text;

namespace DocMarkingSystemContracts.DTO.Documents
{
    public class Document
    {
        public string UserID { get; set; }
        public string DocID { get; set; }
        public string DocumentName { get; set; }
        public string ImageURL { get; set; }
    }
}
