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
        public string ResponseType { get; set; }

        public LiveDrawResponse()
        {
            ResponseType = this.GetType().Name;
        }
    }
}
