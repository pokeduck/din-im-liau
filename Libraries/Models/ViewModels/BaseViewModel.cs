namespace Models.ViewModels;


public interface IBasePagination
{
    public string? Keyword { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
}

public interface IBaseSoftDelete
{
    public bool? IsDeleted { get; set; }
}


public class BaseViewModel : IBasePagination, IBaseSoftDelete
{
    public string? Keyword { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 50;
    public bool? IsDeleted { get; set; }
}
