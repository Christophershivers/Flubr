using Flubr.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flubr.Models
{
    public class PostUserModel
    {
        public Guid PostId { get; set; }
        public string Id { get; set; }
        public string Post { get; set; }
        public DateTime Date { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string ProfileImage { get; set; }
        public string Comment { get; set; }
        public int Likes { get; set; }
    }
}
