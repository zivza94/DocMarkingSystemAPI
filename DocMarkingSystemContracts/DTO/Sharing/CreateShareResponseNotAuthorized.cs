using System;
using System.Collections.Generic;
using System.Text;

namespace DocMarkingSystemContracts.DTO.Sharing
{
    public class CreateShareResponseNotAuthorized:CreateShareResponse
    {
        public CreateShareRequest Request { get; }

        public CreateShareResponseNotAuthorized(CreateShareRequest request)
        {
            Request = request;
        }
    }
}
