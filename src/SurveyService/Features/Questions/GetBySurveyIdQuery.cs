using MediatR;
using SurveyService.Data;
using SurveyService.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace SurveyService.Features.Questions
{
    public class GetBySurveyIdQuery
    {
        public class GetBySurveyIdRequest : IRequest<GetBySurveyIdResponse>
        {
            public GetBySurveyIdRequest()
            {

            }
        }

        public class GetBySurveyIdResponse
        {
            public GetBySurveyIdResponse()
            {

            }
        }

        public class GetBySurveyIdHandler : IAsyncRequestHandler<GetBySurveyIdRequest, GetBySurveyIdResponse>
        {
            public GetBySurveyIdHandler(SurveyServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetBySurveyIdResponse> Handle(GetBySurveyIdRequest request)
            {
                throw new System.NotImplementedException();
            }

            private readonly SurveyServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
