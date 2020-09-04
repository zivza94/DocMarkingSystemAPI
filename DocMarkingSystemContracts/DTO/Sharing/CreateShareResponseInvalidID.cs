using System;
using System.Collections.Generic;
using System.Text;

namespace DocMarkingSystemContracts.DTO.Sharing
{
    public class CreateShareResponseInvalidID:CreateShareResponse
    {
        public CreateShareRequest Request { get; }

        public CreateShareResponseInvalidID(CreateShareRequest request)
        {
            Request = request;
        }
    }
}
