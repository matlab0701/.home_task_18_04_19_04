namespace Domain.DTOs.Restauronts;

public class RestaurantRevenueDto
{
    public int RestaurantId { get; set; }
    public DateTime Date { get; set; }
    public decimal TotalRevenue { get; set; }
}
