using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DIContract;
using DocMarkingSystemContracts.DTO.Users;

namespace DocMarkingSystemContracts.Interfaces
{
    public interface IUserService
    {
        public void Connect(string connStr);
        public Task<Response> CreateUser(CreateUserRequest request);
        public Task<Response> Login(LoginRequest request);
        public Task<Response> RemoveUser(RemoveUserRequest request);
    }
}
