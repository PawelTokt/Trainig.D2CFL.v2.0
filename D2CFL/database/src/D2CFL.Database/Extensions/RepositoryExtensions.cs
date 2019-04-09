using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using D2CFL.Database.Interfaces;
using D2CFL.Database.Repository;

namespace D2CFL.Database.Extensions
{
    public static class RepositoryExtensions
    {
        public static TEntity Get<TEntity, TType>(this IRepository<TEntity, TType> repository, Expression<Func<TEntity, bool>> filterExpression)
            where TEntity : class, IEntity<TType>
        {
            return repository.Get(GetQueryParameters<TEntity, TType>(filterExpression));
        }

        private static QueryParameters<TEntity, TType> GetQueryParameters<TEntity, TType>(Expression<Func<TEntity, bool>> filterExpression)
            where TEntity : class, IEntity<TType>
        {
            return new QueryParameters<TEntity, TType>
            {
                Filter = new FilterRule<TEntity, TType>
                {
                    Expression = filterExpression
                }
            };
        }

        public static TEntity Get<TEntity, TType>(this IRepository<TEntity, TType> repository, TType id)
            where TEntity : class, IEntity<TType>
        {
            return repository.Get(GetIdFilterExpression<TEntity, TType>(id));
        }

        public static TModel Get<TModel, TEntity, TType>(this IRepository<TEntity, TType> repository, IMapper mapper, Expression<Func<TEntity, bool>> filterExpression)
            where TEntity : class, IEntity<TType>
        {
            return repository.Get<TModel>(mapper, GetQueryParameters<TEntity, TType>(filterExpression));
        }

        public static TModel Get<TModel, TEntity, TType>(this IRepository<TEntity, TType> repository, IMapper mapper, TType id)
            where TEntity : class, IEntity<TType>
        {
            return repository.Get<TModel, TEntity, TType>(mapper, GetIdFilterExpression<TEntity, TType>(id));
        }

        public static async Task<TEntity> GetAsync<TEntity, TType>(this IRepository<TEntity, TType> repository, Expression<Func<TEntity, bool>> filterExpression)
            where TEntity : class, IEntity<TType>
        {
            return await repository.GetAsync(GetQueryParameters<TEntity, TType>(filterExpression));
        }

        public static async Task<TEntity> GetAsync<TEntity, TType>(this IRepository<TEntity, TType> repository, TType id)
            where TEntity : class, IEntity<TType>
        {
            return await repository.GetAsync(GetIdFilterExpression<TEntity, TType>(id));
        }

        public static async Task<TModel> GetAsync<TModel, TEntity, TType>(this IRepository<TEntity, TType> repository, IMapper mapper, Expression<Func<TEntity, bool>> filterExpression)
            where TEntity : class, IEntity<TType>
        {
            return await repository.GetAsync<TModel>(mapper, GetQueryParameters<TEntity, TType>(filterExpression));
        }

        public static async Task<TModel> GetAsync<TModel, TEntity, TType>(this IRepository<TEntity, TType> repository, IMapper mapper, TType id)
            where TEntity : class, IEntity<TType>
        {
            return await repository.GetAsync<TModel, TEntity, TType>(mapper, GetIdFilterExpression<TEntity, TType>(id));
        }

        private static Expression<Func<TEntity, bool>> GetIdFilterExpression<TEntity, TType>(TType id)
            where TEntity : class, IEntity<TType>
        {
            Expression<Func<TEntity, TType>> property = x => x.Id;

            var leftExpression = property.Body;
            var rightExpression = Expression.Constant(id, typeof(TType));

            return Expression.Lambda<Func<TEntity, bool>>(Expression.Equal(leftExpression, rightExpression), property.Parameters.Single());
        }

        public static IList<TEntity> GetList<TEntity, TType>(this IRepository<TEntity, TType> repository, Expression<Func<TEntity, bool>> filterExpression)
            where TEntity : class, IEntity<TType>
        {
            return repository.GetList(GetQueryParameters<TEntity, TType>(filterExpression));
        }

        public static IList<TModel> GetList<TModel, TEntity, TType>(this IRepository<TEntity, TType> repository, IMapper mapper, Expression<Func<TEntity, bool>> filterExpression)
            where TEntity : class, IEntity<TType>
        {
            return repository.GetList<TModel>(mapper, GetQueryParameters<TEntity, TType>(filterExpression));
        }

        public static async Task<IList<TEntity>> GetListAsync<TEntity, TType>(this IRepository<TEntity, TType> repository, Expression<Func<TEntity, bool>> filterExpression)
            where TEntity : class, IEntity<TType>
        {
            return await repository.GetListAsync(GetQueryParameters<TEntity, TType>(filterExpression));
        }

        public static async Task<IList<TModel>> GetListAsync<TModel, TEntity, TType>(this IRepository<TEntity, TType> repository, IMapper mapper, Expression<Func<TEntity, bool>> filterExpression)
            where TEntity : class, IEntity<TType>
        {
            return await repository.GetListAsync<TModel>(mapper, GetQueryParameters<TEntity, TType>(filterExpression));
        }

        public static bool Exists<TEntity, TType>(this IRepository<TEntity, TType> repository, Expression<Func<TEntity, bool>> filterExpression)
            where TEntity : class, IEntity<TType>
        {
            return repository.Exists(GetQueryParameters<TEntity, TType>(filterExpression));
        }

        public static bool Exists<TEntity, TType>(this IRepository<TEntity, TType> repository, TType id)
            where TEntity : class, IEntity<TType>
        {
            return repository.Exists(GetIdFilterExpression<TEntity, TType>(id));
        }

        public static async Task<bool> ExistsAsync<TEntity, TType>(this IRepository<TEntity, TType> repository, Expression<Func<TEntity, bool>> filterExpression)
            where TEntity : class, IEntity<TType>
        {
            return await repository.ExistsAsync(GetQueryParameters<TEntity, TType>(filterExpression));
        }

        public static async Task<bool> ExistsAsync<TEntity, TType>(this IRepository<TEntity, TType> repository, TType id)
            where TEntity : class, IEntity<TType>
        {
            return await repository.ExistsAsync(GetIdFilterExpression<TEntity, TType>(id));
        }

        public static void Delete<TEntity, TType>(this IRepository<TEntity, TType> repository, TType id)
            where TEntity : class, IEntity<TType>
        {
            repository.Delete(repository.Get(id));
        }
    }
}
