namespace Api.Models
{
    public interface IShoeRetrievalService
    {
        public IQueryable<Shoe> GetShoes();
        public PagedList<Shoe> GetPagedSortingShoes(
            QueryStringParameters shoeParameters,
            IQueryable<Shoe> shoes);
    }
}