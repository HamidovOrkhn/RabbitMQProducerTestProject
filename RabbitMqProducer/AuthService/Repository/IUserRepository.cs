using AuthService.DTO;
using AuthService.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Repository
{
    public interface IUserRepository
    {
        public UserDTO Login([FromBody] LoginDTO user);
        public User Register([FromBody] LoginDTO user);
    }
}
