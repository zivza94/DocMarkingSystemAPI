using System;
using System.Collections.Generic;
using System.Text;

namespace DocMarkingSystemContracts.DTO.Sharing
{
    public class RemoveShareResponseInvalidID : RemoveShareResponse
    {

        public RemoveShareRequest Request { get; set; }

        public RemoveShareResponseInvalidID(RemoveShareRequest request)
        {
            Request = request;
        }
    }
}
