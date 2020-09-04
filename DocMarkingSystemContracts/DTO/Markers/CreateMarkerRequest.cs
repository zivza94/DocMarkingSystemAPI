using System;
using System.Collections.Generic;
using System.Text;

namespace DocMarkingSystemContracts.DTO.Markers
{
    public class CreateMarkerRequest
    {
        public string DocID { get; set; }
        public string MarkerType { get; set; }
        public Location MarkerLocation { get; set; }
        public string ForeColor { get; set; }
        public string BackColor { get; set; }
        public string UserID { get; set; }
    }
}
