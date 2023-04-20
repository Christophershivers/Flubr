using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Flubr.Models
{
    public class MessageThreadModel
    {
        [Key]
        public string MessageThreadId { get; set; }
        public string UserId { get; set; }
        public string MessageId { get; set; }
    }
}
