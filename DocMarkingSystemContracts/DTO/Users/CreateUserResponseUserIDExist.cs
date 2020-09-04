using System;
using System.Collections.Generic;
using System.Text;

namespace DocMarkingSystemContracts.DTO.Users
{
    public class CreateUserResponseUserIDExist:CreateUserResponse
    {
        public CreateUserRequest Request { get; }

        public CreateUserResponseUserIDExist(CreateUserRequest request)
        {
            Request = request;
        }
    }
}
