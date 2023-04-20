using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Flubr.Models
{
    public class MessagesModel
    {
        [Key]
        public string MessageId { get; set; }
        public string MessageName { get; set; }
        public DateTime RecentDate { get; set; }
    }
}
