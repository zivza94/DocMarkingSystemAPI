using System;
using System.Collections.Generic;
using System.Text;
using DIContract;
using Newtonsoft.Json;

namespace DocMarkingSystemContracts.DTO.DocumentsWS
{
    public class CurrentlyViewingResponse:Response
    {
        [JsonProperty("users")]
        public List<string> Users { get; set; }
    }
}
