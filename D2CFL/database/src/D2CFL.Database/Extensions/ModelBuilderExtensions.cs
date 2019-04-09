using D2CFL.Database.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace D2CFL.Database.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void AddConfiguration<TEntity, TKey>(this ModelBuilder modelBuilder, IEntityTypeConfiguration<TEntity, TKey> entityTypeConfiguration)
            where TEntity : class, IEntity<TKey>
        {
            entityTypeConfiguration.Map(modelBuilder.Entity<TEntity>());
        }
    }
}
