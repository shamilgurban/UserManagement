using Consumer.API.DataAccess.Organisations;
using Consumer.API.DataAccess.Users;
using Microsoft.EntityFrameworkCore;

namespace Consumer.API.DataAccess
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Organisation> Organisations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Organisation>().HasData(
                            new Organisation
                            {
                                Id = 1,
                                Name = "Sample Organisation"
                            }
                         );

            modelBuilder.Entity<User>().HasData(
                            new User
                            {
                                Id = 1,
                                Name = "Jack",
                                Surname = "Williams",
                                FatherName = "John",
                                Email = "jack.williams@email.com",
                                PhoneNumber = "123456789",
                                OrganisationId = 1
                            }
                        );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 2,
                    Name = "Tommy",
                    Surname = "Vercetti",
                    Email = "tommy.vercetti@vicecity.com",
                    PhoneNumber = "123456789",
                    OrganisationId = 1
                }
            );
        }
    }
}
