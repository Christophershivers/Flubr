using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flubr.Models
{
    public class UserMessage
    {
        
        public Guid UserMessageId { get; set; }
        
        public string Id { get; set; }
        public string MessageId { get; set; }
        public string Messages { get; set; }
        public DateTime Date { get; set; }
    }
}
