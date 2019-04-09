using System;
using System.Linq.Expressions;
using D2CFL.Database.Interfaces;

namespace D2CFL.Database.Repository
{
    public class FilterRule<TEntity, TType>
        where TEntity: class, IEntity<TType>
    {
        public Expression<Func<TEntity, bool>> Expression { get; set; }
    }
}
