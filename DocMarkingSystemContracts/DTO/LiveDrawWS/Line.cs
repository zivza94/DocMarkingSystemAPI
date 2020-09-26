using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace DocMarkingSystemContracts.DTO.LiveDrawWS
{
    public class Line
    {
        [JsonProperty("start")]
        public Point Start { get; set; }
        [JsonProperty("end")]
        public Point End { get; set; }
        [JsonProperty("fgColor")]
        public string FgColor { get; set; }
    }
}
