using MediatR;
using SurveyService.Data;
using SurveyService.Features.Core;
using System;
using System.Threading.Tasks;

namespace SurveyService.Features.Notifications
{
    public class SendSurveyCompletedNotificationCommand
    {
        public class SendSurveyCompletedNotificationRequest : IRequest<SendSurveyCompletedNotificationResponse>
        {
            public int? SurveyResultId { get; set; }
            public int? TenantId { get; set; }
        }

        public class SendSurveyCompletedNotificationResponse { }

        public class SendSurveyCompletedNotificationHandler : IAsyncRequestHandler<SendSurveyCompletedNotificationRequest, SendSurveyCompletedNotificationResponse>
        {
            public SendSurveyCompletedNotificationHandler(SurveyServiceContext context, ICache cache, INotificationService notificationService)
            {
                _context = context;
                _cache = cache;
                _notificationService = notificationService;
            }

            public async Task<SendSurveyCompletedNotificationResponse> Handle(SendSurveyCompletedNotificationRequest request)
            {                
                var mailMessage = _notificationService.BuildSurveyCompletedMessage();
                _notificationService.ResolveRecipients(ref mailMessage);
                var result = await _notificationService.SendAsync(mailMessage);
                return new SendSurveyCompletedNotificationResponse();
            }

            private readonly SurveyServiceContext _context;
            private readonly INotificationService _notificationService;
            private readonly ICache _cache;
        }
    }
}