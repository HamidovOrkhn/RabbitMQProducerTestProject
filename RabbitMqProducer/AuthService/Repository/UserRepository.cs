using AuthService.Database;
using AuthService.DTO;
using AuthService.Models;
using AuthService.Repository;
using AutoMapper;
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

namespace AuthService.Repository
{
    public class UserRepository :IUserRepository
    {
        public readonly DbService db;
        public readonly IMapper mapper;
        public readonly IConfiguration configuration;

        public UserRepository(DbService db, IConfiguration configuration, IMapper mapper)
        {
            this.db = db;
            this.configuration = configuration;
            this.mapper = mapper;
        }
        
        public UserDTO Login([FromBody] LoginDTO user)
        {
            User userdata = db.UserTb.Where(t => t.Username == user.Username && t.Password == user.Password).FirstOrDefault();
            if (userdata == null)
            {
                return new UserDTO { Message = "False Credentials , try again",Token = null, RefreshToken = "" };
            }
            else
            {

                var claim = new[] { new Claim(ClaimTypes.Name, user.Username) };
                var symmetric = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["SecretKey"]));
                var signing = new SigningCredentials(symmetric, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    issuer: "hh",
                    audience: "hh",
                    expires: DateTime.Now.AddMinutes(10),
                    claims: claim,
                    signingCredentials: signing
                    );

                userdata.RefreshToken = Guid.NewGuid().ToString();
                db.UserTb.Update(userdata);
                db.SaveChanges();
                return new UserDTO  { Message = "Success", Token=new JwtSecurityTokenHandler().WriteToken(token), RefreshToken = userdata.RefreshToken };
            }
        }
     
        public User Register([FromBody] LoginDTO user)
        {
            User test = mapper.Map<LoginDTO, User>(user);
            var existeduser = db.UserTb.FirstOrDefault(a => a.Username == test.Username);
            if (existeduser!=null)
            {
                return test;
            }
            db.Add(test);
            db.SaveChanges();
            return test;
        }
    }
}
