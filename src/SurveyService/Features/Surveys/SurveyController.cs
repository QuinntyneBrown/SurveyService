using SurveyService.Security;
using MediatR;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

using static SurveyService.Features.Surveys.GetSurveyByUniqueIdQuery;

namespace SurveyService.Features.Surveys
{
    [Authorize]
    [RoutePrefix("api/survey")]
    public class SurveyController : ApiController
    {
        public SurveyController(IMediator mediator, IUserManager userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateSurveyCommand.AddOrUpdateSurveyResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdateSurveyCommand.AddOrUpdateSurveyRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateSurveyCommand.AddOrUpdateSurveyResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdateSurveyCommand.AddOrUpdateSurveyRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetSurveysQuery.GetSurveysResponse))]
        public async Task<IHttpActionResult> Get([FromUri]GetSurveysQuery.GetSurveysRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }
        
        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetSurveyByIdQuery.GetSurveyByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetSurveyByIdQuery.GetSurveyByIdRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("getByUniqueId")]
        [HttpGet]
        [AllowAnonymous]
        [ResponseType(typeof(GetSurveyByUniqueIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetSurveyByUniqueIdRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveSurveyCommand.RemoveSurveyResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveSurveyCommand.RemoveSurveyRequest request)
            => Ok(await _mediator.Send(request));

        protected readonly IMediator _mediator;
        protected readonly IUserManager _userManager;

    }
}
