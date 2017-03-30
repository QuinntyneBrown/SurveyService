using SurveyService.Data.Model;
using SurveyService.Features.Questions;

namespace SurveyService.Features.SurveyResults
{
    public class ResponseApiModel
    {        
        public int Id { get; set; }
        public int? TenantId { get; set; }
        public int? QuestionId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public QuestionApiModel Question { get; set; }

        public static TModel FromResponse<TModel>(Response response) where
            TModel : ResponseApiModel, new()
        {
            var model = new TModel();
            model.Id = response.Id;
            model.TenantId = response.TenantId;
            model.Value = response.Value;
            model.QuestionId = response.QuestionId;
            model.Question = QuestionApiModel.FromQuestion(response.Question);
            return model;
        }

        public static ResponseApiModel FromResponse(Response response)
            => FromResponse<ResponseApiModel>(response);

    }
}
