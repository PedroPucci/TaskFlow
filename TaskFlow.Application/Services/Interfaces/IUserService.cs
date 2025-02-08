using TaskFlow.Domain.Entity;
using TestWebBackEndDeveloper.Application.ExtensionError;

namespace TaskFlow.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<Result<UserEntity>> AddUserAsync(UserEntity userEntity);
        Task<Result<UserEntity>> UpdateUserAsync(UserEntity userEntity);
        Task DeleteUserAsync(int userId);
        Task<List<UserEntity>> GetAllUsersAsync();
    }
}
