using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;

namespace Domain.Entities;

[Table("Users")]
public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
    public string Address { get; set; }
    public DateTime RegistrationDate { get; set; }
    public UserRole Role { get; set; }

    public virtual  List<Order> Orders { get; set; }
    public virtual  Courier Courier { get; set; } 
}
