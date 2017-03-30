using MediatR;
using SurveyService.Data;
using SurveyService.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace SurveyService.Features.Surveys
{
    public class GetSurveyByIdQuery
    {
        public class GetSurveyByIdRequest : IRequest<GetSurveyByIdResponse> { 
			public int Id { get; set; }
            public int? TenantId { get; set; }
        }

        public class GetSurveyByIdResponse
        {
            public SurveyApiModel Survey { get; set; } 
		}

        public class GetSurveyByIdHandler : IAsyncRequestHandler<GetSurveyByIdRequest, GetSurveyByIdResponse>
        {
            public GetSurveyByIdHandler(SurveyServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetSurveyByIdResponse> Handle(GetSurveyByIdRequest request)
            {                
                return new GetSurveyByIdResponse()
                {
                    Survey = SurveyApiModel.FromSurvey(await _context.Surveys
                    .Include(x=>x.Questions)
                    .Include(x=>x.SurveyResults)
                    .Include("SurveyResults.Responses")
                    .Include("SurveyResults.Responses.Question")
                    .SingleAsync(x => x.Id == request.Id))
                };
            }

            private readonly SurveyServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
