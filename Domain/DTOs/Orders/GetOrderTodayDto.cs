namespace Domain.DTOs.Orders;

public class GetOrderTodayDto
{
    public DateTime CreatedAt { get; set; }
    
    public int SumOrder { get; set; }
}
