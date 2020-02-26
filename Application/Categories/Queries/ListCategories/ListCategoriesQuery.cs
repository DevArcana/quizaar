using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Categories.Commands.CreateCategory;
using Application.Common.Extensions;
using Application.Common.Interfaces;
using Application.Common.Queries;
using Application.Common.ViewModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace Application.Categories.Queries.ListCategories
{
    public class ListCategoriesQuery : PaginatedQuery, IRequest<PaginatedList<ListCategoriesViewModel>>
    {
        public ListCategoriesQuery(int page, int itemsPerPage, string sortQuery, string search) : base(page, itemsPerPage, sortQuery, search)
        {

        }

        public class CommandHandler : IRequestHandler<ListCategoriesQuery, PaginatedList<ListCategoriesViewModel>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public CommandHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<PaginatedList<ListCategoriesViewModel>> Handle(ListCategoriesQuery request, CancellationToken cancellationToken)
            {
                var categories = _context.Categories
                    .ProjectTo<ListCategoriesViewModel>(_mapper.ConfigurationProvider)
                    .Sort(request.SortQuery);

                if (!string.IsNullOrWhiteSpace(request.Search))
                {
                    categories = categories.Where(x => x.Name.Contains(request.Search, StringComparison.InvariantCultureIgnoreCase));
                }

                return await categories.PaginateAsync(request.Page, request.ItemsPerPage, cancellationToken);
            }
        }
    }
}
