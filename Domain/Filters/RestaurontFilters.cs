namespace Domain.Filters;

public class RestaurontFilters
{

    public string? Name { get; set; }
    public string? Address { get; set; }
     public decimal? Rating { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int? From { get; set; }
    public int? To { get; set; }

}
