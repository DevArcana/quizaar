using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Categories.Queries.ListCategories;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Categories.Queries.CategoryDetails
{
    public class CategoryDetailsQuery : IRequest<CategoryDetailsViewModel>
    {
        public long Id { get; set; }

        public class CommandHandler : IRequestHandler<CategoryDetailsQuery, CategoryDetailsViewModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public CommandHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<CategoryDetailsViewModel> Handle(CategoryDetailsQuery request, CancellationToken cancellationToken)
            {
                var category = await _context.Categories
                    .ProjectTo<CategoryDetailsViewModel>(_mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                return category;
            }
        }
    }
}
