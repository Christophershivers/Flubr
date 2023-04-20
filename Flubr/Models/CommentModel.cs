using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Flubr.Models
{
    public class CommentModel
    {
        [Key]
        public Guid CommentId { get; set; }
        public Guid PostId  { get; set; }
        public string UserId { get; set; }
        public string Comment { get; set; }
        public int Likes { get; set; }
        public string Date { get; set; }
    }
}
