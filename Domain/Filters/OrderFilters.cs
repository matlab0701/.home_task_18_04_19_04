using Domain.Enums;

namespace Domain.Filters;

public class OrderFilters
{
    public OrderStatus OrderStatus { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
