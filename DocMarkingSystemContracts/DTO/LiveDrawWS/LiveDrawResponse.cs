using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace DocMarkingSystemContracts.DTO.LiveDrawWS
{
    public class LiveDrawResponse
    {
        [JsonProperty("userID")]
        public string UserID { get; set; }
    }
}
