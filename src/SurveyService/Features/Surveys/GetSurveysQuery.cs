using MediatR;
using SurveyService.Data;
using SurveyService.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;
using Microsoft.Owin;

namespace SurveyService.Features.Surveys
{
    public class GetSurveysQuery
    {
        public class GetSurveysRequest : IRequest<GetSurveysResponse> {
            public int? TenantId { get; set; }
        }

        public class GetSurveysResponse
        {
            public ICollection<SurveyApiModel> Surveys { get; set; } = new HashSet<SurveyApiModel>();            
        }

        public class GetSurveysHandler : IAsyncRequestHandler<GetSurveysRequest, GetSurveysResponse>
        {
            public GetSurveysHandler(SurveyServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetSurveysResponse> Handle(GetSurveysRequest request)
            {
               var surveys = await _context.Surveys
                    .Include(x=> x.Questions)
                    .Where(x=>x.TenantId == request.TenantId)
                    .ToListAsync();

                return new GetSurveysResponse()
                {
                    Surveys = surveys.Select(x => SurveyApiModel.FromSurvey(x)).ToList()
                };
            }

            private readonly SurveyServiceContext _context;
            private readonly ICache _cache;
        }
    }
}