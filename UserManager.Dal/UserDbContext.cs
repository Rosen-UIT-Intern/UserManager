using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace UserManager.Dal
{
    public class UserDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }

        protected UserDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        /// <summary>
        /// seed data
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>()
                .HasKey(usrl => new { usrl.UserId, usrl.RoleId });
            modelBuilder.Entity<UserRole>()
                .HasOne(usrl => usrl.User)
                .WithMany(us => us.UserRoles)
                .HasForeignKey(usrl => usrl.UserId);
            modelBuilder.Entity<UserRole>()
                .HasOne(usrl => usrl.Role)
                .WithMany(rl => rl.UserRoles)
                .HasForeignKey(usrl => usrl.RoleId);

            modelBuilder.Entity<UserGroup>()
                .HasKey(usgr => new { usgr.UserId, usgr.GroupId });
            modelBuilder.Entity<UserGroup>()
                .HasOne(usgr => usgr.User)
                .WithMany(us => us.UserGroups)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(usgr => usgr.UserId);
            modelBuilder.Entity<UserGroup>()
                .HasOne(usgr => usgr.Group)
                .WithMany(gr => gr.UserGroups)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(usgr => usgr.GroupId);

            var seedData = SeedData.Instance;
            Organization[] organizations = new Organization[]
            {
                seedData.RosenOrg,seedData.UITOrg
            };
            Group[] groups = new Group[]
            {
                seedData.RosenTechGroup,seedData.RosenHRGroup,seedData.UITSEGroup,seedData.UITCEGroup
            };

            Role[] roles = new Role[]
            {
                seedData.TechLeadRole,seedData.HRLeadRole,seedData.EngineerRole
            };

            modelBuilder.Entity<Organization>().HasData(organizations);
            modelBuilder.Entity<Group>().HasData(groups);
            modelBuilder.Entity<Role>().HasData(roles);
            modelBuilder.Entity<User>().HasData(seedData.User);
            modelBuilder.Entity<UserGroup>().HasData(seedData.UserGroups);
            modelBuilder.Entity<UserRole>().HasData(seedData.UserRoles);

            base.OnModelCreating(modelBuilder);
        }
    }
}
