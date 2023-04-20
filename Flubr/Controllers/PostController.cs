using Flubr.Areas.Identity.Data;
using Flubr.Models;
using Flubr.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flubr.Controllers
{
    public class PostController : Controller
    {
        private readonly ILogger<PostController> _logger;
        private readonly FlubrContext _FC;
        private readonly UserManager<ApplicationUser> _UM;
        private readonly SignalHub _Sig;
        private readonly MessagesRepository _MP;

        public PostController(ILogger<PostController> logger, FlubrContext FC, UserManager<ApplicationUser> UM, SignalHub Sig, MessagesRepository MP)
        {
            _logger = logger;
            _FC = FC;
            _UM = UM;
            _Sig = Sig;
            _MP = MP;
        }
        public IActionResult Comments(Guid id)
        {
            

            

            PostUserModel getPost = _FC.UserPosts.Where(p => p.PostId == id).Join(_FC.Users, post=> post.Id, users => users.Id, (post, users) => new { post, users }).Select(s => new PostUserModel { FirstName = s.users.FirstName, LastName = s.users.LastName, ProfileImage = s.users.ProfileImage, UserName = s.users.UserName, Post = s.post.Post, Likes = s.post.Likes, Date = s.post.Date, PostId = s.post.PostId }).SingleOrDefault();
            var signedInUserId = _UM.GetUserAsync(User).Result.Id;
            var ProfilePicture = _FC.Users.Where(u => u.Id == signedInUserId).Select(s => s.ProfileImage).SingleOrDefault();
            ViewBag.ProfileImage = ProfilePicture;
            return View(getPost);
        }

        [HttpPost]
        public async Task<IActionResult> Comments(CommentModel cm, PostUserModel pm)
        {
            Guid commentId = Guid.NewGuid();
            cm.UserId = _UM.GetUserAsync(User).Result.Id;
            cm.CommentId = commentId;
            cm.Likes = 0;
            DateTime now = DateTime.Now;
            string date = now.ToString("MMM, d yyyy H:mm tt");
            cm.Date = date;
            cm.Comment = pm.Comment;

            await _FC.Comments.AddAsync(cm);
            await _FC.SaveChangesAsync();

            PostUserModel getPost = _FC.UserPosts.Where(p => p.PostId == cm.PostId).Join(_FC.Users, post => post.Id, users => users.Id, (post, users) => new { post, users }).Select(s => new PostUserModel { FirstName = s.users.FirstName, LastName = s.users.LastName, ProfileImage = s.users.ProfileImage, UserName = s.users.UserName, Post = s.post.Post, Likes = s.post.Likes, Date = s.post.Date, PostId = s.post.PostId }).SingleOrDefault();
            return View(getPost);
        }


        [HttpPost]
        public IActionResult AddLikePost(Guid id)
        {
            var findUserPost = _FC.UserPosts.Where(p => p.PostId == id).SingleOrDefault();

            if (findUserPost != null)
            {
                findUserPost.Likes += 1;
                _FC.Entry(findUserPost).State = EntityState.Modified;
                _FC.SaveChanges();
            }

            return RedirectToAction("Comments", new { id });

        }

        [HttpPost]
        public IActionResult AddLikeComment(Guid id)
        {
            var findUserComment = _FC.Comments.Where(p => p.CommentId == id).SingleOrDefault();

            if (findUserComment != null)
            {
                findUserComment.Likes += 1;
                _FC.Entry(findUserComment).State = EntityState.Modified;
                _FC.SaveChanges();
            }

            var getPostId = _FC.Comments.Where(c => c.CommentId == id).Select(s => s.PostId).SingleOrDefault();
            var postId = getPostId;

            return RedirectToAction("Comments", new { id = getPostId });

        }
    }
}
