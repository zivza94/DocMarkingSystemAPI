using System;
using System.Collections.Generic;
using System.Text;

namespace DocMarkingSystemContracts.DTO.Sharing
{
    public class CreateShareResponseShareExists:CreateShareResponse
    {
        public CreateShareRequest Request { get; set; }

        public CreateShareResponseShareExists(CreateShareRequest request)
        {
            Request = request;
        }
    }
}
