using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Categories.Queries.ListCategories;
using Application.Common.ErrorHandling;
using Application.Common.Extensions;
using Application.Common.Interfaces;
using Application.Common.Queries;
using Application.Common.ViewModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Questions.Queries.ListQuestions
{
    public class ListQuestionsQuery : PaginatedQuery<QuestionDto>
    {
        public long CategoryId { get; set; }

        public class CommandHandler : IRequestHandler<ListQuestionsQuery, PaginatedList<QuestionDto>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public CommandHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<PaginatedList<QuestionDto>> Handle(ListQuestionsQuery request, CancellationToken cancellationToken)
            {
                var questions = _context.Questions
                    .Where(x => x.CategoryId == request.CategoryId)
                    .ProjectTo<QuestionDto>(_mapper.ConfigurationProvider)
                    .Sort(request.Sort);

                if (!string.IsNullOrWhiteSpace(request.Search))
                {
                    questions = questions.Where(x => x.Content.Contains(request.Search, StringComparison.InvariantCultureIgnoreCase));
                }

                return await questions.PaginateAsync(request.Page, request.ItemsPerPage, cancellationToken);
            }
        }
    }
}
