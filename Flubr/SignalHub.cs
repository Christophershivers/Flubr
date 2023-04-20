using Flubr.Areas.Identity.Data;
using Flubr.Models;
using Flubr.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Flubr
{
    public class SignalHub : Hub
    {
        private readonly FlubrContext _FC;
        private readonly MessagesRepository _MP;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _UM;
        public SignalHub(FlubrContext FC, MessagesRepository MP, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> UM)
        {
            _FC = FC;
            _MP = MP;
            _httpContextAccessor = httpContextAccessor;
            _UM = UM;
        }






        public async Task AddGroup(string id, string url)
        {
            await Groups.AddToGroupAsync(id, url);
        }

        public async Task PrivateMessage(string id = "")
        {
            var privateMessages = _MP.PrivateMessage(id);

            
            await Clients.Group(id).SendAsync("PriMess", privateMessages);
        }


        public override Task OnConnectedAsync()
        {
            var id = _UM.GetUserAsync(_httpContextAccessor.HttpContext.User);


            ConnectedUsers.Ids.Add(id.ToString());
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var id = _UM.GetUserAsync(_httpContextAccessor.HttpContext.User);
            ConnectedUsers.Ids.Remove(id.ToString());
            return base.OnDisconnectedAsync(exception);
        }

        public async Task Send(string message, string url )
        {
           

            UserMessagesModel um = new UserMessagesModel();
            Guid g = Guid.NewGuid();
            um.Id = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            um.Message = message;
            um.UserMessageId = g;
            um.MessageId = url;
            um.Date = DateTime.Now;
            _FC.UserMessage.Add(um);
            await _FC.SaveChangesAsync();

            var mess = _MP.PrivateMessage(url);
            await Clients.Group(url).SendAsync("PriMess", mess);

            //await Clients.All.SendAsync("MessageSent", name, message);
        }


    }
}
