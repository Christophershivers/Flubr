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
    public class CommentViewComponent : ViewComponent
    {
        private readonly ILogger<CommentViewComponent> _logger;
        private readonly FlubrContext _FC;
        private readonly UserManager<ApplicationUser> _UM;
        private readonly SignalHub _Sig;
        private readonly MessagesRepository _MP;

        public CommentViewComponent(ILogger<CommentViewComponent> logger, FlubrContext FC, UserManager<ApplicationUser> UM, SignalHub Sig, MessagesRepository MP)
        {
            _logger = logger;
            _FC = FC;
            _UM = UM;
            _Sig = Sig;
            _MP = MP;
        }


        public  IViewComponentResult Invoke(Guid id)
        {
            
            List<ViewUserCommentsModel> getComments = _FC.Comments.Where(o => o.PostId == id).Join(_FC.Users, comment => comment.UserId, users => users.Id, (comment, users) => new { comment, users }).Select(s => new ViewUserCommentsModel { FirstName = s.users.FirstName, LastName = s.users.LastName, ProfileImage = s.users.ProfileImage, Comment = s.comment.Comment, UserName = s.users.UserName, Likes = s.comment.Likes, Date = s.comment.Date, CommentId = s.comment.CommentId }).ToList();
            return View(getComments);
        }
       
    }
}
