using MediatR;
using SurveyService.Data;
using SurveyService.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace SurveyService.Features.SurveyResults
{
    public class GetSurveyResultsQuery
    {
        public class GetSurveyResultsRequest : IRequest<GetSurveyResultsResponse> { 
            public int? TenantId { get; set; }        
        }

        public class GetSurveyResultsResponse
        {
            public ICollection<SurveyResultApiModel> SurveyResults { get; set; } = new HashSet<SurveyResultApiModel>();
        }

        public class GetSurveyResultsHandler : IAsyncRequestHandler<GetSurveyResultsRequest, GetSurveyResultsResponse>
        {
            public GetSurveyResultsHandler(SurveyServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetSurveyResultsResponse> Handle(GetSurveyResultsRequest request)
            {
                var surveyResults = await _context.SurveyResults
                    .Where( x => x.TenantId == request.TenantId )
                    .ToListAsync();

                return new GetSurveyResultsResponse()
                {
                    SurveyResults = surveyResults.Select(x => SurveyResultApiModel.FromSurveyResult(x)).ToList()
                };
            }

            private readonly SurveyServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
