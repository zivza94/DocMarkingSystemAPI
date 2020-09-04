using System;
using System.Collections.Generic;
using System.Text;

namespace DocMarkingSystemContracts.DTO.Users
{
    public class LoginResponseOK:LoginResponse
    {
        public LoginResponseOK(LoginRequest request)
        {
            Request = request;
        }

        public LoginRequest Request { get; }
    }
}
