using AuthService.DTO;
using AuthService.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AuthService.Infrastructure.Mapping
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<LoginDTO, User>();
            CreateMap<User, LoginDTO>();

        }
    }
}
