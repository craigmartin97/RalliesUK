using RalliesUK.Domain.Entities;

namespace RalliesUK.Application.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateTokenAsync(IUserToken user, DateTime expiration);
    }
}