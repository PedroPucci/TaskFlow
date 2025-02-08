using TaskFlow.Domain.Entity;

namespace TaskFlow.Infrastracture.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<UserEntity> AddUserAsync(UserEntity userEntity);
        UserEntity UpdateUserAsync(UserEntity userEntity);
        UserEntity DeleteUserAsync(UserEntity userEntity);
        Task<List<UserEntity>> GetAllUsersAsync();
    }
}