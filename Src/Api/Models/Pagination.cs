namespace Api.Models
{
    public class Pagination
    {
        public int PageNumber { get; private set; }

        public int PageSize { get; private set; }

        public int TotalPages { get; private set; }

        public int TotalRecords { get; private set; }
        public string FirstPage { get; private set; }
        public string LastPage { get; private set; }
        public string PreviousPage { get; private set; }
        public string NextPage { get; private set; }

        public Pagination(int pageNumber, int pageSize, int totalPages, int totalRecords)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalPages = totalPages;
            TotalRecords = totalRecords;
        }
    }
}
