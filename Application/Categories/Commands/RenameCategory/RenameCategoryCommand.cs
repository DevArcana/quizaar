using System.Threading;
using System.Threading.Tasks;
using Application.Common.ErrorHandling;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Categories.Commands.RenameCategory
{
    public class RenameCategoryCommand : IRequest<Result>
    {
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }

        public class CommandHandler : IRequestHandler<RenameCategoryCommand, Result>
        {
            private readonly IApplicationDbContext _context;

            public CommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(RenameCategoryCommand request, CancellationToken cancellationToken)
            {
                var category = await _context.Categories.SingleOrDefaultAsync(x => x.Id == request.CategoryId, cancellationToken);

                if (category == null)
                {
                    return Result.Failure(ErrorType.NOT_FOUND, $"Couldn't find category of id {request.CategoryId}.");
                }

                if (category.Name == request.CategoryName)
                {
                    return Result.Failure($"Category {request.CategoryId} already has the name: {request.CategoryName}");
                }

                category.Name = request.CategoryName;

                await _context.SaveChangesAsync(cancellationToken);

                return Result.Ok();
            }
        }
    }
}
