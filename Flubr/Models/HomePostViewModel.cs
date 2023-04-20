using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flubr.Models
{
    public class HomePostViewModel
    {
        public List<UserPostModel> UP { get; set; }

        public FollowersModel FM { get; set; }
    }
}
