using System.Collections.Generic;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class CityManager : ICityService
    {
        private readonly ICityDal _cityDal;

        public CityManager(ICityDal cityDal)
        {
            _cityDal = cityDal;
        }
        public int Add(City city)
        {
            _cityDal.Add(city);
            return 1;
        }

        public int Update(City city)
        {
            _cityDal.Update(city);
            return 1;
        }

        public int Delete(City city)
        {
            _cityDal.Delete(city);
            return 1;
        }

        public City GetById(int id)
        {
            return _cityDal.Get(x => x.CityId == id);
        }

        public List<City> GetAll()
        {
            return _cityDal.GetAll();
        }
    }
}