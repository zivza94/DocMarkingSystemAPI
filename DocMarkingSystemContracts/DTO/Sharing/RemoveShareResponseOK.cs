using System;
using System.Collections.Generic;
using System.Text;

namespace DocMarkingSystemContracts.DTO.Sharing
{
    public class RemoveShareResponseOK:RemoveShareResponse
    {

        public RemoveShareRequest Request { get; set; }

        public RemoveShareResponseOK(RemoveShareRequest request)
        {
            Request = request;
        }
    }
}
