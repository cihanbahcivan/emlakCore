using System;
using BusinessLayer.Concrete;
using DataAccessLayer.Concrete.EntityFramework;
using EntityLayer.Concrete;

namespace KonsolUI
{
    class Program
    {
        static void Main(string[] args)
        {
            CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
            foreach (var category in categoryManager.GetAll())
            {
                Console.WriteLine(category.CategoryName);
            }
            Console.WriteLine("Hello World!");
        }
    }
}
