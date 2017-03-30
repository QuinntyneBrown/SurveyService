using MediatR;
using SurveyService.Data;
using SurveyService.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace SurveyService.Features.SurveyResults
{
    public class GetSurveyResultByIdQuery
    {
        public class GetSurveyResultByIdRequest : IRequest<GetSurveyResultByIdResponse> { 
            public int Id { get; set; }
            public int? TenantId { get; set; }
        }

        public class GetSurveyResultByIdResponse
        {
            public SurveyResultApiModel SurveyResult { get; set; } 
        }

        public class GetSurveyResultByIdHandler : IAsyncRequestHandler<GetSurveyResultByIdRequest, GetSurveyResultByIdResponse>
        {
            public GetSurveyResultByIdHandler(SurveyServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetSurveyResultByIdResponse> Handle(GetSurveyResultByIdRequest request)
            {                
                return new GetSurveyResultByIdResponse()
                {
                    SurveyResult = SurveyResultApiModel.FromSurveyResult(await _context.SurveyResults
                    .Include(x=>x.Responses)
                    .Include("Responses.Question")
                    .SingleAsync(x=>x.Id == request.Id && x.TenantId == request.TenantId))
                };
            }

            private readonly SurveyServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
