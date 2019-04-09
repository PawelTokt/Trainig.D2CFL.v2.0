using D2CFL.Database.Extensions;
using D2CFL.Database.Mappings;
using Microsoft.EntityFrameworkCore;

namespace D2CFL.Database.Context
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //SchemaName
            modelBuilder.AddConfiguration(new PlayerConfiguration());
        }
    }
}
