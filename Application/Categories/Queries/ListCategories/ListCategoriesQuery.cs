using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Categories.Commands.CreateCategory;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Categories.Queries.ListCategories
{
    public class ListCategoriesQuery : IRequest<IEnumerable<ListCategoriesViewModel>>
    {
        public class CommandHandler : IRequestHandler<ListCategoriesQuery, IEnumerable<ListCategoriesViewModel>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public CommandHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<IEnumerable<ListCategoriesViewModel>> Handle(ListCategoriesQuery request, CancellationToken cancellationToken)
            {
                var categories = await _context.Categories
                    .ProjectTo<ListCategoriesViewModel>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                return categories;
            }
        }
    }
}
