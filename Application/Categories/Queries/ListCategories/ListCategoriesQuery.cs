using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Categories.Commands.CreateCategory;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Categories.Queries.ListCategories
{
    public class ListCategoriesQuery : IRequest<IEnumerable<Category>>
    {
        public class CommandHandler : IRequestHandler<ListCategoriesQuery, IEnumerable<Category>>
        {
            private readonly IApplicationDbContext _context;

            public CommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Category>> Handle(ListCategoriesQuery request, CancellationToken cancellationToken)
            {
                return await _context.Categories.ToListAsync(cancellationToken);
            }
        }
    }
}
