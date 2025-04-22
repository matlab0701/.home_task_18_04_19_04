namespace Domain.DTOs.Couriers;

public class CourierEarningsDto
{
    public int CourierId { get; set; }
    public string CourierName { get; set; }
    public decimal TotalEarnings { get; set; }
}
