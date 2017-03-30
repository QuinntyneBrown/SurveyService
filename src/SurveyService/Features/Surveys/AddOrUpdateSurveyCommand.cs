using MediatR;
using SurveyService.Data;
using SurveyService.Data.Model;
using SurveyService.Features.Core;
using System.Threading.Tasks;
using System.Data.Entity;
using System;

namespace SurveyService.Features.Surveys
{
    public class AddOrUpdateSurveyCommand
    {
        public class AddOrUpdateSurveyRequest : IRequest<AddOrUpdateSurveyResponse>
        {
            public SurveyApiModel Survey { get; set; }
            public int? TenantId { get; set; }
        }

        public class AddOrUpdateSurveyResponse { }

        public class AddOrUpdateSurveyHandler : IAsyncRequestHandler<AddOrUpdateSurveyRequest, AddOrUpdateSurveyResponse>
        {
            public AddOrUpdateSurveyHandler(SurveyServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<AddOrUpdateSurveyResponse> Handle(AddOrUpdateSurveyRequest request)
            {
                var entity = await _context.Surveys
                    .Include(x=>x.Questions)
                    .SingleOrDefaultAsync(x => x.Id == request.Survey.Id && x.TenantId == request.TenantId);
                if (entity == null) _context.Surveys.Add(entity = new Survey());

                entity.Name = request.Survey.Name;

                entity.TenantId = request.TenantId;

                entity.UniqueId = request.Survey.UniqueId;

                entity.LogoUrl = request.Survey.LogoUrl;

                entity.Description = request.Survey.Description;

                entity.Questions.Clear();

                foreach(var questionApiModel in request.Survey.Questions)
                {
                    var question = await _context.Questions.SingleOrDefaultAsync(x => x.Id == questionApiModel.Id);

                    if(question == null) { question = new Question(); }

                    question.TenantId = request.TenantId;

                    question.Body = questionApiModel.Body;

                    question.Name = questionApiModel.Name;

                    question.OrderIndex = questionApiModel.OrderIndex;

                    question.SurveyId = entity.Id;

                    question.Description = questionApiModel.Description;

                    question.QuestionType = questionApiModel.QuestionType;

                    entity.Questions.Add(question);
                }

                await _context.SaveChangesAsync();

                return new AddOrUpdateSurveyResponse();
            }

            private readonly SurveyServiceContext _context;
            private readonly ICache _cache;
        }
    }
}