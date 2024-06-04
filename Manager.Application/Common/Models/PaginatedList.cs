using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Manager.Application.Common.Models;

public class PaginatedList<TOut>
{
    public List<TOut> Items { get; }
    public int PageNumber { get; } = 1;
    public int TotalPages { get; }
    public int TotalCount { get; }

    public PaginatedList(List<TOut> items, int count, int pageNumber, int pageSize)
    {
        Items = items;
        PageNumber = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        TotalCount = count;
    }

    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;

    public static async Task<PaginatedList<TOut>> CreateAsync<T>(IQueryable<T> source,
        int pageNumber,
        int pageSize,
        IMapper mapper)
    {
        var count = await source.CountAsync();
        var items = await source
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        var dto = mapper.Map<List<TOut>>(items);
        return new PaginatedList<TOut>(dto, count, pageNumber, pageSize);
    }
}
