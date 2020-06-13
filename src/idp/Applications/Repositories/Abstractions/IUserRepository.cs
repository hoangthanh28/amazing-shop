using System;
using System.Threading.Tasks;
using IdentityServer.Domain.Entity;

namespace IdentityServer.Application.Repository.Abstraction
{
    public interface IUserRepository
    {
        Task<User> FindUserByUserNameAsync(string userName);
        Task<User> GetUserByIdAsync(Guid id);
    }
}