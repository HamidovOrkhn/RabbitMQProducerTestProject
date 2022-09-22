using AuthService.Database;
using AuthService.DTO;
using AuthService.Models;
using AuthService.Repository;
using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Controllers
{
    [Route("api/identityservice")]
    [ApiController]
    public class AuthController : ControllerBase 
    {
        public readonly IUserRepository rp;
        public readonly IBusControl bus;
        public AuthController(IUserRepository rp,IBusControl bus)
        {
            this.rp = rp;
            this.bus = bus;

        }
       
        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login([FromBody] LoginDTO user)
        {
            Uri uri = new Uri("rabbitmq://localhost/orxan_queue");
            var endpoint = await bus.GetSendEndpoint(uri);
            await endpoint.Send(user);
            UserDTO data = rp.Login(user);   
            return Ok(data);
        }
        [HttpPost("register")]
        public  ActionResult<User> Register([FromBody] LoginDTO user)
        {
            User data = rp.Register(user);
            return Ok(data);

        }
      
        [HttpGet("getuser")]
        public IActionResult GetUser() {
            var data = "Orxan";
            return Ok(data);
        }
    }
}
