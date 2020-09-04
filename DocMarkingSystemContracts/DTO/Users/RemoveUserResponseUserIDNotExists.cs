using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace DocMarkingSystemContracts.DTO.Users
{
    public class RemoveUserResponseUserIDNotExists:RemoveUserResponse
    {
        public RemoveUserRequest Request { get; }

        public RemoveUserResponseUserIDNotExists(RemoveUserRequest request)
        {
            Request = request;
        }
    }
}
