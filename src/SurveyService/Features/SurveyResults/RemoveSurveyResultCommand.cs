using MediatR;
using SurveyService.Data;
using SurveyService.Data.Model;
using SurveyService.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace SurveyService.Features.SurveyResults
{
    public class RemoveSurveyResultCommand
    {
        public class RemoveSurveyResultRequest : IRequest<RemoveSurveyResultResponse>
        {
            public int Id { get; set; }
            public int? TenantId { get; set; }
        }

        public class RemoveSurveyResultResponse { }

        public class RemoveSurveyResultHandler : IAsyncRequestHandler<RemoveSurveyResultRequest, RemoveSurveyResultResponse>
        {
            public RemoveSurveyResultHandler(SurveyServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<RemoveSurveyResultResponse> Handle(RemoveSurveyResultRequest request)
            {
                var surveyResult = await _context.SurveyResults.SingleAsync(x=>x.Id == request.Id && x.TenantId == request.TenantId);
                surveyResult.IsDeleted = true;
                await _context.SaveChangesAsync();
                return new RemoveSurveyResultResponse();
            }

            private readonly SurveyServiceContext _context;
            private readonly ICache _cache;
        }
    }
}
