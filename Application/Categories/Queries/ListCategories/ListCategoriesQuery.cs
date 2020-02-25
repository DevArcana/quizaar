using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Categories.Commands.CreateCategory;
using Application.Common.Interfaces;
using Application.Common.Queries;
using Application.Common.ViewModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Categories.Queries.ListCategories
{
    public class ListCategoriesQuery : PaginatedQuery, IRequest<PaginatedList<ListCategoriesViewModel>>
    {
        public ListCategoriesQuery(int page, int itemsPerPage) : base(page, itemsPerPage)
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
                var categories = await _context.Categories
                    .ProjectTo<ListCategoriesViewModel>(_mapper.ConfigurationProvider)
                    .PaginateAsync(request.Page, request.ItemsPerPage, cancellationToken);

                return categories;
            }
        }
    }
}
