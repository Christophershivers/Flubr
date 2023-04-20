using Flubr.Areas.Identity.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flubr.Models
{
    public class AccountViewModel
    {
        public UserMessagesModel UM { get; set; }
        public MessagesModel MM { get; set; }
        public ApplicationUser AU { get; set; }
        public List<UserPostModel> PM { get; set; }
        public UserPostModel PM2 { get; set; }
        public IFormFile Picture { get; set; }
    }
}
