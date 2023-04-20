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
    public class BottomNavBarViewComponent : ViewComponent
    {
        private readonly ILogger<FollowersViewComponent> _logger;
        private readonly FlubrContext _FC;
        private readonly UserManager<ApplicationUser> _UM;
        private readonly SignalHub _Sig;
        private readonly MessagesRepository _MP;

        public BottomNavBarViewComponent(ILogger<FollowersViewComponent> logger, FlubrContext FC, UserManager<ApplicationUser> UM, SignalHub Sig, MessagesRepository MP)
        {
            _logger = logger;
            _FC = FC;
            _UM = UM;
            _Sig = Sig;
            _MP = MP;
        }


        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            var user = await _UM.GetUserAsync(HttpContext.User);
            var findUserForSignInImage = _FC.Users.Where(u => u.Id == user.Id).FirstOrDefault();
           
            return View(findUserForSignInImage);
        }
    }
}
