using System;
using System.Collections.Generic;
using System.Text;

namespace DocMarkingSystemContracts.DTO.Users
{
    public class CreateUserResponseOK:CreateUserResponse
    {
        public CreateUserRequest Request { get; }

        public CreateUserResponseOK(CreateUserRequest request)
        {
            Request = request;

        }
    }
}
