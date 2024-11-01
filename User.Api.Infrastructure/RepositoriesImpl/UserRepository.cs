using Microsoft.EntityFrameworkCore;
using User.Api.Domain.Entities;
using User.Api.Domain.Repositories;
using User.Api.Infrastructure.Data;
using User.Api.Infrastructure.RepositoriesImpl.Base;
using User.Api.Shared.Uitls;

namespace User.Api.Infrastructure.RepositoriesImpl
{
    public class UserRepository : Repository<UserEntity>, IUserRepository
    {
        public UserRepository(UserDbContext userContext) : base(userContext)
        {
        }

        public async Task<UserEntity> GetbyUserName(string userName)
        {
            return await _userContext.Set<UserEntity>().Where(x => x.UserName.ToLower() == userName.ToLower().Trim()).FirstOrDefaultAsync();        
        }

        public async Task<UserEntity> GetUserByUserNameAndPassword(string userName, string password)
        {
            var ePass = Security.Encrypt(password.Trim());
            return await _userContext.Set<UserEntity>().Where(x => x.UserName.ToLower() == userName.ToLower().Trim() && x.Password == ePass).FirstOrDefaultAsync();
        }
    }
}
