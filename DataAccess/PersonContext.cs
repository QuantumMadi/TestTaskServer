using Microsoft.EntityFrameworkCore;
using TestTask.Models;

namespace TestTask.DataAccess
{
    public sealed class PersonContext : DbContext
    {
        public PersonContext(DbContextOptions options) : base(options)
        {
            Database.Migrate();
        }
        public DbSet<Person> People { get; set; }
    }
}