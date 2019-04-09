using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace D2CFL.Database.Interfaces
{
    public interface IEntityTypeConfiguration<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        void Map(EntityTypeBuilder<TEntity> entityTypeBuilder);
    }
}
