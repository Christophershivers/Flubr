using Flubr.Areas.Identity.Data;
using Flubr.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SqlServiceBrokerListener;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Flubr.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _UM;
        private readonly FlubrContext _FC;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHubContext<DatabaseHub> _hubContext;

        public ProfileController(UserManager<ApplicationUser> UM, FlubrContext FC, IWebHostEnvironment webHostEnvironment, IHubContext<DatabaseHub> hubContext)
        {
            _UM = UM;
            _FC = FC;
            _webHostEnvironment = webHostEnvironment;
            _hubContext = hubContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Account(string id)
        {
            await _hubContext.Clients.All.SendAsync("DatabaseChanged", "it changed");
            ViewBag.change = "hey";
            var i = 0;
            Console.WriteLine("testing");
            var connection = "Server=(local)\\sqlexpress;Database=Flubr;Trusted_Connection=True;MultipleActiveResultSets=true";
            var listener = new SqlDependencyEx(connection, "Flubr", "UserMessage");
            listener.TableChanged += async (o, e) => {
                await _hubContext.Clients.All.SendAsync("DatabaseChanged", "it changed");

                



                Console.WriteLine("it changed");

            };
            listener.Start();

            ViewBag.testing = "";

            if(i == 0)
            {
                ViewBag.testing = "false";
            }
            else
            {
                ViewBag.testing = "true";
            }

            Console.WriteLine(ViewBag.testing);

            AccountViewModel AVM = new AccountViewModel();
            var findUser = await _UM.FindByIdAsync(id);
            var post = _FC.UserPosts.Where(x => x.Id == id).OrderByDescending(p => p.Date).ToList();
            AVM.AU = findUser;
            AVM.PM = post;
            var howManyFollowing = _FC.Followers.Where(o => o.UserId == id && o.UserId != o.FollowingId).Count();
            var howManyFollowers = _FC.Followers.Where(o => o.FollowingId == id).Count();

            ViewBag.following = howManyFollowing;
            ViewBag.followers = howManyFollowers;
            var checkFollow = _FC.Followers.Where(c => c.FollowingId == id && c.UserId == _UM.GetUserAsync(User).Result.Id).FirstOrDefault();

            ViewBag.follow = false;

            if(checkFollow != null)
            {
                ViewBag.follow = true;
            }

            //listener.Stop();
            return View(AVM);
        }
        [HttpPost]
        public async Task<IActionResult> Account(AccountViewModel av, string id)
        {
            Guid g = Guid.NewGuid();
            
            AccountViewModel AVM = new AccountViewModel();
            var findUser = await _UM.FindByIdAsync(id);
            var post = _FC.UserPosts.Where(x => x.Id == id).OrderByDescending(p => p.Date).ToList();
            AVM.AU = findUser;
            AVM.PM = post;
            av.PM2.PostId = g;
            av.PM2.Id = _UM.GetUserAsync(User).Result.Id;
            av.PM2.Date = DateTime.Now;
            av.PM2.Likes = 0;
            _FC.UserPosts.Add(av.PM2);
            _FC.SaveChanges();

            return RedirectToAction("Account", new { id });
        }


        public async Task<IActionResult> AddMessage(string id)
        {
            AccountViewModel AVM = new AccountViewModel();
            var findUser = await _UM.FindByIdAsync(id);
            AVM.AU = findUser;
            return View(AVM);
        }

        [HttpPost]
        public async Task<IActionResult> AddMessage(MessagesModel mm, UserMessagesModel um, string id, AccountViewModel AVM)
        {
            string randomString = "tg64Hngld";
            Guid g = Guid.NewGuid();

            var user = await _UM.FindByIdAsync(id);
            AVM.AU = user;
           

            //message
            AVM.MM.MessageId = g.ToString();
            AVM.MM.RecentDate = DateTime.Now;

            _FC.Message.Add(AVM.MM);

            _FC.SaveChanges();


            //user
            AVM.UM.Id = _UM.GetUserAsync(User).Result.Id;
            AVM.UM.MessageId = randomString;
            AVM.UM.Date = DateTime.Now;
            _FC.UserMessage.Add(AVM.UM);
            _FC.SaveChanges();


            //receipient message
            AVM.UM.MessageId = randomString;
            AVM.UM.Date = DateTime.Now;
            AVM.UM.Message = "";
            AVM.UM.Id = id;
            AVM.UM.UserMessageId = g;
            _FC.UserMessage.Add(AVM.UM);
            _FC.SaveChanges();







            return RedirectToAction("Account", new { id });
        }


        [HttpPost]
        public async Task <IActionResult> AddFollow(string id, FollowersModel fm)
        {
            Guid g = Guid.NewGuid();
            AccountViewModel AVM = new AccountViewModel();
            var findUser = await _UM.FindByIdAsync(id);
            var post = _FC.UserPosts.Where(x => x.Id == id).ToList();
            AVM.AU = findUser;
            AVM.PM = post;

            var checkFollow = _FC.Followers.Where(c => c.FollowingId == id && c.UserId == _UM.GetUserAsync(User).Result.Id).FirstOrDefault();

            if(checkFollow == null)
            {
                fm.UserId = _UM.GetUserAsync(User).Result.Id;
                fm.FollowingId = id;
                fm.Date = DateTime.Now;
                fm.Id = g;
                _FC.Followers.Add(fm);
                _FC.SaveChanges();
            }
            else
            {
                _FC.Followers.Remove(checkFollow);
                _FC.SaveChanges();
            }
            

            return RedirectToAction("Account", new { id });

        }


        [HttpPost]
        public async Task<IActionResult> EditAccount(AccountViewModel avm, string id)
        {
          
            AccountViewModel AVM = new AccountViewModel();
            var findUser = await _UM.FindByIdAsync(id);
            var post = _FC.UserPosts.Where(x => x.Id == id).ToList();
            AVM.AU = findUser;
            AVM.PM = post;

            
            
            
            
            
            if(avm.Picture != null)
            {
                
                string folder = "Users/" + avm.AU.Id + "/ProfileImage/";
                string rootFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                string subPath = rootFolder;

                Directory.CreateDirectory(subPath);

                folder += Guid.NewGuid().ToString() + "_" + avm.Picture.FileName;
                rootFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                avm.AU.ProfileImage = "/" + folder;
                await avm.Picture.CopyToAsync(new FileStream(rootFolder, FileMode.Create));
            }
            avm.AU.NormalizedUserName = avm.AU.NormalizedEmail;
            _FC.Users.Update(avm.AU);
            _FC.SaveChanges();






            return RedirectToAction("Account", new { id });
        }

    }
}
