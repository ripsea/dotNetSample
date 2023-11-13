using System.Reflection;

namespace Api.Models
{
    public class ShoeRetrievalService: IShoeRetrievalService
    {
        private readonly ISortHelper<Shoe> _sortHelper;
        public ShoeRetrievalService(ISortHelper<Shoe> sortHelper)
        {
            _sortHelper = sortHelper;
        }

        public PagedList<Shoe> GetPagedSortingShoes(
            QueryStringParameters shoeParameters,
            IQueryable<Shoe> shoes)
        {
            var sortedOwners = 
                _sortHelper.ApplySort(shoes, shoeParameters.OrderBy);

            return PagedList<Shoe>.ToPagedList(
                sortedOwners,
                pageNumber: shoeParameters.PageNumber,
                pageSize: shoeParameters.PageSize
                );
        }

        public IQueryable<Shoe> GetShoes()
        {
            return new List<Shoe>
        {
            new()
            {
                Name = "Pegasus 39",
                Brand = "Nike",
                Price = 119.99M,
                Category = "Running",
                Rating = 4.5M,
                Retailer = new Retailer(){ ID=1,Name="A"}
            },
            new()
            {
                Name = "Pegasus Trail 3",
                Brand = "Nike",
                Price = 129.99M,
                Category = "Trail",
                Rating = 3.8M,
                Retailer = new Retailer(){ ID=1,Name="A"}
            },
            new()
            {
                Name = "Ride 15",
                Brand = "Saucony",
                Price = 119.99M,
                Category = "Neutral",
                Rating = 4.9M,
                Retailer = new Retailer(){ ID=2,Name="A"}
            },
            new()
            {
                Name = "Tesla 15",
                Brand = "Saucony",
                Price = 300.99M,
                Category = "Neutral",
                Rating = 4.0M,
                Retailer = new Retailer(){ ID=2,Name="B"}
            },
            new()
            {
                Name = "Jacket 33",
                Brand = "Adidas",
                Price = 5.99M,
                Category = "Sport",
                Rating = 2.0M,
                Retailer = new Retailer(){ ID=2,Name="B"}
            },
            new()
            {
                Name = "Classic 33",
                Brand = "Adidas",
                Price = 5.99M,
                Category = "Sport",
                Rating = 2.0M,
                Retailer = new Retailer(){ ID=2,Name="B"}
            }
        }.AsQueryable();
        }
    }
}
