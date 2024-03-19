using Microsoft.EntityFrameworkCore;
using RegistrationIncomingLetters.DataAccess.Models;

namespace RegistrationIncomingLetters.DataAccess.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Letter> Letters { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder
                    .Entity<Letter>()
                    .HasOne(s => s.Sender)
                    .WithOne()
                    .OnDelete(DeleteBehavior.NoAction);
            modelBuilder
                    .Entity<Letter>()
                    .HasIndex(x => new { x.Id, x.SenderId, x.AddresseeId });
            modelBuilder
                    .Entity<Letter>()
                    .HasOne(s => s.Addressee)
                    .WithOne()
                    .OnDelete(DeleteBehavior.NoAction);
            CreateData(modelBuilder);
        }
        private void CreateData(ModelBuilder modelBuilder)
        {
            AddEmployees(modelBuilder);
        }

        private void AddEmployees(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasData(
                    new Employee { Id = 1, FirstName = "Иван", LastName = "Иванов", SecondryName = "Иванович", Age = 24, Email = "IvanovII@MyCompany.ru", Phone = "8129455315" },
                    new Employee { Id = 2, FirstName = "Петр", LastName = "Петров", SecondryName = "Петрович", Age = 63, Email = "PetrovPP@MyCompany.ru", Phone = "8129455316" }
                );
        }
    }
}
