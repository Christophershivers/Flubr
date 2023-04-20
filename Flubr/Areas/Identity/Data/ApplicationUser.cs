using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flubr.Models;
using Microsoft.AspNetCore.Identity;

namespace Flubr.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfileDescript { get; set; }
        public string ProfileImage { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string Birthday { get; set; }

        
    }
}
