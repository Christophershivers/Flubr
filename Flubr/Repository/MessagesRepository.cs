using Flubr.Areas.Identity.Data;
using Flubr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flubr.Repository
{
    public class MessagesRepository
    {
        private readonly FlubrContext _FC;

        public MessagesRepository(FlubrContext FC)
        {

            _FC = FC;

        }

        public List<PrivateMessage> PrivateMessage(string id)
        {
            

            List<PrivateMessage> message = _FC.UserMessage.Join(_FC.Users, message => message.Id, user => user.Id, (message, user) => new { message, user }).Where(o => o.message.MessageId == id).Select(s => new PrivateMessage { FirstName = s.user.FirstName, LastName = s.user.LastName, UsertName = s.user.UserName, Message = s.message.Message, Date = s.message.Date, Id = s.user.Id, ProfileImage = s.user.ProfileImage }).OrderBy(x => x.Date).ToList();
            return message;
        }


    }
}
