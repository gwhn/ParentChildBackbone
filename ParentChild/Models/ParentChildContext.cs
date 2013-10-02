using System.Data.Entity;

namespace ParentChild.Models
{
    public class ParentChildContext : DbContext
    {
        public ParentChildContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Parent> Parents { get; set; }
        public DbSet<Child> Children { get; set; }
    }
}