using System;
using System.Collections.Generic;
using System.Text;

namespace DocMarkingSystemContracts.DTO.Sharing
{
    public class CreateShareRequest
    {
        public string UserID { get; set; }
        public Share Share { get; set; }
    }
}
