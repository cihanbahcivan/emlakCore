using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface ICategoryService
    {
        public int Add(Category category);
        public int Update(Category category);
        public int Delete(Category category);
        public Category GetById(int id);
        public List<Category> GetAll();
    }
}
