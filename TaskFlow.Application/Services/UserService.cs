using TaskFlow.Application.Services.Interfaces;
using TaskFlow.Domain.Entity;
using TaskFlow.Infrastracture.Repository.RepositoryUoW;
using TestWebBackEndDeveloper.Application.ExtensionError;

namespace TaskFlow.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryUoW _repositoryUoW;

        public UserService(IRepositoryUoW repositoryUoW)
        {
            _repositoryUoW = repositoryUoW;
        }

        public Task<Result<UserEntity>> AddUserAsync(UserEntity userEntity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUserAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserEntity>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TestWebBackEndDeveloper.Application.ExtensionError.Result<UserEntity>> UpdateUserAsync(UserEntity userEntity)
        {
            throw new NotImplementedException();
        }
    }
}
