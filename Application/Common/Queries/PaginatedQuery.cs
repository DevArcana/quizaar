using Application.Common.ErrorHandling;
using Application.Common.ViewModels;
using MediatR;

namespace Application.Common.Queries
{
    public class PaginationOptions
    {
        public int Page { get; set; }
        public int ItemsPerPage { get; set; }

        public string Sort { get; set; }
        public string Search { get; set; }
    }

    public abstract class PaginatedQuery<T> : PaginationOptions, IRequest<PaginatedList<T>>
    {
    }

    public abstract class PaginatedResultQuery<T> : PaginationOptions, IRequest<Result<PaginatedList<T>>>
    {
    }
}
