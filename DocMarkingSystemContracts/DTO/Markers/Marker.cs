using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace DocMarkingSystemContracts.DTO.Markers
{
    public enum MarkerType { Rectangle, Ellipse}
    public class Marker
    {
        public string DocID { get; set; }
        public string MarkerID { get; set; }
        public string MarkerType { get; set; }
        public Location MarkerLocation { get; set; }
        public string ForeColor { get; set; }
        public string BackColor { get; set; }
        public string UserID { get; set; }
    }
}
