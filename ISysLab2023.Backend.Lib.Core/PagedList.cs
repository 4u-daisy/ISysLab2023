namespace ISysLab2023.Backend.Lib.Core.Service;

/// <summary>
/// Performs pagination for List<T>
/// </summary>
/// <typeparam name="T">Type of collection</typeparam>
public sealed class PagedList<T> : List<T>
{
    /// <summary>
    /// page number
    /// </summary>
    public int PageNumber { get; set; }

    /// <summary>
    /// number of possible pages in the collection
    /// </summary>
    public int TotalPages { get; set; }


    // TODO add configs and secrets
    /// <summary>
    /// Number of items on the page
    /// </summary>
    public static int PageSize { get; } = 5;

    /// <summary>
    /// Generate PagedList
    /// </summary>
    /// <param name="items">collection items</param>
    /// <param name="count">count elems in collection</param>
    /// <param name="pageNumber">page number</param>
    public PagedList(List<T> items, int count, int pageNumber)
    {
        PageNumber = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)PageSize);
        this.AddRange(items);
    }

    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;


    public static PagedList<T> Create(List<T> source, int pageIndex)
    {
        var count = source.Count;
        var items = source.Skip((pageIndex - 1) * PageSize).Take(PageSize).ToList();
        return new PagedList<T>(items, count, pageIndex);
    }

}
