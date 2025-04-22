namespace Domain.Filters;

public class OrderDetailFilters
{

    public int? Quantity { get; set; }
    public decimal? Price { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int? From { get; set; }
    public int? To { get; set; }

}
