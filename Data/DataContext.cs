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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Music>().HasKey(x => x.Id);
            builder
                .Entity<Music>()
                .HasOne<Artist>(a => a.Artist)
                .WithMany(m => m.Musics)
                .HasForeignKey(a => a.ArtistId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Team>().HasKey(x => x.Id);
            builder
                .Entity<Team>()
                .HasOne<Department>(d => d.Department)
                .WithMany(t => t.TeamList)
                .HasForeignKey(d => d.DepartmentId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<KeyResult>().HasKey(x => x.Id);
            builder
                .Entity<KeyResult>()
                .HasOne<Objective>(s => s.SurObjective)
                .WithMany(k => k.KeyResultList)
                .HasForeignKey(s => s.SurObjectiveId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.Entity<User>().HasKey(x => x.Id);
            builder
                .Entity<User>()
                .HasOne<Department>(d => d.Department)
                .WithMany(u => u.UserList)
                .HasForeignKey(d => d.DepartmentId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
