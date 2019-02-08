using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Irdata.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string DisplayName { get; set; }

        public int AccountId { get; set; }

        public virtual Account Account { get; set; }

        public ApplicationUser() : base()
        {
            PasswordHistory = new List<PasswordHistory>();
            Files = new List<File>();
            FundMes = new List<FundMe>();
        }

        public virtual List<PasswordHistory> PasswordHistory { get; set; }

        public virtual ICollection<File> Files { get; set; }
        public virtual ICollection<FundMe> FundMes { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        //INSERT INTO [dbo].[AspNetUsers] ([Id], [DisplayName], [AccountId], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'7ff5430e-8e5b-4250-b72f-8c2e9046a2f6', N'Admin', 1, N'Admin@Admin.com', 0, N'ALZaM71xzxZ+lWIO8iZUX9MlchU7SvZOcUfjGNapdrnUE5sheWXS8k5+zKTgoednjg==', N'795a3c69-a9c1-422c-a336-6b690aec3e71', N'0912345678', 0, 0, NULL, 1, 0, N'Admin@Admin.com')
        //INSERT INTO[dbo].[AspNetUsers]
        //([Id], [DisplayName], [AccountId], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES(N'fa002a87-b880-49ab-b55f-d3cad6380552', N'Local', 4, N'Local@Local.com', 0, N'APGWTo5kt+aJiGiN7OtfRb553B38oyQwZyoZS6ofBofK8txpwI8X/Wn4U0ryGj/wEA==', N'a3763f6b-d293-4505-844d-c1571ba971d4', N'0987654321', 0, 0, NULL, 1, 0, N'Local@Local.com')

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<File> Files { get; set; }

        public DbSet<VolunteeringEvents> VolunteeringEvents { get; set; }
        public DbSet<VolunteerFile> VolunteeringFiles { get; set; }

        public DbSet<FundMe> FundMes { get; set; }
        public DbSet<FundMeFile> FundMeFiles { get; set; }

        public DbSet<Funder> funders { get; set; }
        public DbSet<VolunteerDetails> volunteerDetails { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<File>()
                .HasRequired(b => b.ApplicationUser)
                .WithMany(b => b.Files)
                .WillCascadeOnDelete(true);
            modelBuilder.Entity<VolunteerFile>()
                .HasRequired(b => b.VolunteeringEvents)
                .WithMany(b => b.VolunteerFiles)
                .WillCascadeOnDelete(true);
            modelBuilder.Entity<FundMeFile>()
                .HasRequired(b => b.FundMe)
                .WithMany(b => b.FundMeFiles)
                .WillCascadeOnDelete(true);
        }

        //public System.Data.Entity.DbSet<Irdata.Models.ApplicationUser> ApplicationUsers { get; set; }
    }

    public class PasswordHistory
    {
        public PasswordHistory()
        {
            CreatedDate = DateTime.Now;
        }

        public DateTime CreatedDate { get; set; }

        [Key, Column(Order = 1)]
        public string PasswordHash { get; set; }

        [Key, Column(Order = 0)]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

    }

}