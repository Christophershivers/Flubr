using Flubr.Areas.Identity.Data;
using Flubr.Models;
using Flubr.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Flubr.Components
{
    public class FollowingViewComponent: ViewComponent
    {
        private readonly ILogger<FollowersViewComponent> _logger;
        private readonly FlubrContext _FC;
        private readonly UserManager<ApplicationUser> _UM;
        private readonly SignalHub _Sig;
        private readonly MessagesRepository _MP;

        public FollowingViewComponent(ILogger<FollowersViewComponent> logger, FlubrContext FC, UserManager<ApplicationUser> UM, SignalHub Sig, MessagesRepository MP)
        {
            _logger = logger;
            _FC = FC;
            _UM = UM;
            _Sig = Sig;
            _MP = MP;
        }


        public IViewComponentResult Invoke(string id, int following)
        {
            ViewBag.following = following;
            List<ViewFollowersViewModel> myFollowing = _FC.Followers.Where(o => o.UserId == id).Join(_FC.Users, followers => followers.FollowingId, users => users.Id, (followers, users) => new { followers, users }).Select(s => new ViewFollowersViewModel { FirstName = s.users.FirstName, LastName = s.users.LastName, ProfileImage = s.users.ProfileImage }).Take(9).ToList();
            return View(myFollowing);
        }
    }
}
