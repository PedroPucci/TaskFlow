using Microsoft.EntityFrameworkCore.Storage;
using TaskFlow.Infrastracture.Repository.Interfaces;

namespace TaskFlow.Infrastracture.Repository.RepositoryUoW
{
    public interface IRepositoryUoW
    {
        IUserRepository UserRepository { get; }
        ITaskRepository TaskRepository { get; }

        Task SaveAsync();
        void Commit();
        IDbContextTransaction BeginTransaction();
    }
}