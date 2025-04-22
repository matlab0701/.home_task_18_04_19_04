using Domain.Enums;

namespace Domain.Filters;

public class CourierFilters
{
    public CourierStatus Status { get; set; }
    public decimal Rating { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int? From { get; set; }
    public int? To { get; set; }
}
