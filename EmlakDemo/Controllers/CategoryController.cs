using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Concrete;
using DataAccessLayer.Concrete.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;

namespace EmlakDemo.Controllers
{
    public class CategoryController : Controller
    {
        private CategoryManager _categoryManager = new CategoryManager(new EfCategoryDal());
        public IActionResult Index()
        {
            var values = _categoryManager.GetAll();
            return View(values);
        }



        // GET: CategoryController
        public ActionResult List()
        {
            List<Category> categories = _categoryManager.GetAll();
            return View(categories);
        }

        // GET: CategoryController/Details/5
        public ActionResult Details(int id)
        {
            Category category = null;
            category = _categoryManager.GetById(id);
            return View(category);
        }

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            Category category = null;
            int result = 0;
            try
            {
                category = new Category();
                category.CategoryName = collection["CategoryName"];
                result = _categoryManager.Add(category);
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
                return RedirectToAction("List", "Category");
            }
        }

        // GET: CategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            Category category = null;
            category = _categoryManager.GetById(id);
            return View(category);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            Category category = null;
            int result = 0;
            try
            {
                category = _categoryManager.GetById(id);
                category.CategoryName = collection["CategoryName"];
                result = _categoryManager.Update(category);
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
                return RedirectToAction("List", "Category");
            }
        }

        // GET: CategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            Category category = null;
            try
            {
                category = _categoryManager.GetById(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return View(category);
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Category category, IFormCollection collection)
        {
            int result = 0;
            try
            {
                result = _categoryManager.Delete(category);
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
                return RedirectToAction("List", "Category");
            }
        }
    }
}
