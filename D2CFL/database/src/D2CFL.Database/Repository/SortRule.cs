using System;
using System.Linq.Expressions;
using D2CFL.Database.Interfaces;

namespace D2CFL.Database.Repository
{
    public class SortRule<TEntity, TType>
        where TEntity : class, IEntity<TType>
    {
        public SortOrder SortOrder { get; set; }

        public Expression<Func<TEntity, object>> Expression { get; set; }
    }
}
