using Flubr.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flubr.Models
{
    public class FollowersModelView
    {
        

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string ProfileImage { get; set; }
        public string Id { get; set; }
    }
}
