using SurveyService.Security;
using MediatR;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using static SurveyService.Features.SurveyResults.AddOrUpdateSurveyResultCommand;
using static SurveyService.Features.SurveyResults.GetSurveyResultsQuery;
using static SurveyService.Features.SurveyResults.GetSurveyResultByIdQuery;
using static SurveyService.Features.SurveyResults.RemoveSurveyResultCommand;

namespace SurveyService.Features.SurveyResults
{
    [Authorize]
    [RoutePrefix("api/surveyResult")]
    public class SurveyResultController : ApiController
    {
        public SurveyResultController(IMediator mediator, IUserManager userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        [Route("add")]
        [HttpPost]
        [AllowAnonymous]
        [ResponseType(typeof(AddOrUpdateSurveyResultResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdateSurveyResultRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateSurveyResultResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdateSurveyResultRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetSurveyResultsResponse))]
        public async Task<IHttpActionResult> Get()
        {
            var request = new GetSurveyResultsRequest();
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetSurveyResultByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetSurveyResultByIdRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveSurveyResultResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveSurveyResultRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        protected readonly IMediator _mediator;
        protected readonly IUserManager _userManager;
    }
}
