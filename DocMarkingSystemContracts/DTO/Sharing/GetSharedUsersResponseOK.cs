using System;
using System.Collections.Generic;
using System.Text;
using DocMarkingSystemContracts.DTO.Users;

namespace DocMarkingSystemContracts.DTO.Sharing
{
    public class GetSharedUsersResponseOK:GetSharedUsersResponse
    {
        public List<string> Users { get; }

        public GetSharedUsersResponseOK(List<string> users)
        {
            Users = users;
        }
    }
}
