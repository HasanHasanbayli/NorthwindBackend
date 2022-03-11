using Core.Entities;

namespace Entities.DTOs.User;

public class RegisterRequest : IDto
{
    public string Email { get; set; }

    public string Password { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }
}