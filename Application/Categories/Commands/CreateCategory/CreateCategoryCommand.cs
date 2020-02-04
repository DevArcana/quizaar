using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.ErrorHandling;
using Application.Common.Interfaces;
using Application.Common.ViewModels;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommand : IRequest<CreatedObject>
    {
        public string CategoryName { get; set; }

        public class CommandHandler : IRequestHandler<CreateCategoryCommand, CreatedObject>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public CommandHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<CreatedObject> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
            {
                var entity = new Category(request.CategoryName);

                _context.Categories.Add(entity);
                await _context.SaveChangesAsync(cancellationToken);

                return _mapper.Map<CreatedObject>(entity);
            }
        }
    }
}
