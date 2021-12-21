using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface ICityService 
    {
        public int Add(City city);
        public int Update(City city);
        public int Delete(City city);
        public City GetById(int id);
        public List<City> GetAll();
    }
}
