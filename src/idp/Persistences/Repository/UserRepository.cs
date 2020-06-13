using System;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.Application.Repository.Abstraction;
using IdentityServer.Domain.Entity;
using IdentityServer.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Persistence.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _dbContext;
        public UserRepository(UserDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<User> FindUserByUserNameAsync(string userName)
        {
            return _dbContext.Users.Where(x => x.UserName == userName).AsNoTracking().FirstOrDefaultAsync();
        }

        public Task<User> GetUserByIdAsync(Guid id)
        {
            return _dbContext.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}