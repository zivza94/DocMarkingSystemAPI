using System;
using System.Collections.Generic;
using System.Text;
using DIContract;
using DocMarkingSystemContracts.DTO.Documents;
using Newtonsoft.Json;

namespace DocMarkingSystemContracts.DTO.DocumentsWS
{
    public class NewDocumentResponse:Response
    {
        [JsonProperty("document")]
        public Document Document { get; set; }
    }
}
