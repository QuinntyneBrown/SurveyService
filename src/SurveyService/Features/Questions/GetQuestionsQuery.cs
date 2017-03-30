using MediatR;
using SurveyService.Data;
using SurveyService.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace SurveyService.Features.Questions
{
    public class GetQuestionsQuery
    {
        public class GetQuestionsRequest : IRequest<GetQuestionsResponse> { 
            public int? TenantId { get; set; }        
        }

        public class GetQuestionsResponse
        {
            public ICollection<QuestionApiModel> Questions { get; set; } = new HashSet<QuestionApiModel>();
        }

        public class GetQuestionsHandler : IAsyncRequestHandler<GetQuestionsRequest, GetQuestionsResponse>
        {
            public GetQuestionsHandler(SurveyServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetQuestionsResponse> Handle(GetQuestionsRequest request)
            {
                var questions = await _context.Questions
                    .Where( x => x.TenantId == request.TenantId )
                    .ToListAsync();

                return new GetQuestionsResponse()
                {
                    Questions = questions.Select(x => QuestionApiModel.FromQuestion(x)).ToList()
                };
            }

            private readonly SurveyServiceContext _context;
            private readonly ICache _cache;
        }

    }

}