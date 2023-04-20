using Flubr.Areas.Identity.Data;
using Flubr.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flubr.Components
{
    public class HomePostViewComponent : ViewComponent
    {
        private readonly FlubrContext _FC;
        private readonly UserManager<ApplicationUser> _UM;

        public HomePostViewComponent(FlubrContext FC, UserManager<ApplicationUser> UM)
        {
            _FC = FC;
            _UM = UM;
        }
        
        
        [HttpPost]
        public async Task<IViewComponentResult> InvokeAsync(UserPostModel up)
        {
            //Guid g = Guid.NewGuid();
            //string guid = "f5ecb82e-9c55-4afd-816f-5c16e5209fa9";
            //var gu = new Guid(guid);
            //up.PostId = gu;
            //up.Id = _UM.GetUserAsync((System.Security.Claims.ClaimsPrincipal)User).Result.Id;
            //up.Date = DateTime.Now;
            //up.Likes = 0;

            //_FC.Add(up);
            //await _FC.SaveChangesAsync();

            return View();
        }
    }
}
