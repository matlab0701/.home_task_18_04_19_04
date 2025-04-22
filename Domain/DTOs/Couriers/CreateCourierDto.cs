using Domain.Enums;
namespace Domain.DTOs.Couriers;
public class CreateCourierDto
{
    public int UserId { get; set; }
    public CourierStatus Status { get; set; }
    public string CurrentLocation { get; set; }
    public TransportType TransportType { get; set; }
}
