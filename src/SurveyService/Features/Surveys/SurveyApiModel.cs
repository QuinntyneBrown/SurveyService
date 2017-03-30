using SurveyService.Data.Model;
using SurveyService.Features.Questions;
using SurveyService.Features.SurveyResults;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SurveyService.Features.Surveys
{
    public class SurveyApiModel
    {        
        public int Id { get; set; }

        public string Name { get; set; }

        public string LogoUrl { get; set; }

        public int? TenantId { get; set; }

        public Guid UniqueId { get; set; }

        public string Description { get; set; }

        public ICollection<QuestionApiModel> Questions { get; set; } = new HashSet<QuestionApiModel>();

        public ICollection<SurveyResultApiModel> SurveyResults { get; set; } = new HashSet<SurveyResultApiModel>();

        public static TModel FromSurvey<TModel>(Survey survey) where
            TModel : SurveyApiModel, new()
        {
            var model = new TModel();

            model.Id = survey.Id;

            model.Name = survey.Name;

            model.UniqueId = survey.UniqueId;

            model.LogoUrl = survey.LogoUrl;

            model.Description = survey.Description;

            model.TenantId = survey.TenantId;

            model.Questions = survey.Questions
                .OrderBy(x=>x.OrderIndex)
                .Select(x => QuestionApiModel.FromQuestion(x)).ToList();

            model.SurveyResults = survey.SurveyResults
                .Select(x => SurveyResultApiModel.FromSurveyResult(x)).ToList();

            return model;
        }

        public static SurveyApiModel FromSurvey(Survey survey)
            => FromSurvey<SurveyApiModel>(survey);
    }
}
