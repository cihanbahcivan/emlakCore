using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace EmlakDemo.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        // GET: AdminController
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: UserController;
        [Authorize]
        public ActionResult List()
        {
            List<User> users = new List<User>();
            users = _userService.GetAll();
            return View(users);
        }

        // GET: UserController/Details/5
        [Authorize]
        public ActionResult Details(int id)
        {
            User user = null;
            user = _userService.GetById(id);
            return View(user);
        }

        // GET: UserController/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            User user = null;
            int result = 0;
            try
            {
                user = new User();
                user.Name = collection["Name"];
                user.Surname = collection["Surname"];
                user.Email = collection["Email"];
                user.Password = collection["Password"];
                user.PhoneNumber = collection["PhoneNumber"];
                //user.Post = collection["Post"];
                result = _userService.Add(user);
            }
            catch
            {
                return View();
            }
            if (result == 0)
            {
                return View();
            }
            else
            {
                return RedirectToAction("List", "User");
            }
        }

        // GET: UserController/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            User user = null;
            user = _userService.GetById(id);
            return View(user);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            User user = null;
            int result = 0;
            try
            {
                user = _userService.GetById(id);
                if (user != null)
                {
                    user = new User();
                    user.Name = collection["Name"];
                    user.Surname = collection["Surname"];
                    user.Email = collection["Email"];
                    user.Password = collection["Password"];
                    user.PhoneNumber = collection["PhoneNumber"];
                    user.UserId = id;
                    //user.Post = collection["Post"];
                    result = _userService.Update(user);
                }
            }
            catch
            {
                return View();
            }
            if (result == 0)
            {
                return View();
            }
            else
            {
                return RedirectToAction("List", "User");
            }
        }

        // GET: UserController/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            User user = null;
            try
            {
                user = _userService.GetById(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return View(user);
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, User user)
        {
            int result = 0;
            try
            {
                result = _userService.Delete(user);
            }//TEST EDİLECEK
            catch
            {
                return View();
            }
            if (result == 0)
            {
                return View();
            }
            else
            {
                return RedirectToAction("List", "User");
            }
        }
    }
}
