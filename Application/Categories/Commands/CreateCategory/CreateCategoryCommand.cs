using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Categories.Commands.CreateCategory
{
    public partial class CreateCategoryCommand : IRequest<long>
    {
        public string CategoryName { get; set; }

        public class CommandHandler : IRequestHandler<CreateCategoryCommand, long>
        {
            private readonly IApplicationDbContext _context;

            public CommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<long> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
            {
                var entity = new Category(request.CategoryName);

                _context.Categories.Add(entity);
                await _context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}
