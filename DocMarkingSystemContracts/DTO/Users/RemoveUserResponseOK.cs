using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace DocMarkingSystemContracts.DTO.Users
{
    public class RemoveUserResponseOK:RemoveUserResponse
    {
        public RemoveUserRequest Request { get; }

        public RemoveUserResponseOK(RemoveUserRequest request)
        {
            Request = request;
        }
    }
}
