using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flubr.Models
{
    public class ViewUserCommentsModel
    {
        public Guid CommentId { get; set; }
        public Guid PostId { get; set; }
        public string UserId { get; set; }
        public string Comment { get; set; }
        public int Likes { get; set; }
        public string Date { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string ProfileImage { get; set; }
    }
}
