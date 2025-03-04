using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Context
{
    public class UserRegistrationAppContext : DbContext
    {
        public UserRegistrationAppContext(DbContextOptions<UserRegistrationAppContext> options) : base(options) { }
        public virtual DbSet<Entity.UserEntity> Users { get; set; }
    }
}