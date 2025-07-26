using RalliesUK.Domain.Entities;

namespace RalliesUK.Application.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(IUserToken user, DateTime expiration);
    }
}