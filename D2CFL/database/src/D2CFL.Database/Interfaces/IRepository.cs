using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using D2CFL.Database.Repository;

namespace D2CFL.Database.Interfaces
{
    public interface IRepository<TEntity, TType>
        where TEntity : class, IEntity<TType>
    {
        TEntity Get(QueryParameters<TEntity, TType> queryParameters = null);

        TModel Get<TModel>(IMapper mapper, QueryParameters<TEntity, TType> queryParameters = null);

        Task<TEntity> GetAsync(QueryParameters<TEntity, TType> queryParameters = null);

        Task<TModel> GetAsync<TModel>(IMapper mapper, QueryParameters<TEntity, TType> queryParameters = null);

        IList<TEntity> GetList(QueryParameters<TEntity, TType> queryParameters = null);

        IList<TModel> GetList<TModel>(IMapper mapper, QueryParameters<TEntity, TType> queryParameters = null);

        Task<IList<TEntity>> GetListAsync(QueryParameters<TEntity, TType> queryParameters = null);

        Task<IList<TModel>> GetListAsync<TModel>(IMapper mapper, QueryParameters<TEntity, TType> queryParameters = null);

        PageResult<TEntity> GetPagedList(QueryParameters<TEntity, TType> queryParameters);

        PageResult<TModel> GetPagedList<TModel>(IMapper mapper, QueryParameters<TEntity, TType> queryParameters);

        Task<PageResult<TEntity>> GetPagedListAsync(QueryParameters<TEntity, TType> queryParameters);

        Task<PageResult<TModel>> GetPagedListAsync<TModel>(IMapper mapper, QueryParameters<TEntity, TType> queryParameters);

        bool Exists(QueryParameters<TEntity, TType> queryParameters = null);

        Task<bool> ExistsAsync(QueryParameters<TEntity, TType> queryParameters = null);

        int Count(QueryParameters<TEntity, TType> queryParameters = null);

        Task<int> CountAsync(QueryParameters<TEntity, TType> queryParameters = null);

        TEntity Insert(TEntity entity);

        Task<TEntity> InsertAsync(TEntity entity);

        TEntity Update(TEntity entity, bool startTrackProperties = false);

        void Delete(TEntity entity);
    }
}
