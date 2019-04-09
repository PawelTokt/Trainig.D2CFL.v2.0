using System;
using System.Threading.Tasks;
using D2CFL.Database.Models;

namespace D2CFL.Database.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<PlayerEntity, Guid> PlayerRepository { get; }

        void Commit();

        Task CommitAsync();
    }
}
