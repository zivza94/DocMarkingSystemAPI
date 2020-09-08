using System;
using System.Collections.Generic;
using System.Text;

namespace DocMarkingSystemContracts.DTO.Sharing
{
    public class RemoveShareResponseNotAuthorized:RemoveShareResponse
    {
        public RemoveShareRequest Request { get; set; }

        public RemoveShareResponseNotAuthorized(RemoveShareRequest request)
        {
            Request = request;
        }
    }
}
