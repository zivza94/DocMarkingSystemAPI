using System;
using System.Collections.Generic;
using System.Text;
using DIContract;
using Newtonsoft.Json;

namespace DocMarkingSystemContracts.DTO.DocumentsWS
{
    public class RemoveDocumentResponse:Response
    {
        [JsonProperty("docID")]
        public string DocID { get; set; }
    }
}
