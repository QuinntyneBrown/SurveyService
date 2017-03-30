using SurveyService.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using SurveyService.Features.Core;

namespace SurveyService.Features.SurveyResults
{
    public class SurveyResultApiModel
    {        
        public int Id { get; set; }
        public int? TenantId { get; set; }
        public string Name { get; set; }
        public string CreatedOn { get; set; }
        public ICollection<ResponseApiModel> Responses { get; set; } = new HashSet<ResponseApiModel>();

        public static TModel FromSurveyResult<TModel>(SurveyResult surveyResult) where
            TModel : SurveyResultApiModel, new()
        {
            TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

            var model = new TModel();
            model.Id = surveyResult.Id;
            model.TenantId = surveyResult.TenantId;
            model.Name = surveyResult.Name;
            model.CreatedOn = surveyResult.CreatedOn.ToWebUtcString();
            model.Responses = surveyResult.Responses.Select(x => ResponseApiModel.FromResponse(x)).ToList();
            return model;
        }

        public static SurveyResultApiModel FromSurveyResult(SurveyResult surveyResult)
            => FromSurveyResult<SurveyResultApiModel>(surveyResult);

    }
}
