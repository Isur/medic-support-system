using System;
using medic_api.DAL.Models;
using medic_api.Helpers;
using Microsoft.EntityFrameworkCore;

namespace medic_api.DAL
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options) { }
        
        public DbSet<User> Users { get; set; }
        public DbSet<MedicalData> MedicalDataset { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var rsa = new RSA();
            modelBuilder.Entity<User>().HasData(new User
            {
                Role = "Admin",
                Password = PasswordHasher.Hash("Admin"),
                FirstName = rsa.Encrypt("Admin"),
                LastName = rsa.Encrypt("Admin"),
                UserName = "Admin",
                UserId = Guid.NewGuid(),
            });
        }
    }
}