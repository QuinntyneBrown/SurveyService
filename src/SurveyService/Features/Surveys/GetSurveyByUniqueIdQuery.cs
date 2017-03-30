using MediatR;
using SurveyService.Data;
using SurveyService.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;
using System;

namespace SurveyService.Features.Surveys
{
    public class GetSurveyByUniqueIdQuery
    {
        public class GetSurveyByUniqueIdRequest : IRequest<GetSurveyByUniqueIdResponse>
        {
            public int? TenantId { get; set; }
            public Guid? UniqueId { get; set; }
        }

        public class GetSurveyByUniqueIdResponse
        {
            public SurveyApiModel Survey { get; set; }
        }

        public class GetSurveyByUniqueIdHandler : IAsyncRequestHandler<GetSurveyByUniqueIdRequest, GetSurveyByUniqueIdResponse>
        {
            public GetSurveyByUniqueIdHandler(SurveyServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetSurveyByUniqueIdResponse> Handle(GetSurveyByUniqueIdRequest request)
            {
                return new GetSurveyByUniqueIdResponse()
                {
                    Survey = SurveyApiModel.FromSurvey(await _context.Surveys
                    .Include(x => x.Questions)
                    .SingleAsync(x => x.UniqueId == request.UniqueId))
                };
            }

            private readonly SurveyServiceContext _context;
            private readonly ICache _cache;
        }
    }
}
