using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace DocMarkingSystemContracts.DTO.Markers
{
    public class Location
    {
        [JsonProperty("pointX")]
        public Double PointX { get; set; }
        [JsonProperty("pointY")]
        public Double PointY { get; set; }
        [JsonProperty("radiusX")]
        public Double RadiusX { get; set; }
        [JsonProperty("radiusY")]
        public Double RadiusY { get; set; }
    }
}
