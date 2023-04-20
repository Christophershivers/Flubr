using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flubr.Models
{
    public class UserMessagesObj
    {
        public IEnumerable<UserMessagesModel> UM { get; set; }
        public UserMessagesModel UM2 { get; set; }
        public IEnumerable<MessagesModel> MM { get; set; }
    }
}
