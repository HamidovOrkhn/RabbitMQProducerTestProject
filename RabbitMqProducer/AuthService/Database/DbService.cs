using AuthService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Database
{
    public class DbService : DbContext
    {
        public DbService(DbContextOptions<DbService> options) : base(options)
        {

        }
       public DbSet<User> UserTb { get; set; }

    }
}
