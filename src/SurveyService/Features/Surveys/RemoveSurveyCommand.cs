using MediatR;
using SurveyService.Data;
using SurveyService.Data.Model;
using SurveyService.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace SurveyService.Features.Surveys
{
    public class RemoveSurveyCommand
    {
        public class RemoveSurveyRequest : IRequest<RemoveSurveyResponse>
        {
            public int Id { get; set; }
        }

        public class RemoveSurveyResponse { }

        public class RemoveSurveyHandler : IAsyncRequestHandler<RemoveSurveyRequest, RemoveSurveyResponse>
        {
            public RemoveSurveyHandler(SurveyServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<RemoveSurveyResponse> Handle(RemoveSurveyRequest request)
            {
                var survey = await _context.Surveys.FindAsync(request.Id);
                survey.IsDeleted = true;
                await _context.SaveChangesAsync();
                return new RemoveSurveyResponse();
            }

            private readonly SurveyServiceContext _context;
            private readonly ICache _cache;
        }
    }
}
