namespace Application.Common.Queries
{
    public abstract class PaginatedQuery
    {
        public int Page { get; }
        public int ItemsPerPage { get; }

        public string SortQuery { get; }
        public string Search { get; }

        protected PaginatedQuery(int page, int itemsPerPage, string sortQuery, string search)
        {
            Page = page;
            ItemsPerPage = itemsPerPage;
            SortQuery = sortQuery;
            Search = search;
        }
    }
}
