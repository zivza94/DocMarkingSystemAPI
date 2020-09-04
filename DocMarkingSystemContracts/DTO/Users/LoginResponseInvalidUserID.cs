using System;
using System.Collections.Generic;
using System.Text;

namespace DocMarkingSystemContracts.DTO.Users
{
    public class LoginResponseInvalidUserID : LoginResponse
    {
        public LoginResponseInvalidUserID(LoginRequest request)
        {
            Request = request;
        }

        public LoginRequest Request { get; }
    }
}
