using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }
        public int Add(Category category)
        {
            try
            {
                _categoryDal.Add(category);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 0;
            }
            return 1;
        }

        public int Update(Category category)
        {
            try
            {
                _categoryDal.Update(category);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 0;
            }
            return 1;
        }

        public int Delete(Category category)
        {
            try
            {
                _categoryDal.Delete(category);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 0;
            }

            return 1;
        }

        public Category GetById(int id)
        {
            Category category = null;
            try
            {
                category = _categoryDal.Get(x => x.CategoryId == id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }
            return category;
        }

        public List<Category> GetAll()
        {
            List<Category> categories = null;
            try
            {
                categories = _categoryDal.GetAll();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return categories;
        }
    }
}
