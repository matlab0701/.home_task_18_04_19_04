using Domain.Enums;

namespace Domain.DTOs.Users;

public class CreateUserDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
    public string Address { get; set; }
    public UserRole Role { get; set; }
}
