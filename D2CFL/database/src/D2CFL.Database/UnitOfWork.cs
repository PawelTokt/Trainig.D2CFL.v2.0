using System;
using System.Threading.Tasks;
using D2CFL.Database.Interfaces;
using D2CFL.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace D2CFL.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Context.Context _dbContext;
        private bool _isDisposed;

        private readonly Lazy<IRepository<PlayerEntity, Guid>> _playerRepository;

        public UnitOfWork(Context.Context dbContext, Func<DbContext, IRepository<PlayerEntity, Guid>> playerRepository)
        {
            this._dbContext = dbContext;
            this._playerRepository = new Lazy<IRepository<PlayerEntity, Guid>>(() => playerRepository(this._dbContext));
        }

        public IRepository<PlayerEntity, Guid> PlayerRepository => this._playerRepository.Value;

        public void Commit()
        {
            this._dbContext.SaveChanges();
        }

        public virtual Task CommitAsync()
        {
            return this._dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            if(this._isDisposed)
            {
                // no need to dispose twice.
                return;
            }

            // free managed resources 
            this._dbContext?.Dispose();

            this._isDisposed = true;
            GC.SuppressFinalize(this);
        }
    }
}
