using MediatR;
using SurveyService.Data;
using SurveyService.Data.Model;
using SurveyService.Features.Core;
using System.Threading.Tasks;
using System.Data.Entity;
using System;

using static SurveyService.Features.Notifications.SendSurveyCompletedNotificationCommand;

namespace SurveyService.Features.SurveyResults
{
    public class AddOrUpdateSurveyResultCommand
    {
        public class AddOrUpdateSurveyResultRequest : IRequest<AddOrUpdateSurveyResultResponse>
        {
            public SurveyResultApiModel SurveyResult { get; set; }
            public int? TenantId { get; set; }
            public Guid SurveyUniqueId { get; set; }
        }

        public class AddOrUpdateSurveyResultResponse { }

        public class AddOrUpdateSurveyResultHandler : IAsyncRequestHandler<AddOrUpdateSurveyResultRequest, AddOrUpdateSurveyResultResponse>
        {
            public AddOrUpdateSurveyResultHandler(SurveyServiceContext context, ICache cache, IMediator mediator)
            {
                _context = context;
                _cache = cache;
                _mediator = mediator;
            }

            public async Task<AddOrUpdateSurveyResultResponse> Handle(AddOrUpdateSurveyResultRequest request)
            {
                var entity = new SurveyResult();

                var survey = await _context.Surveys.Include(x => x.SurveyResults).SingleAsync(x => x.UniqueId == request.SurveyUniqueId);
                entity.Name = request.SurveyResult.Name;
                entity.TenantId = survey.TenantId;
                entity.SurveyId = survey.Id;

                foreach (var responseApiModel in request.SurveyResult.Responses) {
                    var response = new Response();
                    response.Value = responseApiModel.Value;
                    response.QuestionId = responseApiModel.QuestionId;
                    entity.Responses.Add(response);
                }

                survey.SurveyResults.Add(entity);

                await _context.SaveChangesAsync();

                //await _mediator.Send(new SendSurveyCompletedNotificationRequest()
                //{
                //    TenantId = request.TenantId,
                //    SurveyResultId = entity.Id
                //});

                return new AddOrUpdateSurveyResultResponse();
            }

            private readonly SurveyServiceContext _context;
            private readonly ICache _cache;
            private readonly IMediator _mediator;
        }
    }
}