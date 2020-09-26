using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace DocMarkingSystemContracts.DTO.LiveDrawWS
{
    public class NewLiveDrawRequest:LiveDrawRequest
    {
        public Line Line { get; set; }
    }
}
