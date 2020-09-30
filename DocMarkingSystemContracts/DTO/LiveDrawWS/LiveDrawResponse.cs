using System;
using System.Collections.Generic;
using System.Text;
using DIContract;
using Newtonsoft.Json;

namespace DocMarkingSystemContracts.DTO.LiveDrawWS
{
    public class LiveDrawResponse:Response
    {
        [JsonProperty("userID")]
        public string UserID { get; set; }
        
    }
}
