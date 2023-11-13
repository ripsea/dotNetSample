namespace Api.Models
{
    public interface ISortHelper<T>
    {
        IQueryable<T> ApplySort(IQueryable<T> entities, string orderByQueryString);
    }
}