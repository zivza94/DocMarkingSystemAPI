using System;
using System.Collections.Generic;
using System.Text;

namespace DocMarkingSystemContracts.DTO.Sharing
{
    public class CreateShareResponseOK:CreateShareResponse
    {
        public CreateShareRequest Request { get; }

        public CreateShareResponseOK(CreateShareRequest request)
        {
            Request = request;
        }
    }
}
