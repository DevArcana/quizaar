using FluentValidation;

namespace Application.Common.Queries
{
    public abstract class PaginatedQueryValidator<T, R> : AbstractValidator<T> where T : PaginatedQuery<R>
    {
        protected PaginatedQueryValidator()
        {
            RuleFor(x => x.Page).GreaterThan(0);
            RuleFor(x => x.ItemsPerPage).GreaterThan(0);
        }
    }

    public abstract class PaginatedResultQueryValidator<T, R> : AbstractValidator<T> where T : PaginatedResultQuery<R>
    {
        protected PaginatedResultQueryValidator()
        {
            RuleFor(x => x.Page).GreaterThan(0);
            RuleFor(x => x.ItemsPerPage).GreaterThan(0);
        }
    }
}
