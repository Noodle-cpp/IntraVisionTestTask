namespace Api.ViewModels.Responses;

public class PaginationResponse<T>
{
    public IEnumerable<T> List { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
}