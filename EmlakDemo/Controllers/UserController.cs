using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using BusinessLayer.Concrete;
using DataAccessLayer.Concrete.EntityFramework;

namespace EmlakDemo.Controllers
{
    public class UserController : Controller
    {
        UserManager userManager = new UserManager(new EfUserDal());
        // GET: AdminController
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }


        // GET: UserController;
        [Authorize]
        public ActionResult List()
        {
            List<User> users = new List<User>();
            users = userManager.GetAll();
            return View(users);
        }

        // GET: UserController/Details/5
        [Authorize]
        public ActionResult Details(int id)
        {
            User user = null;
            user = userManager.GetById(id);
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
                result = userManager.Add(user);
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
            user = userManager.GetById(id);
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
                user = userManager.GetById(id);
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
                    result = userManager.Update(user);
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
                user = userManager.GetById(id);
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
                result = userManager.Delete(user);
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
