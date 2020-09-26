using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json.Serialization;
using DIContract;
using Newtonsoft.Json;

namespace DocMarkingSystemContracts.DTO.MarkersWS
{
    public class NewFreeDrawResponse:Response
    {
        
        [JsonProperty("startPoint")]
        public Point StartPoint { get; set; }
        [JsonProperty("endPoint")]
        public Point EndPoint { get; set; }

    }
}
