using EmlakDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.EntityFramework;
using Microsoft.AspNetCore.Authentication;
using PagedList.Core;
using EntityLayer.Concrete;

namespace EmlakDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int page = 1, int pageSize = 8, int category = 0, bool ordering = false, string searchKeyword = "")
        {
            List<Post> posts = null;
            PostManager pm = null;
            CategoryManager cm = null;
            try
            {
                pm = new PostManager(new EfPostDal());
                cm = new CategoryManager(new EfCategoryDal());
                posts = pm.GetPosts(page, pageSize, category, ordering, searchKeyword);
                posts = pm.SetDescriptionLimit(posts, 60);
                ViewBag.BestSeller = pm.GetBestSeller();
                ViewBag.MostExpensives = pm.GetMostExpensive();
                ViewBag.Categories = cm.GetAll();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return View(posts);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult ProductDetail(int postId)
        {
            PostManager pm = null;
            ImageManager im = null;
            Post post = null;
            try
            {
                pm = new PostManager(new EfPostDal());
                im = new ImageManager(new EfImageDal());
                post = pm.GetById(postId);
                pm.IncreaseViews(postId);
                post.Images = im.GetAll().Where(x => x.PostId == postId).ToList();
                ViewBag.BestSeller = pm.GetBestSeller(5);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return View(post);
        }

        public async Task<IActionResult> LoginAsync(string userName, string password)
        {
            UserManager um = new UserManager(new EfUserDal());
            User user = null;
            try
            {
                user = new User();
                user.Name = userName;
                user.Password = password;
                if (um.IsUser(userName, password))
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, userName)
                    };
                    var userIdentity = new ClaimsIdentity(claims, "Admin");
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                    await HttpContext.SignInAsync(principal);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return RedirectToAction("Index", "Home");
            }
            if (um.IsUser(userName, password))
            {
                return RedirectToAction("Index", "Admin", new { area = "" });
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
