using Flubr.Areas.Identity.Data;
using Flubr.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flubr.Controllers
{
    public class FriendsController : Controller
    {
        private readonly FlubrContext _FC;

        public FriendsController(FlubrContext FC)
        {
            _FC = FC;
        }
        public IActionResult Index()
        {

            IdentityUser ide = new IdentityUser();

            return View();
        }


        public IActionResult Followers(string id)
        {
            List<FollowersModelView> followers = _FC.Followers.Where(o => o.FollowingId == id && o.FollowingId != o.UserId).Join(_FC.Users, follow => follow.UserId, user => user.Id, (follow, user) => new { follow, user }).Select(s => new FollowersModelView { FirstName = s.user.FirstName, LastName = s.user.LastName, UserName = s.user.UserName, Id = s.user.Id, ProfileImage = s.user.ProfileImage }).ToList();


            
            
            return View(followers);
        }

        public IActionResult Following(string id)
        {
            return View();
        }
    }
}
