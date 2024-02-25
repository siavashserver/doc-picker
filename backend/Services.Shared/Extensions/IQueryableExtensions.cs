namespace Services.Shared.Extensions;

public static class IQueryableExtensions
{
    public static IQueryable<TEntity> Paginate<TEntity>(this IQueryable<TEntity> queryable, int page, int pageSize)
    {
        return queryable.Skip(page * pageSize).Take(pageSize);
    }
}