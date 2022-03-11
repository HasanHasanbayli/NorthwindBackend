using Core.Entities;

namespace Entities.DTOs.User;

public class AuthenticationRequest : IDto
{
    public string Email { get; set; }

    public string Password { get; set; }
}