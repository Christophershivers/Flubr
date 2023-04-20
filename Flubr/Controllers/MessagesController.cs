using Flubr.Areas.Identity.Data;
using Flubr.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flubr.Controllers
{
    public class MessagesController : Controller
    {
        private readonly FlubrContext _FC;
        private readonly UserManager<ApplicationUser> _UM;
        private readonly IHubContext<SignalHub> _signal;

        public MessagesController(FlubrContext FC, UserManager<ApplicationUser> UM, IHubContext<SignalHub> signal)
        {
            _FC = FC;
            _UM = UM;
            _signal = signal;

        }

        
      
        public IActionResult Open(string id)
        {
            ViewBag.id = ConnectedUsers.Ids;

            var messageName = _FC.Message.Where(o => o.MessageId == id).Select(s => s.MessageName).SingleOrDefault();

            ViewBag.messageName = messageName;
            //var message = _FC.UserMessage.Include("applicationUser").Where(o => o.MessageId == id).Where(o => o.Message != "").OrderBy(d => d.Date).ToList();

            //var listMessages = _FC.Message.ToList();

            //umo.UM = message;
            //umo.MM = listMessages;
            
            return View();
        }
        //[HttpPost]
        //public async Task<IActionResult> Open(string id, UserMessagesModel um, UserMessagesObj umo)
        //{

        //    var message = _FC.UserMessage.Include("applicationUser").Where(o => o.MessageId == id).Where(o => o.Message != " ").OrderBy(d => d.Date).ToList();
        //    var listMessages = _FC.Message.ToList();
        //    Guid g = Guid.NewGuid();

        //    umo.UM = message;
        //    umo.MM = listMessages;
        //    umo.UM2.UserMessageId = g;
        //    umo.UM2.Id = _UM.GetUserAsync(User).Result.Id;
            
        //    umo.UM2.MessageId = id;
        //    umo.UM2.Date = DateTime.Now;
        //    _FC.UserMessage.Add(umo.UM2);
        //    await _FC.SaveChangesAsync();
        //    //await _signal.Clients.All.SendAsync("LoadData");

        //    return View(umo);
        //}
    }
}
