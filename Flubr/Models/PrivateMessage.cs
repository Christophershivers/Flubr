using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flubr.Models
{
    public class PrivateMessage
    {

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UsertName { get; set; }
        public string Message { get; set; }
        public string ProfileImage { get; set; }
        public DateTime Date { get; set; }
    }
}
