using TaskFlow.Application.Services;

namespace TaskFlow.Application.UnitOfWork
{
    public interface IUnitOfWorkService
    {
        UserService UserService { get; }
        //TaskService TaskService { get; }
        //CategoryService CategoryService { get; }
    }
}