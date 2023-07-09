namespace ISysLab2023.Backend.Lib.Core.Service;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class PagedList<T> : List<T>
{
    /// <summary>
    /// 
    /// </summary>
    public int PageIndex { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int TotalPages { get; set; }


    // TODO add configs and secrets
    /// <summary>
    /// 
    /// </summary>
    public static int PageSize { get; } = 5;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="items"></param>
    /// <param name="count"></param>
    /// <param name="pageIndex"></param>
    public PagedList(List<T> items, int count, int pageIndex)
    {
        PageIndex = pageIndex;
        TotalPages = (int)Math.Ceiling(count / (double)PageSize);
        this.AddRange(items);
    }

    public bool HasPreviousPage => PageIndex > 1;
    public bool HasNextPage => PageIndex < TotalPages;


    public static PagedList<T> Create(List<T> source, int pageIndex)
    {
        var count = source.Count;
        var items = source.Skip((pageIndex - 1) * PageSize).Take(PageSize).ToList();
        return new PagedList<T>(items, count, pageIndex);
    }

}
