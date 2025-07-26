using System.ComponentModel.DataAnnotations;

namespace RalliesUK.Application.Requests.Auth
{
    public readonly record struct RegisterRequest
    {
        [EmailAddress]
        public required string Email { get; init; }

        public required string Password { get; init; }
    }
}