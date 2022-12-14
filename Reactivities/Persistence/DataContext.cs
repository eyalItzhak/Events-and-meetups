using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions options) : base(options)//base is the consructor of IdentityDbContext
        {
        }

        public DbSet<Activity> Activities { get; set; } //cerate the db
        public DbSet<ActivityAttendee> ActivityAttendees { get; set; } //cerate the db
        public DbSet<Photo> Photos { get; set; } //cerate the db
        public DbSet<Comment> Comments { get; set; }
        public DbSet<UserFollowing> UserFollowings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ActivityAttendee>(x => x.HasKey(aa => new { aa.AppUserId, aa.ActivityId }));//config ActivityAttendee prime ket from combine AppUserId and ActivityId
                                                                                                       //many to many relationship appuser to many Activities and Activity has many Attendees (look on domain folder)
            builder.Entity<ActivityAttendee>()
                .HasOne(u => u.AppUser)
                .WithMany(a => a.Activities)
                .HasForeignKey(aa => aa.AppUserId);

            builder.Entity<ActivityAttendee>()
                .HasOne(u => u.Activity)
                .WithMany(a => a.Attendees)
                .HasForeignKey(aa => aa.ActivityId);
                
            builder.Entity<Comment>()
                .HasOne(a => a.Activity)
                .WithMany(c => c.Comments)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserFollowing>(b=>{
                b.HasKey(k =>new{k.ObserverId,k.TargetId}); //key for UserFollowing db
                //the rlationship
                b.HasOne(o => o.Observer)  // for every Observer can have many Followings
                    .WithMany(f => f.Followings)
                    .HasForeignKey(o=>o.ObserverId)
                    .OnDelete(DeleteBehavior.Cascade); //what to do when Delete user profile...

                b.HasOne(o => o.Target)  // for every Target can have many Followers
                    .WithMany(f => f.Followers)
                    .HasForeignKey(o=>o.TargetId)
                    .OnDelete(DeleteBehavior.Cascade); //what to do when Delete user profile...

            });


        }
    }
}

//dotnet ef migrations add IdentityAdded -p Persistence -s API  (migration  command =>create by this class and domain folder => "IdentityAdded" is the name when make the migration) 