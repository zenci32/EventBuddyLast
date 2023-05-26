using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Context.EntityFramework
{
    public class NewsContextDb : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=217.131.28.73;Database=NewsDB;UID=sa;PWD=Eventbuddy123;Integrated Security=false;");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<Email> EmailParameters { get; set; }

        public DbSet<Event> Event { get; set; }
        public DbSet<Rate> Rate { get; set; }
        public DbSet<EventRequest> EventRequest { get; set; }

    }
}
