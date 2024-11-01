using User.Api.Domain.Entities;
using User.Api.Domain.Repositories.Base;

namespace User.Api.Domain.Repositories
{
    public interface IUserRepository : IRepository<UserEntity>
    {
        Task<UserEntity> GetbyUserName(string userName);
        Task<UserEntity> GetUserByUserNameAndPassword(string userName, string password);
    }
}
