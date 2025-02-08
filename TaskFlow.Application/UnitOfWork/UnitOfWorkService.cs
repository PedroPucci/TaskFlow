using TaskFlow.Application.Services;
using TaskFlow.Infrastracture.Repository.RepositoryUoW;

namespace TaskFlow.Application.UnitOfWork
{
    public class UnitOfWorkService : IUnitOfWorkService
    {
        private readonly IRepositoryUoW _repositoryUoW;
        private UserService userService;

        public UnitOfWorkService(IRepositoryUoW repositoryUoW)
        {
            _repositoryUoW = repositoryUoW;
        }

        public UserService UserService
        {
            get
            {
                if (userService is null)
                {
                    userService = new UserService(_repositoryUoW);
                }
                return userService;
            }
        }
    }
}
