using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DIContract;
using DocMarkingSystemContracts.DTO.Users;
using DocMarkingSystemContracts.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DocMarkingSystemAPI.Controllers
{
    [Route("api/[controller]/{action}")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService,IConfiguration conf)
        {
            var strConn = conf.GetValue<string>("Oracle:ConnStr");
            _userService = userService;
            _userService.Connect(strConn);
        }

        [HttpPost]
        public async Task<Response> CreateUser(CreateUserRequest request)
        {
            return await _userService.CreateUser(request);
        }

        [HttpPost]
        public async Task<Response> Login(LoginRequest request)
        {
            return await _userService.Login(request);
        }

        [HttpPost]
        public async Task<Response> RemoveUser(RemoveUserRequest request)
        {
            return await _userService.RemoveUser(request);
        }
    }
}