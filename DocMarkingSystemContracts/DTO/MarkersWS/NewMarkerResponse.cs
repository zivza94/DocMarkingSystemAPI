using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using DIContract;
using DocMarkingSystemContracts.DTO.Markers;
using Newtonsoft.Json;

namespace DocMarkingSystemContracts.DTO.MarkersWS
{
    public class NewMarkerResponse:Response
    {
        [JsonProperty("marker")]
        public Marker Marker { get; set; }
    }
}
