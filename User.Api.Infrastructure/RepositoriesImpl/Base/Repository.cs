using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using User.Api.Domain.Repositories.Base;
using User.Api.Infrastructure.Data;

namespace User.Api.Infrastructure.RepositoriesImpl.Base
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly UserDbContext _userContext;

        public Repository(UserDbContext userContext)
        {
            _userContext = userContext;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _userContext.Set<T>().AddAsync(entity);
            await _userContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            var propertyInfo = typeof(T).GetProperty("Active");
            if (propertyInfo != null && propertyInfo.PropertyType == typeof(bool))
            {
                propertyInfo.SetValue(entity, false);
                _userContext.Set<T>().Update(entity);
                await _userContext.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("The entity does not have an 'Active' property for logical deletion.");
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var propertyInfo = typeof(T).GetProperty("Active");

            if (propertyInfo != null && propertyInfo.PropertyType == typeof(bool))
            {
                var parameter = Expression.Parameter(typeof(T), "e");

                var propertyAccess = Expression.Property(parameter, propertyInfo);

                var condition = Expression.Equal(propertyAccess, Expression.Constant(true));

                var lambda = Expression.Lambda<Func<T, bool>>(condition, parameter);

                return await _userContext.Set<T>()
                    .Where(lambda)
                    .ToListAsync();
            }

            return await _userContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _userContext.Set<T>().FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            _userContext.Set<T>().Update(entity);
            await _userContext.SaveChangesAsync();
        }
    }
}
