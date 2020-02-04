using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Categories.Commands.RenameCategory
{
    public class RenameCategoryCommandValidator : AbstractValidator<RenameCategoryCommand>
    {
        private readonly IApplicationDbContext _context;

        public RenameCategoryCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.CategoryName)
                .NotEmpty().WithMessage("Name cannot be empty.")
                .MaximumLength(200).WithMessage("Name cannot exceed 200 characters.")
                .MustAsync(BeUniqueName).WithMessage("Such a category already exists.");
        }

        public async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
        {
            return await _context.Categories.AllAsync(x => x.Name != name, cancellationToken);
        }
    }
}
