using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.DTO
{
    public class UserDTO
    {
        public object Token { get; set; }
        public string RefreshToken { get; set; }
        public string Message { get; set; }
    }
}
