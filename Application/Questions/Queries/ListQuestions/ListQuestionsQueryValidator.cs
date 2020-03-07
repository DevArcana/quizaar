using System;
using System.Collections.Generic;
using System.Text;
using Application.Common.Queries;
using FluentValidation;

namespace Application.Questions.Queries.ListQuestions
{
    public class ListQuestionsQueryValidator : PaginatedQueryValidator<ListQuestionsQuery, QuestionDto>
    {
        public ListQuestionsQueryValidator() : base()
        {
            RuleFor(x => x.CategoryId).GreaterThanOrEqualTo(1);
        }
    }
}
