using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using PagedList.Core;
using BusinessLayer.Concrete;
using DataAccessLayer.Concrete.EntityFramework;

namespace EmlakDemo.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly IImageService _imageService;
        public PostController(IPostService postService, IImageService imageService)
        {
            _postService = postService;
            _imageService = imageService;
        }

        public ActionResult List()
        {
            PagedList<Post> posts = null;
            try
            {
                posts = _postService.GetPosts(1, 8, 0, false);
                posts = _postService.SetDescriptionLimit(posts, 50);
                posts = _postService.SetTitleLimit(posts, 35);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                posts = null;
            }
            return View(posts);
        }

        // GET: PostController/Details/5
        [Authorize]
        public ActionResult Details(int id)
        {
            Post post = null;
            post = _postService.GetById(id);
            return View(post);
        }

        // GET: PostController/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: PostController/Create
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post post)
        {
            PostManager pm = null;
            Image image = null;
            int result = 0;
            try
            {
                Image image1 = new Image();
                pm = new PostManager(new EfPostDal());
                post.Code = pm.ProduceCode(pm.GetPosts(1, 8, 0, false));
                result = _postService.Add(post);
                image1.ImageFile = "";
                image1.PostId = post.PostId;
                _imageService.Add(image1);

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
                return RedirectToAction("List", "Post");
            }
        }

        // GET: PostController/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            Post post = null;
            post = _postService.GetById(id);
            return View(post);
        }

        // POST: PostController/Edit/5
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Post post)
        {
            int result = 0;
            try
            {
                result = _postService.Update(post);
            } //TEST EDİLECEK
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
                return RedirectToAction("List", "Post");
            }
        }

        // GET: PostController/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            Post post = null;
            try
            {
                post = _postService.GetById(id);
            }
            catch (Exception)
            {

                throw;
            }
            return View(post);
        }

        // POST: PostController/Delete/5
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Post post)
        {
            int result = 0;
            try
            {
                result = _postService.Delete(post);
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
                return RedirectToAction("List", "Post");
            }
        }
    }
}
