using Flubr.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Flubr.Models
{
    public class UserMessagesModel
    {
        [Key]
        public Guid UserMessageId { get; set; }
        [Column(TypeName ="nvarchar(450)")]
        [Required]
        public string Id { get; set; }
        [Column(TypeName = "nvarchar(450)")]
        [Required]
        public string MessageId { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("Id")]
        public ApplicationUser applicationUser { get; set; }
        
        [ForeignKey("MessageId")]
        public MessagesModel messagesModel { get; set; }
    }
}
