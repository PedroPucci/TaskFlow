using TaskFlow.Application.Services;
using TaskFlow.Infrastracture.Repository.RepositoryUoW;

namespace TaskFlow.Application.UnitOfWork
{
    public class UnitOfWorkService : IUnitOfWorkService
    {
        private readonly IRepositoryUoW _repositoryUoW;
        private UserService userService;
        private TaskService taskService;
        private CategoryService categoryService;

        public UnitOfWorkService(IRepositoryUoW repositoryUoW)
        {
            _repositoryUoW = repositoryUoW;
        }

        public UserService UserService
        {
            get
            {
                if (userService is null)
                    userService = new UserService(_repositoryUoW);
                return userService;
            }
        }

        public TaskService TaskService
        {
            get
            {
                if (taskService is null)
                    taskService = new TaskService(_repositoryUoW);
                return taskService;
            }
        }

        public CategoryService CategoryService
        {
            get
            {
                if (categoryService is null)
                    categoryService = new CategoryService(_repositoryUoW);
                return categoryService;
            }
        }
    }
}
