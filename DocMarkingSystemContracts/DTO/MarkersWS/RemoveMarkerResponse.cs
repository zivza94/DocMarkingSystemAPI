using System;
using System.Collections.Generic;
using System.Text;
using DIContract;
using Newtonsoft.Json;

namespace DocMarkingSystemContracts.DTO.MarkersWS
{
    public class RemoveMarkerResponse:Response
    {
        [JsonProperty("markerID")]
        public string MarkerID { get; set; }
    }
}
