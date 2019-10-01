using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebCoreAppFramework.Models;
using WebCoreAppFramework.ViewModels;

namespace WebCoreAppFramework.Data
{
    public class ApplicationDbContext
    : IdentityDbContext<ApplicationUser, ApplicationRole, string,
        ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin,
        ApplicationRoleClaim, ApplicationUserToken>
    {


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>(b =>
            {
                // Each User can have many UserClaims
                b.HasMany(e => e.Claims)
                    .WithOne(e => e.User)
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();

                // Each User can have many UserLogins
                b.HasMany(e => e.Logins)
                    .WithOne(e => e.User)
                    .HasForeignKey(ul => ul.UserId)
                    .IsRequired();

                // Each User can have many UserTokens
                b.HasMany(e => e.Tokens)
                    .WithOne(e => e.User)
                    .HasForeignKey(ut => ut.UserId)
                    .IsRequired();

                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();
            });


            modelBuilder.Entity<ApplicationRole>(b =>
            {
                // Each Role can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();

                // Each Role can have many associated RoleClaims
                b.HasMany(e => e.RoleClaims)
                    .WithOne(e => e.Role)
                    .HasForeignKey(rc => rc.RoleId)
                    .IsRequired();
            });
            modelBuilder.Entity<ApplicationUserRole>().HasKey(ur => new { ur.UserId, ur.TenantId });
            modelBuilder.Entity<ApplicationUserRole>()
            .HasOne<ApplicationUser>(u => u.User)
            .WithMany(s => s.UserRoles)
            .HasForeignKey(u => u.UserId);

            modelBuilder.Entity<ApplicationUserRole>()
            .HasOne<ApplicationTenant>(u => u.Tenant)
            .WithMany(s => s.Users)
            .HasForeignKey(u => u.TenantId);

            modelBuilder.Entity<ApplicationUserRole>()
                        .HasOne<ApplicationRole>(u => u.Role)
                        .WithMany(s => s.UserRoles)
                        .HasForeignKey(u => u.RoleId);

            modelBuilder.Entity<CountryLanguage>().HasKey(cl => new { cl.CountryId, cl.LanguageId });


        }

        public DbSet<About> Abouts { get; set; }
        public DbSet<ApplicationTenant> Tenants { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<PostalCode> PostalCodes { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<GeoLocation> GeoLocations { get; set; }
        public DbSet<District> Districs { get; set; }
        public DbSet<County> Counties { get; set; }
        public DbSet<Language> Languages { get; set; }
        //public DbSet<WebCoreAppFramework.ViewModels.UserIndexViewModel> UserIndexViewModel { get; set; }
        //public DbSet<WebCoreAppFramework.ViewModels.UserDetailsViewModel> UserDetailsViewModel { get; set; }

    }
}
