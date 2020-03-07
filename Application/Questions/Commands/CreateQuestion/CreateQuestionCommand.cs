using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Categories.Commands;
using Application.Categories.Commands.CreateCategory;
using Application.Common.ErrorHandling;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Questions.Commands.CreateQuestion
{
    public class CreateQuestionCommand : IRequest<Result<CreateQuestionViewModel>>
    {
        public long CategoryId { get; set; }
        public bool IsOpen { get; set; }
        public string Question { get; set; }

        public class CommandHandler : IRequestHandler<CreateQuestionCommand, Result<CreateQuestionViewModel>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public CommandHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<CreateQuestionViewModel>> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
            {
                var question = new Question(request.IsOpen, request.Question);

                var category = await _context.Categories
                    .Include(x => x.Questions)
                    .SingleOrDefaultAsync(x => x.Id == request.CategoryId, cancellationToken);
                
                if (category == null)
                    return Result.Failure(ErrorType.NOT_FOUND, $"Couldn't find category with id {request.CategoryId}");

                category.Questions.Add(question);
                await _context.SaveChangesAsync(cancellationToken);

                return Result.Ok(_mapper.Map<CreateQuestionViewModel>(question));
            }
        }
    }
}
