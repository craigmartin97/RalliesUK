namespace RalliesUK.Application.Interfaces
{
    public interface IUserRoleService
    {
        Task<IList<string>> GetRolesAsync(string userId);

        Task<bool> IsUserInRoleAsync(string userId, string roleName);
    }
}