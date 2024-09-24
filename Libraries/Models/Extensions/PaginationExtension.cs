#nullable disable
using Microsoft.EntityFrameworkCore;
namespace Models.Exceptions;

public class PaginationList<T> where T : class
{
    public int Page { get; set; }
    public int PageSize { get; set; }

    public int TotalCount { get; set; }
    public int TotalPage => TotalCount / PageSize;
    public bool HasPrevious => Page > 0;
    public bool HasNext => Page < TotalPage;
}

public static class PaginationExtension
{
    public static async Task<PaginationList<T>> ToPagationList<T>(this IQueryable<T> items) where T : class
    {
        return new PaginationList<T> { TotalCount = await items.CountAsync() };
    }

    public static PaginationList<T> ToPagationList<T>(this IEnumerable<T> items) where T : class
    {
        return new PaginationList<T> { TotalCount = items.Count() };
    }

}
