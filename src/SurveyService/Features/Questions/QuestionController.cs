using SurveyService.Security;
using MediatR;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using static SurveyService.Features.Questions.AddOrUpdateQuestionCommand;
using static SurveyService.Features.Questions.GetQuestionsQuery;
using static SurveyService.Features.Questions.GetQuestionByIdQuery;
using static SurveyService.Features.Questions.RemoveQuestionCommand;

namespace SurveyService.Features.Questions
{
    [Authorize]
    [RoutePrefix("api/question")]
    public class QuestionController : ApiController
    {
        public QuestionController(IMediator mediator, IUserManager userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateQuestionResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdateQuestionRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateQuestionResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdateQuestionRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetQuestionsResponse))]
        public async Task<IHttpActionResult> Get()
        {
            var request = new GetQuestionsRequest();
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetQuestionByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetQuestionByIdRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveQuestionResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveQuestionRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        protected readonly IMediator _mediator;
        protected readonly IUserManager _userManager;
    }
}
