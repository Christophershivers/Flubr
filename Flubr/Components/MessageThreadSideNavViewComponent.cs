using Flubr.Areas.Identity.Data;
using Flubr.Models;
using Flubr.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flubr.Components
{
    public class MessageThreadSideNavViewComponent: ViewComponent
    {
        private readonly ILogger<MessageThreadViewComponent> _logger;
        private readonly FlubrContext _FC;
        private readonly UserManager<ApplicationUser> _UM;
        private readonly SignalHub _Sig;
        private readonly MessagesRepository _MP;

        public MessageThreadSideNavViewComponent(ILogger<MessageThreadViewComponent> logger, FlubrContext FC, UserManager<ApplicationUser> UM, SignalHub Sig, MessagesRepository MP)
        {
            _logger = logger;
            _FC = FC;
            _UM = UM;
            _Sig = Sig;
            _MP = MP;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _UM.GetUserAsync(HttpContext.User);
            var id = user.Id;
            List<MessageThreadCascade> myMessageThreads = _FC.MessageThread
                .Where(o => o.UserId == id)
                .Join(_FC.Users, 
                    messagethread => messagethread.UserId, 
                    users => users.Id, 
                    (messagethread, users) => new { messagethread, users })
                .Join(_FC.Message, 
                    messagethread2 => messagethread2.messagethread.MessageId, 
                    message => message.MessageId, 
                    (messagethread2, message)=> new {messagethread2, message })
                .Select(s => new MessageThreadCascade { 
                    ProfileImage = s.messagethread2.users.ProfileImage, 
                    MessageName = s.message.MessageName,
                    MessageId = s.message.MessageId 
                }).ToList();
            
            return View(myMessageThreads);
        }
    }
}
