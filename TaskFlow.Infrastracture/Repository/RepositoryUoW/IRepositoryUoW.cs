using Microsoft.EntityFrameworkCore.Storage;
using TaskFlow.Infrastracture.Repository.Interfaces;

namespace TaskFlow.Infrastracture.Repository.RepositoryUoW
{
    public interface IRepositoryUoW
    {
        IUserRepository UserEntityRepository { get; }

        Task SaveAsync();
        void Commit();
        IDbContextTransaction BeginTransaction();
    }
}