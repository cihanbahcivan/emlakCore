using System.Collections.Generic;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IUserService
    {
        public int Add(User user);
        public int Update(User user);
        public int Delete(User user);
        public User GetById(int id);
        public List<User> GetAll();
        public bool IsUser(User userObj);
    }
}