using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using D2CFL.Database.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace D2CFL.Database.Repository
{
    public class Repository<TEntity, TType> : IRepository<TEntity, TType>
        where TEntity : class, IEntity<TType>
    {
        public Repository(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        protected DbContext DbContext { get; }

        protected DbSet<TEntity> DbSet => DbContext.Set<TEntity>();

        public virtual TEntity Get(QueryParameters<TEntity, TType> queryParameters = null)
        {
            return Query(queryParameters).FirstOrDefault();
        }

        public virtual TModel Get<TModel>(IMapper mapper, QueryParameters<TEntity, TType> queryParameters = null)
        {
            return Query<TModel>(mapper, queryParameters).FirstOrDefault();
        }

        public virtual async Task<TEntity> GetAsync(QueryParameters<TEntity, TType> queryParameters = null)
        {
            return await Query(queryParameters).FirstOrDefaultAsync();
        }

        public virtual async Task<TModel> GetAsync<TModel>(IMapper mapper, QueryParameters<TEntity, TType> queryParameters = null)
        {
            return await Query<TModel>(mapper, queryParameters).FirstOrDefaultAsync();
        }

        public virtual IList<TEntity> GetList(QueryParameters<TEntity, TType> queryParameters = null)
        {
            return Query(queryParameters).ToList();
        }

        public virtual IList<TModel> GetList<TModel>(IMapper mapper, QueryParameters<TEntity, TType> queryParameters = null)
        {
            return Query<TModel>(mapper, queryParameters).ToList();
        }

        public virtual async Task<IList<TEntity>> GetListAsync(QueryParameters<TEntity, TType> queryParameters = null)
        {
            return await Query(queryParameters).ToListAsync();
        }

        public virtual async Task<IList<TModel>> GetListAsync<TModel>(IMapper mapper, QueryParameters<TEntity, TType> queryParameters = null)
        {
            return await Query<TModel>(mapper, queryParameters).ToListAsync();
        }

        public virtual PageResult<TEntity> GetPagedList(QueryParameters<TEntity, TType> queryParameters)
        {
            var items = PageResultQuery(queryParameters).ToList();

            var totalCount = Count(queryParameters);

            return new PageResult<TEntity>
            {
                PageIndex = queryParameters.Page.Index,
                PageSize = queryParameters.Page.Size,
                Items = items,
                TotalCount = totalCount
            };
        }

        public virtual PageResult<TModel> GetPagedList<TModel>(IMapper mapper, QueryParameters<TEntity, TType> queryParameters)
        {
            var items = PageResultQuery<TModel>(mapper, queryParameters).ToList();

            var totalCount = Count(queryParameters);

            return new PageResult<TModel>
            {
                PageIndex = queryParameters.Page.Index,
                PageSize = queryParameters.Page.Size,
                Items = items,
                TotalCount = totalCount
            };
        }

        public virtual async Task<PageResult<TEntity>> GetPagedListAsync(QueryParameters<TEntity, TType> queryParameters)
        {
            var items =  await PageResultQuery(queryParameters).ToListAsync();

            var totalCount = await CountAsync(queryParameters);

            return new PageResult<TEntity>
            {
                PageIndex = queryParameters.Page.Index,
                PageSize = queryParameters.Page.Size,
                Items = items,
                TotalCount = totalCount
            };
        }

        public virtual async Task<PageResult<TModel>> GetPagedListAsync<TModel>(IMapper mapper, QueryParameters<TEntity, TType> queryParameters)
        {
            var items = await PageResultQuery<TModel>(mapper, queryParameters).ToListAsync();

            var totalCount = await CountAsync(queryParameters);

            return new PageResult<TModel>
            {
                PageIndex = queryParameters.Page.Index,
                PageSize = queryParameters.Page.Size,
                Items = items,
                TotalCount = totalCount
            };
        }

        public virtual bool Exists(QueryParameters<TEntity, TType> queryParameters = null)
        {
            return Query(queryParameters).Any();
        }

        public virtual async Task<bool> ExistsAsync(QueryParameters<TEntity, TType> queryParameters = null)
        {
            return await Query(queryParameters).AnyAsync();
        }

        public virtual int Count(QueryParameters<TEntity, TType> queryParameters = null)
        {
            return CountQuery(queryParameters).Count();
        }

        public virtual async Task<int> CountAsync(QueryParameters<TEntity, TType> queryParameters = null)
        {
            return await CountQuery(queryParameters).CountAsync();
        }

        public virtual TEntity Insert(TEntity entity)
        {
            return DbSet.Add(entity).Entity;
        }

        public virtual async Task<TEntity> InsertAsync(TEntity entity)
        {
            return (await DbSet.AddAsync(entity)).Entity;
        }

        public virtual TEntity Update(TEntity entity, bool startTrackProperties = false)
        {
            DbSet.Attach(entity);

            if (!startTrackProperties)
            {
                MarkAsModified(entity);
            }

            return entity;
        }

        public virtual void Delete(TEntity entity)
        {
            if (IsDetached(entity))
            {
                DbSet.Attach(entity);
            }

            MarkAsDeleted(entity);

            DbSet.Remove(entity);
        }

        protected virtual IQueryable<TEntity> Query(QueryParameters<TEntity, TType> queryParameters = null)
        {
            IQueryable<TEntity> query = DbSet;

            if(queryParameters == null) return query;

            if(queryParameters.Filter?.Expression != null)
            {
                query = query.Where(queryParameters.Filter.Expression);
            }

            if (queryParameters.Sort?.Expression != null)
            {
                query = queryParameters.Sort.SortOrder == SortOrder.Descending
                    ? query.OrderByDescending(queryParameters.Sort.Expression)
                    : query.OrderBy(queryParameters.Sort.Expression);
            }

            if (queryParameters.Page != null && queryParameters.Page.IsValid)
            {
                query = query
                    .Skip(queryParameters.Page.Index)
                    .Take(queryParameters.Page.Size);
            }

            return query;
        }

        protected virtual IQueryable<TModel> Query<TModel>(IMapper mapper, QueryParameters<TEntity, TType> queryParameters = null)
        {
            return Query(queryParameters).ProjectTo<TModel>(mapper.ConfigurationProvider);
        }

        protected virtual IQueryable<TEntity> PageResultQuery(QueryParameters<TEntity, TType> queryParameters)
        {
            if (queryParameters == null) throw new ArgumentNullException(nameof(queryParameters), "Query Parameters can't be null.");

            if (queryParameters.Page == null) throw new ArgumentNullException(nameof(queryParameters.Page), "Query Parameters Page can't be null.");

            if (!queryParameters.Page.IsValid) throw new ArgumentException("Query Parameters Page is not valid.", nameof(queryParameters.Page));

            return Query(queryParameters);
        }

        protected IQueryable<TModel> PageResultQuery<TModel>(IMapper mapper, QueryParameters<TEntity, TType> queryParameters)
        {
            return PageResultQuery(queryParameters).ProjectTo<TModel>(mapper.ConfigurationProvider);
        }

        protected virtual IQueryable<TEntity> CountQuery(QueryParameters<TEntity, TType> queryParameters = null)
        {
            IQueryable<TEntity> query = DbSet;

            if (queryParameters == null) return query;

            if (queryParameters.Filter?.Expression != null)
            {
                query = query.Where(queryParameters.Filter.Expression);
            }

            return query;
        }

        protected void MarkAsModified(TEntity entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
        }

        protected bool IsDetached(TEntity entity)
        {
            return DbContext.Entry(entity).State == EntityState.Detached;
        }

        protected void MarkAsDeleted(TEntity entity)
        {
            DbContext.Entry(entity).State = EntityState.Deleted;
        }
    }
}
