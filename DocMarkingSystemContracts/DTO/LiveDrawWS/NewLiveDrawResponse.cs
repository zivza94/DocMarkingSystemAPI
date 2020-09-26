using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Newtonsoft.Json;

namespace DocMarkingSystemContracts.DTO.LiveDrawWS
{
    public class NewLiveDrawResponse:LiveDrawResponse
    {
        [JsonProperty("lines")]
        public List<Line> Lines { get; set; }
    }
}
