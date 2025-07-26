namespace RalliesUK.Domain.Entities
{
    public interface IUserToken
    {
        public string? Email { get; }
        public string? UserName { get; }
    }
}
