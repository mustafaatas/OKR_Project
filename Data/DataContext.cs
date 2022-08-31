using Core.Auth;
using Core.Models;
using Data.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DataContext:IdentityDbContext<User, Role, Guid>
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {
        }

        public DbSet<Music> Musics { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<KeyResult> KeyResults { get; set; }
        public DbSet<Objective> Objectives { get; set; }
        public DbSet<TeamUser> TeamUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder
               .Entity<Team>()
               .HasMany(s => s.TeamUsers)
               .WithOne(g => g.Team)
               .OnDelete(DeleteBehavior.Cascade);

            builder
                .Entity<TeamUser>()
                .HasOne(s => s.User);


            //builder
            //    .Entity<Department>()
            //    .HasOne<User>(d => d.Leader)
            //    .WithOne(u => u.Department)
            //    .HasForeignKey<Department>(d => d.LeaderId)
            //    .OnDelete(DeleteBehavior.SetNull);


            //builder
            //    .Entity<User>()
            //    .HasOne(u => u.Department)
            //    .WithOne(d => d.Leader)
            //    .HasForeignKey<Department>(d => d.LeaderId);


            //builder
            //    .Entity<User>()
            //    .HasOne<Department>(d => d.Department)
            //    .WithMany(u => u.Users)
            //    .HasForeignKey(d => d.DepartmentId)
            //    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
