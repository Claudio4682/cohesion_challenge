using Microsoft.EntityFrameworkCore;
using ServiceRequest.Domain.Common;
using ServiceRequest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceRequest.Persistance
{
    public class ServiceRequestDbContext : DbContext
    {
        public ServiceRequestDbContext(DbContextOptions<ServiceRequestDbContext> options) : base(options)
        {
        }

        public DbSet<Request> Requests { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Request>().HasKey(r => r.Id);
            modelBuilder.Entity<Request>().Property(r => r.Description).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Request>().Property(r => r.BuildingCode).IsRequired().HasMaxLength(15);

            modelBuilder.Entity<Building>().HasKey(b => b.BuildingCode);


            var user1 = Guid.Parse("{B0788D2F-8003-43C1-92A4-EDC76A7C5DDE}");
            var user2 = Guid.Parse("{6313179F-7837-473A-A4D5-A5571B43E6A6}");
            
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = user1,
                Name = "Claudio Jaramillo",
                Email = "cjaramillotw@gmail.com"
            });

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = user2,
                Name = "Juan Sanchez",
                Email = "cjaramillotw@gmail.com"
            });

            modelBuilder.Entity<Building>().HasData(new Building
            {
                BuildingCode = "TW20",
                Name = "Prisma"
            });

            modelBuilder.Entity<Building>().HasData(new Building
            {
                BuildingCode = "CR21",
                Name = "Terra Nova"
            });



        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
