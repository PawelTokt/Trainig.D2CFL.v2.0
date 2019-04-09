using D2CFL.Database.Interfaces;

namespace D2CFL.Database.Repository
{
    public class QueryParameters<TEntity, TType>
        where TEntity : class, IEntity<TType>
    {
        public FilterRule<TEntity, TType> Filter { get; set; }

        public SortRule<TEntity, TType> Sort { get; set; }

        public PageRule Page { get; set; }
    }
}
