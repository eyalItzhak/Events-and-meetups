using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : IdentityDbContext<AppUser>//base is the consructor of IdentityDbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Activity> Activities { get; set; } //cerate the db
        public DbSet<ActivityAttendee> ActivityAttendees { get; set; } //cerate the db
        public DbSet<Photo> Photos { get; set; } //cerate the db
        

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ActivityAttendee>(x => x.HasKey(aa => new { aa.AppUserId, aa.ActivityId }));//config ActivityAttendee prime ket from combine AppUserId and ActivityId
                                                                                                       //many to many relationship appuser to many Activities and Activity has many Attendees (look on domain folder)

            builder.Entity<ActivityAttendee>()
                .HasOne(u => u.AppUser)
                .WithMany(u => u.Activities)
                .HasForeignKey(aa => aa.AppUserId);

            builder.Entity<ActivityAttendee>()
                .HasOne(u => u.Activity)
                .WithMany(u => u.Attendees)
                .HasForeignKey(aa => aa.ActivityId);
        }
    }
}

//dotnet ef migrations add IdentityAdded -p Persistence -s API  (migration  command =>create by this class and domain folder => "IdentityAdded" is the name when make the migration) 