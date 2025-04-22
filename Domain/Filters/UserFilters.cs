namespace Domain.Filters;

public class UserFilters
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string Password { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

}
