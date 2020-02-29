using Application.Common.ViewModels;
using MediatR;

namespace Application.Common.Queries
{
    public abstract class PaginatedQuery<T> : IRequest<PaginatedList<T>>
    {
        public int Page { get; set; }
        public int ItemsPerPage { get; set; }

        public string Sort { get; set; }
        public string Search { get; set; }
    }
}
