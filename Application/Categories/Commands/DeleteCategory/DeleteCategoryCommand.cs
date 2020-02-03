using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.ErrorHandling;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommand : IRequest<Result>
    {
        public long Id { get; set; }

        public DeleteCategoryCommand(long id)
        {
            Id = id;
        }

        public class CommandHandler : IRequestHandler<DeleteCategoryCommand, Result>
        {
            private readonly IApplicationDbContext _context;

            public CommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Categories.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (entity == null)
                {
                    return Result.Failure(ErrorType.NOT_FOUND, $"Couldn't find category of id {request.Id}.");
                }

                _context.Categories.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return Result.Ok();
            }
        }
    }
}
