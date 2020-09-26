using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Newtonsoft.Json;

namespace DocMarkingSystemContracts.DTO.Markers
{
    public enum MarkerType { Rectangle, Ellipse}
    public class Marker
    {
        [JsonProperty("docID")]
        public string DocID { get; set; }
        [JsonProperty("markerID")]
        public string MarkerID { get; set; }
        [JsonProperty("markerType")]
        public string MarkerType { get; set; }
        [JsonProperty("markerLocation")]
        public Location MarkerLocation { get; set; }
        [JsonProperty("foreColor")]
        public string ForeColor { get; set; }
        [JsonProperty("backColor")]
        public string BackColor { get; set; }
        [JsonProperty("userID")]
        public string UserID { get; set; }
    }
}
