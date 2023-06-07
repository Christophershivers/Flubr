using Flubr.Areas.Identity.Data;
using Flubr.Models;
using Flubr.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Flubr.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly FlubrContext _FC;
        private readonly UserManager<ApplicationUser> _UM;
        private readonly SignalHub _Sig;
        private readonly MessagesRepository _MP;

        public HomeController(ILogger<HomeController> logger, FlubrContext FC, UserManager<ApplicationUser> UM, SignalHub Sig, MessagesRepository MP)
        {
            _logger = logger;
            _FC = FC;
            _UM = UM;
            _Sig = Sig;
            _MP = MP;
        }


        public IActionResult RealTime()
        {
            
            return View();
        }

        public JsonResult GetRealTime()
        {
            var post = _FC.UserPosts.ToList();
            return Json(post);
        }


        public IActionResult GetMessage(string id)
        {
            

            return View();
        }

        public IActionResult Message(string id)
        {

            //var test = "test";
           // _Sig.Clients.All.SendAsync("ListData", test);

            //var viewModel = _FC.UserMessage.Select(x => new UserMessagesModel { Id = x.Id, Message = x.Message, MessageId = x.MessageId }).ToList();

            //await _Sig.Clients.All.SendAsync("ListData", viewModel);
            

            return View();
        }


        public JsonResult test()
        {
            var jo = _FC.UserPosts.Join(_FC.Followers, post => post.Id, follow => follow.FollowingId, (post, follow) => new { post, follow }).Join(_FC.Users, pos => pos.post.Id, use => use.Id, (pos, use) => new { pos, use }).Select(s => new { s.use.FirstName, s.pos.post.Post }).ToList();
            return Json(jo);
        }
        


        public async Task<IActionResult> GetIndex()
        {
            var val1 = User?.Identity.IsAuthenticated;

            
            if (val1 == true)
            {

                UserPostModel up = new UserPostModel();
                //var posts = _FC.UserPosts.Include("applicationUser").Join(_FC.Followers, post => post.Id, follow => follow.FollowingId, (post, follow) => new { post, follow }).Where(o => o.follow.UserId == _UM.GetUserAsync(User).Result.Id).Select(s => new userpose { s.post.Date, s.post.Post }).ToList();

                string pass = "this is a test for the log";
                List<PostUserModel> post = _FC.UserPosts.Join(_FC.Followers, post => post.Id, follow => follow.FollowingId, (post, follow) => new { post, follow }).Where(o => o.follow.UserId == _UM.GetUserAsync(User).Result.Id)
                    .Join(_FC.Users, pos => pos.post.Id, use => use.Id, (pos, use) => new { pos, use })
                    .Select(s => new PostUserModel { Post = s.pos.post.Post, Date = s.pos.post.Date, FirstName = s.use.FirstName, LastName = s.use.LastName, ProfileImage = s.use.ProfileImage, UserName = s.use.UserName, Id = s.use.Id, Likes = s.pos.post.Likes, PostId = s.pos.post.PostId }).ToList();
                await _Sig.Clients.All.SendAsync("LoadData", pass);

                return Ok(post);

            }
            else
            {
                
                return Ok();

            }
        }




        public IActionResult AddLike(Guid id)
        {
            var findUserPost = _FC.UserPosts.Where(p => p.PostId == id).SingleOrDefault();

            if (findUserPost != null)
            {
                findUserPost.Likes += 1;
                _FC.Entry(findUserPost).State = EntityState.Modified;
                _FC.SaveChanges();
            }

            return RedirectToAction("Index");

        }






        public IActionResult Index()
        {
            var val1 = User?.Identity.IsAuthenticated;



            if (val1 == true)
            {

                UserPostModel up = new UserPostModel();
                //var posts = _FC.UserPosts.Include("applicationUser").Join(_FC.Followers, post => post.Id, follow => follow.FollowingId, (post, follow) => new { post, follow }).Where(o => o.follow.UserId == _UM.GetUserAsync(User).Result.Id).Select(s => new userpose { s.post.Date, s.post.Post }).ToList();
                

                List<PostUserModel> post = _FC.UserPosts.Join(_FC.Followers, post => post.Id, follow => follow.FollowingId, (post, follow) => new { post, follow }).Where(o => o.follow.UserId == _UM.GetUserAsync(User).Result.Id)
                    .Join(_FC.Users, pos => pos.post.Id, use => use.Id, (pos, use) => new { pos, use })
                    .Select(s => new PostUserModel { Post = s.pos.post.Post, Date = s.pos.post.Date, FirstName = s.use.FirstName, LastName = s.use.LastName, ProfileImage = s.use.ProfileImage, UserName = s.use.UserName, Id = s.use.Id, Likes = s.pos.post.Likes, PostId = s.pos.post.PostId }).ToList();

                
                return View(post);

            }
            else
            {
                
                return View();

            }
        }

        public IActionResult Search()
        {
            


            return View(_FC.Users.ToList());
        }


        public IActionResult Mes()
        {
            var list = _FC.Message.ToList();

            return View(list);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
