using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions options) : base(options) //base is the consructor of IdentityDbContext
        {
        }

        public DbSet<Activity> Activities { get; set; } //the name of the table we create
    }
}

//dotnet ef migrations add IdentityAdded -p Persistence -s API  (migration  command)